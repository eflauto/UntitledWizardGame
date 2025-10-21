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
    
    private Transform _player;
    private Rigidbody _rigidbody;
    private AudioManager _audioManager;
    
    private bool _isAggro;
    private bool _isDead;
    private bool _preparingToJump;
    private bool _preparingToShoot;

    private new void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _rigidbody = GetComponent<Rigidbody>();
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
        
        if (!CheckIfGrounded()) return;

        if (_rigidbody.useGravity)
        {
            _rigidbody.useGravity = false;
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.constraints ^= RigidbodyConstraints.FreezePosition;
        }

        var distanceToPlayer = Vector3.Distance(_player.position, transform.position);

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

    private bool CheckIfGrounded()
    { 
        return Physics.Raycast(transform.position, -Vector3.up, 1.35f);
    }

    private IEnumerator JumpCoroutine()
    {
        var timer = 0f;

        while (timer < jumpTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        
        transform.LookAt(_player.position);
        _rigidbody.constraints ^= RigidbodyConstraints.FreezePosition;
        _rigidbody.useGravity = true;
        _rigidbody.AddForce(-(transform.position - _player.position) + new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        _audioManager.PlaySound("slime_jump", transform.position);
        _preparingToJump = false;
    }

    private IEnumerator ShootCoroutine()
    {
        var timer = 0f;

        while (timer < shootTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        
        transform.LookAt(_player.position);
        
        var bulletPosition = transform.forward * 3f + transform.position;
        var bulletInstance = Instantiate(bullet, bulletPosition, transform.rotation);
        
        bulletInstance.GetComponent<Rigidbody>().AddForce(transform.forward * bulletForce, ForceMode.Impulse);
        _audioManager.PlaySound("slime_spit", transform.position);
        _preparingToShoot = false;
    }

    private void HandleTrigger(Collider other)
    {
        if (_isAggro) return;

        if (!other.gameObject.CompareTag("Player")) return;

        Physics.Raycast(transform.position, other.transform.position - transform.position, out var hit);

        Debug.Log(hit.collider.name);

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