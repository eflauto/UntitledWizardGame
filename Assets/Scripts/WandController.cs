using System.Collections.Generic;
using UnityEngine;

public class WandController : MonoBehaviour
{
    public List<string> attacks;
    public List<GameObject> attackObjects;

    private Animator _animator;

    private Camera _playerCamera;
    private AudioManager _playerAudioManager;

    public ManaBar manaBar;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _playerAudioManager = GameObject.Find("Player").GetComponent<AudioManager>();
        
        manaBar.SetMaxMana(MainManager.Instance.maxMana);
        manaBar.SetMana(MainManager.Instance.mana);
    }

    private void Update()
    {
        if (MainManager.Instance.paused) return;

        if (MainManager.Instance.mana > MainManager.Instance.maxMana)
        {
            MainManager.Instance.mana = MainManager.Instance.maxMana;
            manaBar.SetMana(MainManager.Instance.mana);
        } 
        else if (MainManager.Instance.mana < MainManager.Instance.maxMana)
        {
            MainManager.Instance.mana += MainManager.Instance.manaRegen * Time.deltaTime;
            manaBar.SetMana(MainManager.Instance.mana);
        }
        
        var currentAnimatorClip = _animator.GetCurrentAnimatorClipInfo(0)[0].clip;
        
        if (Input.GetButtonDown("Fire1") && currentAnimatorClip.name == "Idle")
        {
            var activatedSpellIndex = MainManager.Instance.selectedSpell;
            
            if (activatedSpellIndex > MainManager.Instance.unlockedSpellsCount)
            {
                activatedSpellIndex = -1;
            }
            
            switch (activatedSpellIndex)
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
                    Debug.LogError("Tried to use an invalid/locked attack!");
                    break;
            }
        }

        CheckForSpellChange();
    }

    private void BasicAttack()
    {
        var attackObject = attackObjects[MainManager.Instance.selectedSpell];
        var attackObjectComponent = attackObject.GetComponent<AttackObject>();
        
        if (attackObjectComponent.manaCost > MainManager.Instance.mana) return;
        
        _playerAudioManager.PlaySound("wind_gust");
        
        MainManager.Instance.mana -= attackObjectComponent.manaCost;
        manaBar.SetMana(MainManager.Instance.mana);
        
        var forward = _playerCamera.transform.TransformDirection(Vector3.forward);
        var attackObjectPosition = forward * 1.5f + transform.position;
        var attackObjectInstance = Instantiate(attackObject, attackObjectPosition, transform.rotation);
        var attackObjectForce = attackObjectComponent.objectForce;

        attackObjectInstance.GetComponent<Rigidbody>().AddForce(forward * attackObjectForce, ForceMode.Impulse);
    }

    // Function to call the RockAttack method in the rockAttack script, passing the caller's transform data.
    private void RockAttack()
    {
        //Debug.Log("Rock attack!");
        var rockObjectSpell = attackObjects[MainManager.Instance.selectedSpell];
        var rockAttack = rockObjectSpell.GetComponent<RockAttack>();
        
        if (rockAttack.manaCost > MainManager.Instance.mana) return;
        
        _playerAudioManager.PlaySound("wind_gust");
        
        MainManager.Instance.mana -= rockAttack.manaCost;
        manaBar.SetMana(MainManager.Instance.mana);
        
        rockObjectSpell.GetComponent<RockAttack>().RockAttackSpell(_playerCamera.transform);

    }
    private void WindGust()
    {
        //Debug.Log("Wind gust!");
        var spellObject = attackObjects[MainManager.Instance.selectedSpell];
        var spellAttack = spellObject.GetComponent<WindGustSpell>();
        
        if (spellAttack.manaCost > MainManager.Instance.mana) return;
        
        _playerAudioManager.PlaySound("blow_air");
        
        MainManager.Instance.mana -= spellAttack.manaCost;
        manaBar.SetMana(MainManager.Instance.mana);
        
        spellAttack.WindGust(_playerCamera.transform);

    }
    private void FireBreath()
    {
        //Debug.Log("Fire Breath!");
        var spellObject = attackObjects[MainManager.Instance.selectedSpell];
        var spellAttack = spellObject.GetComponent<FireBreathSpell>();
        
        if (spellAttack.manaCost > MainManager.Instance.mana) return;
        
        _playerAudioManager.PlaySound("blow_air");

        MainManager.Instance.mana -= spellAttack.manaCost;
        manaBar.SetMana(MainManager.Instance.mana);
        
        spellAttack.FireBreath(_playerCamera.transform);

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
