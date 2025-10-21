using System.Collections;
using UnityEngine;

public class Slime : EnemyManager
{
    public float startJumpDistance = 5f;
    public float jumpTime = 2f;
    public float shootTime = 2f;
    public float jumpForce = 10f;

    public GameObject bullet;
    public float bulletForce = 20f;
    
    private AudioManager _audioManager;
    
    private bool _isAggro;
    private bool _isDead;
    private bool _preparingToJump;
    private bool _preparingToShoot;
    private bool _isGrounded = true;

    private new void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        _audioManager = GetComponent<AudioManager>();
    }

    private new void Update()
    {
        if (_isDead) return;

        if (health <= 0f)
        {
            _isDead = true;
            StartCoroutine(ScaleToZeroCoroutine());
            return;
        }

        if (!_isAggro) return;
        
        if (_preparingToJump || _preparingToShoot) return;
        
        if (!_isGrounded) return;

        if (rb.useGravity)
        {
            rb.useGravity = false;
            rb.linearVelocity = Vector3.zero;
            rb.constraints ^= RigidbodyConstraints.FreezePosition;
        }

        var distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= startJumpDistance)
        {
            if (_preparingToJump) return;
            StartCoroutine(JumpCoroutine());
            _preparingToJump = true;
        }
        else
        {
            StartCoroutine(ShootCoroutine());
            _preparingToShoot = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Environment")) _isGrounded = true;
        
        if (!collision.gameObject.CompareTag("Attack")) return;

        var attackObjectScript = collision.gameObject.GetComponent<Attack>();
        var damage = attackObjectScript.attackPower;

        if (!_isAggro) _isAggro = true;

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

    private IEnumerator JumpCoroutine()
    {
        var timer = 0f;

        while (timer < jumpTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        
        transform.LookAt(player.position);
        rb.constraints ^= RigidbodyConstraints.FreezePosition;
        rb.useGravity = true;
        rb.AddForce(-(transform.position - player.position) + new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        _audioManager.PlaySound("slime_jump", transform.position);
        _preparingToJump = false;
        _isGrounded = false;
    }

    private IEnumerator ShootCoroutine()
    {
        var timer = 0f;

        while (timer < shootTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        Physics.Raycast(transform.position, player.position - transform.position, out var hit);

        if (hit.collider.CompareTag("Player"))
        {
            transform.LookAt(player.position);
        
            var bulletPosition = transform.forward * 3f + transform.position;
            var bulletInstance = Instantiate(bullet, bulletPosition, transform.rotation);
        
            bulletInstance.GetComponent<Rigidbody>().AddForce(transform.forward * bulletForce, ForceMode.Impulse);
            _audioManager.PlaySound("slime_spit", transform.position);
        }
        
        _preparingToShoot = false;
    }

    private void HandleTrigger(Collider other)
    {
        if (_isAggro) return;

        if (!other.gameObject.CompareTag("Player")) return;

        Physics.Raycast(transform.position, other.transform.position - transform.position, out var hit);

        if (hit.collider.gameObject.CompareTag("Player")) _isAggro = true;
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