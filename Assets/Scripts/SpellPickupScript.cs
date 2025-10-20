using UnityEngine;

public class SpellPickupScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        MainManager.Instance.unlockedSpellsCount += 1;
        Destroy(gameObject);
    }
}
