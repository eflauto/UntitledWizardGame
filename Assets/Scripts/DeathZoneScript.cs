using UnityEngine;

public class DeathZoneScript : MonoBehaviour
{
    // sets player health to 0 when player collides with trigger
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        MainManager.Instance.health = 0;
       
    }
}
