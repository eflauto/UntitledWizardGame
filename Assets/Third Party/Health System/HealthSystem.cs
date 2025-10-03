using System.Collections;
using UnityEngine;
using TMPro;

public class HealthSystem : MonoBehaviour
{
    private Rigidbody _rb;
    
    public float playerHealth = 30f;
    public float vulnerabilityCooldown = 1.5f;
    private float _vulnerabilityCooldownTimer;

    public TextMeshProUGUI resultText;
    public TextMeshProUGUI healthText;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        
        SetHealthText();
        if (resultText is not null) resultText.enabled = false;
    }

    private void Update()
    {
        if (playerHealth <= 0f)
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
        GameObject parentGameObject;
        
        try
        {
            // Try to get the parent, which is where the Enemy tag would be.
            parentGameObject = collision.transform.parent.gameObject;
        }
        catch
        {
            // If there is no parent, it can't be an enemy.
            return;
        }

        if (!parentGameObject.CompareTag("Enemy") || _vulnerabilityCooldownTimer > 0f) return;
        
        playerHealth -= parentGameObject.GetComponent<EnemyManager>().attackPower;
        Debug.Log(playerHealth);
        _vulnerabilityCooldownTimer = vulnerabilityCooldown;
        StartCoroutine(VulnerabilityCooldownCountdownCoroutine());
        SetHealthText();
    }
    
    private void SetHealthText()
    {
        if (healthText is null) return;
        
        // Cast the playerHealth to an int to get a clean output.
        healthText.text = "Health: " + (int)playerHealth;
    }

    private void SetResultText()
    {
        if (resultText is not null)
        {
            resultText.text = "You died!";
            resultText.enabled = true;
        }
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
