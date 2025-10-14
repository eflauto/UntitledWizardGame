using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class HealthSystem : MonoBehaviour
{
    private Rigidbody _rb;
    
    public float vulnerabilityCooldown = 1.5f;
    private float _vulnerabilityCooldownTimer = 0f;

    public TextMeshProUGUI resultText;
    public HealthBar healthBar;

    private GameObject _resultsScreen;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _resultsScreen = GameObject.Find("UI").transform.Find("ResultsScreen").gameObject;
        
        healthBar.SetMaxHealth(MainManager.Instance.maxHealth);
    }

    private void Update()
    {
        if (MainManager.Instance.health <= 0f)
        {
            SetResultText();
            // Destroy the script. Can be expanded later to a death animation or something.
            Destroy(this);
        }
    }

    private void OnCollisionEnter(Collision collision) { HandleCollision(collision); }
    
    private void OnCollisionStay(Collision collision) { HandleCollision(collision); }

    private void HandleCollision(Collision collision)
    {
        var collisionObject = collision.gameObject;
        
        if ((!collisionObject.CompareTag("Enemy") && !collisionObject.CompareTag("Boss")) || _vulnerabilityCooldownTimer > 0f) return;
        
        TakeDamage(collisionObject.GetComponent<EnemyManager>().attackPower);
        _vulnerabilityCooldownTimer = vulnerabilityCooldown;
        StartCoroutine(VulnerabilityCooldownCountdownCoroutine());
    }


    private void TakeDamage(float damage)
    {
        MainManager.Instance.health -= damage;
        healthBar.SetHealth(MainManager.Instance.health);
    }

    private void SetResultText()
    {
        if (resultText is null) return;
        
        resultText.text = "You died!";
        resultText.colorGradient = new VertexGradient(Color.red, Color.red, new Color(0.15f, 0f, 0f), new Color(0.15f, 0f, 0f));
        MainManager.Instance.PauseGame(_resultsScreen);
    }

    private IEnumerator VulnerabilityCooldownCountdownCoroutine()
    {
        while (_vulnerabilityCooldownTimer > 0f)
        {
            _vulnerabilityCooldownTimer -= Time.deltaTime;
            yield return null;
        }
    }
}
