using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AttackObject : Attack
{
    public float objectForce = 1000f;
    
    public AttackObject()
    {
        attackPower = 10f;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) return;
        
        // TODO: Add animation
        
        Destroy(gameObject);
    }
}
