using UnityEngine;

public class SpellPickupScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        MainManager.Instance.unlockedSpellsCount += 1;
        Destroy(this);
    }
}
