using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class WandController : MonoBehaviour
{
    public List<string> attacks;
    public List<GameObject> attackObjects;
    private int _selectedAttack = 0;
    
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
            default:
                Debug.LogError("Tried to use an invalid attack!");
                break;
        }
    }

    private void BasicAttack()
    {
        var attackObject = attackObjects[_selectedAttack];
        var attackObjectInstance = Instantiate(attackObject, transform.position, transform.rotation);
        var attackObjectForce = attackObjectInstance.GetComponent<AttackObject>().objectForce;
        var forward = _playerCamera.transform.TransformDirection(Vector3.forward);
        
        attackObjectInstance.GetComponent<Rigidbody>().AddForce(forward * attackObjectForce);
    }
}
