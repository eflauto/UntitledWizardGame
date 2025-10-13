using System.Collections.Generic;
using UnityEngine;

public class WandController : MonoBehaviour
{
    public List<string> attacks;
    public List<GameObject> attackObjects;

    private Animator _animator;

    private Camera _playerCamera;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (MainManager.Instance.paused) return;
        
        if (Input.GetButtonDown("Fire1"))
        {
            switch (MainManager.Instance.selectedSpell)
            {
                case 0:
                    // Somehow, this is faster than the direct string lookup.
                    _animator.SetTrigger(Animator.StringToHash("Attack"));
                    BasicAttack();
                    break;
                case 1:
                    _animator.SetTrigger(Animator.StringToHash("Attack"));
                    RockAttack();
                    break;
                case 2:
                    _animator.SetTrigger(Animator.StringToHash("Attack"));
                    WindGust();
                    break;
                case 3:
                    _animator.SetTrigger(Animator.StringToHash("Attack"));
                    FireBreath();
                    break;
                default:
                    Debug.LogError("Tried to use an invalid attack!");
                    break;
            }
        }

        CheckForSpellChange();
    }

    private void BasicAttack()
    {
        var forward = _playerCamera.transform.TransformDirection(Vector3.forward);
        var attackObject = attackObjects[MainManager.Instance.selectedSpell];
        var attackObjectPosition = forward * 1.5f + transform.position;
        var attackObjectInstance = Instantiate(attackObject, attackObjectPosition, transform.rotation);
        var attackObjectForce = attackObjectInstance.GetComponent<AttackObject>().objectForce;

        attackObjectInstance.GetComponent<Rigidbody>().AddForce(forward * attackObjectForce, ForceMode.Impulse);
    }

    // Function to call the RockAttack method in the rockAttack script, passing the caller's transform data.
    private void RockAttack()
    {
        //Debug.Log("Rock attack!");
        var rockObjectSpell = attackObjects[MainManager.Instance.selectedSpell];
        rockObjectSpell.GetComponent<RockAttack>().RockAttackSpell(_playerCamera.transform);

    }
    private void WindGust()
    {
        //Debug.Log("Wind gust!");
        var spellObject = attackObjects[MainManager.Instance.selectedSpell];
        spellObject.GetComponent<WindGustSpell>().WindGust(_playerCamera.transform);

    }
    private void FireBreath()
    {
        //Debug.Log("Fire Breath!");
        var spellObject = attackObjects[MainManager.Instance.selectedSpell];
        spellObject.GetComponent<FireBreathSpell>().FireBreath(_playerCamera.transform);

    }

    private void CheckForSpellChange()
    {
        var spellIndex = MainManager.Instance.selectedSpell;

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            spellIndex++;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            spellIndex--;
        }
        else if (Input.anyKeyDown)
        {
            for (var numPressed = 1; numPressed < attacks.Count + 1; numPressed++)
            {
                if (Input.GetKeyDown(numPressed.ToString()))
                {
                    spellIndex = numPressed - 1;
                }
            }
        }

        if (spellIndex < 0) { spellIndex = attacks.Count - 1; }
        if (spellIndex > attacks.Count - 1) { spellIndex = 0; }

        if (!MainManager.Instance.selectedSpell.Equals(spellIndex)) { MainManager.Instance.NewSpellSelected(spellIndex); }
    }
}
