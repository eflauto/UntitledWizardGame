using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AttackObject : MonoBehaviour
{
    public float attackPower = 10f;
    
    public float objectForce = 1000f;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) return;
        
        // TODO: Add animation
        
        Destroy(gameObject);
    }
}
