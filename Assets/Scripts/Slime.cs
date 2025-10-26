using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Slime : EnemyManager
{
    public float startJumpDistance = 5f;
    public float jumpTime = 2f;
    public float shootTime = 2f;
    public float jumpForce = 10f;

    public GameObject bullet;
    public float bulletForce = 20f;
    
    private bool _preparingToJump;
    private bool _preparingToShoot;
    private bool _isGrounded = true;

    private int _didNotShootInstances = 0;

    private new void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        audioManager = GetComponent<AudioManager>();
    }

    private new void Update()
    {
        if (isDead) return;

        if (health <= 0f)
        {
            isDead = true;
            StartCoroutine(ScaleToZeroCoroutine());
            return;
        }

        if (!isAggro) return;

        if (_didNotShootInstances > 2)
        {
            isAggro = false;
            return;
        }
        
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

        if (!isAggro) isAggro = true;

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
        audioManager.PlaySound("slime_jump", transform.position);
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
            audioManager.PlaySound("slime_spit", transform.position);
        }
        else
        {
            _didNotShootInstances++;
        }
        
        _preparingToShoot = false;
    }

    private void HandleTrigger(Collider other)
    {
        if (isAggro) return;

        if (!other.gameObject.CompareTag("Player")) return;

        Physics.Raycast(transform.position, other.transform.position - transform.position, out var hit);

        if (hit.collider.gameObject.CompareTag("Player")) isAggro = true;
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