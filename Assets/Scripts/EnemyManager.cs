using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    private Transform _player;
    
    private NavMeshAgent _agent;
    
    private Rigidbody _rigidbody;

    public float health = 20f;
    public float attackPower = 5f;
    
    protected bool isDead = false;
    
    protected void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    protected void Update()
    {
        if (isDead) return;

        if (health <= 0f)
        {
            isDead = true;
            return;
        }
        
        _agent.SetDestination(_player.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Attack")) return;

        var attackObjectScript = collision.gameObject.GetComponent<Attack>();
        var damage = attackObjectScript.attackPower;
        
        TakeDamage(damage);
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
