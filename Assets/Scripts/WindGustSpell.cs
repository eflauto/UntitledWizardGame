using UnityEngine;
using System.Collections;

public class WindGustSpell : Attack
{
    public float windDistance = 4.0f;
    public float windRadius = 0.5f;
    public float windForce = 100;
    public LayerMask layerMask;

    public WindGustSpell()
    {
        attackPower = 0;
        manaCost = 20f;
    }
    
    public void WindGust(Transform callerTransform)
    {
        var fwd = callerTransform.TransformDirection(Vector3.forward);
        var windSpawnPosition = fwd * 1.5f + callerTransform.position;
        var windBlastedHits = Physics.SphereCastAll(windSpawnPosition, windRadius, fwd, windDistance, layerMask);
        
        foreach (var rayHit in windBlastedHits)
        {
            var hitRigidbody = rayHit.transform.GetComponent<Rigidbody>();
            
            if (rayHit.transform.CompareTag("Enemy"))
            {
                hitRigidbody.constraints ^= RigidbodyConstraints.FreezePosition;
                StartCoroutine(ReactivateEnemyCoroutine(hitRigidbody));
                hitRigidbody.AddForce(fwd.normalized * (windForce * 0.25f), ForceMode.Impulse);
            }
            else
            {
                hitRigidbody.AddForce(fwd.normalized * windForce, ForceMode.Impulse);
            }
        }
    }

    private IEnumerator ReactivateEnemyCoroutine(Rigidbody hitRigidbody)
    {
        var timer = 0f;

        while (timer < 2f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        hitRigidbody.constraints ^= RigidbodyConstraints.FreezePosition;
    }
}
