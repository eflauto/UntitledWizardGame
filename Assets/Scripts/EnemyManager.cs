using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    private Transform _player;
    
    private NavMeshAgent _agent;

    public float health = 20f;
    public float attackPower = 5f;

    private bool _isDead = false;
    
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
    }
    
    private void Update()
    {
        if (_isDead) return;

        if (health <= 0f)
        {
            _isDead = true;
            return;
        }
        
        _agent.SetDestination(_player.position);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
