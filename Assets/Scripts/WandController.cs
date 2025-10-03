using System.Collections.Generic;
using UnityEngine;

public class WandController : MonoBehaviour
{
    public List<string> attacks;
    private string _selectedAttack;
    
    private Animator _animator;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (!Input.GetButtonDown("Fire1")) return;
        
        // 0 is the "Attack" parameter of the wand. You can check these in the animator window.
        _animator.SetTrigger(Animator.StringToHash("Attack"));
        BasicAttack();
    }

    private void BasicAttack()
    {
        
    }
}
