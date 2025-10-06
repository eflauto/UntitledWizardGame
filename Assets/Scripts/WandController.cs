using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class WandController : MonoBehaviour
{
    public List<string> attacks;
    public List<GameObject> attackObjects;
    // added selected attack to public for now for easier testing
    // TODO: re-privatize selected attack later, make internal function that allows variable to be changed to only valid values
    public int _selectedAttack = 2;

    private Animator _animator;

    private Camera _playerCamera;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (!Input.GetButtonDown("Fire1")) return;

        switch (_selectedAttack)
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
            default:
                Debug.LogError("Tried to use an invalid attack!");
                break;
        }
    }

    private void BasicAttack()
    {
        var forward = _playerCamera.transform.TransformDirection(Vector3.forward);
        var attackObject = attackObjects[_selectedAttack];
        var attackObjectPosition = forward * 1.5f + transform.position;
        var attackObjectInstance = Instantiate(attackObject, attackObjectPosition, transform.rotation);
        var attackObjectForce = attackObjectInstance.GetComponent<AttackObject>().objectForce;

        attackObjectInstance.GetComponent<Rigidbody>().AddForce(forward * attackObjectForce, ForceMode.Impulse);
    }

    // Function to call the RockAttack method in the rockAttack script, passing the caller's transform data.
    private void RockAttack()
    {
        //Debug.Log("Rock attack!");
        var rockObjectSpell = attackObjects[_selectedAttack];
        rockObjectSpell.GetComponent<RockAttack>().RockAttackSpell(_playerCamera.transform);

    }
    private void WindGust()
    {
        //Debug.Log("Wind gust!");
        var spellObject = attackObjects[_selectedAttack];
        spellObject.GetComponent<WindGustSpell>().WindGust(_playerCamera.transform);

    }
}
