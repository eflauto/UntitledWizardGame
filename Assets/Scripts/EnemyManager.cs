using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    private Transform _player;
    
    private NavMeshAgent _agent;
    
    private Rigidbody _rigidbody;

    public float health = 20f;
    public float attackPower = 5f;

    public float scaleDuration = 2f;

    protected bool isAggro = false;
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
            _agent.isStopped = true;
            StartCoroutine(ScaleToZeroCoroutine());
            return;
        }

        if (isAggro)
        {
            _agent.SetDestination(_player.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Attack")) return;

        var attackObjectScript = collision.gameObject.GetComponent<Attack>();
        var damage = attackObjectScript.attackPower;
        
        if (!isAggro) { isAggro = true; }
        
        TakeDamage(damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleTrigger(other);
    }

    private void OnTriggerStay(Collider other)
    {
        HandleTrigger(other);
    }

    private void HandleTrigger(Collider other)
    {
        if (isAggro) return;

        if (!other.gameObject.CompareTag("Player")) return;
        
        Physics.Raycast(transform.position, other.transform.position - transform.position, out var hit);
        
        Debug.Log(hit.collider.name);
        
        if (hit.collider.gameObject.CompareTag("Player"))
        {
            isAggro = true;
        }
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
    }
    
    private IEnumerator ScaleToZeroCoroutine()
    {
        var timer = 0f;
        var initialScale = transform.localScale;
        var targetScale = Vector3.zero;

        while (timer < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(initialScale, targetScale, timer / scaleDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        
        transform.localScale = targetScale;
        Destroy(gameObject);
    }
}
