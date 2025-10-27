using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    [DoNotSerialize] protected Transform player;
    
    private NavMeshAgent _agent;
    
    [DoNotSerialize] protected Rigidbody rb;

    [DoNotSerialize] protected AudioManager audioManager;

    public float health = 20f;
    public float attackPower = 5f;

    public float scaleDuration = 2f;

    protected bool isAggro = false;
    protected bool isDead = false;

    private Animator _animator;
    
    protected void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        audioManager = GetComponent<AudioManager>();
        _animator = transform.GetChild(0).GetComponent<Animator>();
    }
    
    protected void Update()
    {
        if (isDead) return;

        if (health <= 0f)
        {
            isDead = true;
            _agent.isStopped = true;
            StartCoroutine(ScaleToZeroCoroutine());
            audioManager.StopSound();
            _animator.SetTrigger(Animator.StringToHash("Dead"));
            return;
        }

        if (isAggro)
        {
            _agent.SetDestination(player.position);

            if (!audioManager.SoundPlaying())
            {
                audioManager.SetSpatializeSettings(1f, 25);
                audioManager.PlayLoop("spider_crawl");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Attack")) return;

        var attackObjectScript = collision.gameObject.GetComponent<Attack>();
        var damage = attackObjectScript.attackPower;

        if (!isAggro)
        {
            isAggro = true;
            _animator.SetTrigger(Animator.StringToHash("Walking"));
        }
        
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
        
        if (hit.collider.gameObject.CompareTag("Player"))
        {
            isAggro = true;
            _animator.SetTrigger(Animator.StringToHash("Walking"));
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
