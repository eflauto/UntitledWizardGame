using UnityEngine;

public class FireBreathSpell : Attack
{
    public GameObject fireBreathObject;
    public float fireDistance = 2.0f;
    public LayerMask layerMask;

    public FireBreathSpell()
    {
        attackPower = 100f;
        manaCost = 40f;
    }
    
    public void FireBreath(Transform callerTransform)
    {
        var instance = Instantiate(fireBreathObject, callerTransform.position, callerTransform.rotation);
        var fwd = callerTransform.TransformDirection(Vector3.back); // this use of .back will send the boxcast properly forward   
        var fireSpawnPosition = fwd * 1.1f + callerTransform.position;
        var fireBlastedHits = Physics.BoxCastAll(fireSpawnPosition, new Vector3(1, .5f, 1), fwd, callerTransform.rotation, fireDistance, layerMask);

        foreach (var rayHit in fireBlastedHits)
        {
            if (!rayHit.collider.gameObject.CompareTag("Enemy") && !rayHit.collider.gameObject.name.Contains("Plane")) return;

            if (rayHit.transform.parent.name == "Web")
            {
                Destroy(rayHit.transform.parent.gameObject);
            }
            else
            {
                rayHit.collider.gameObject.GetComponent<EnemyManager>().TakeDamage(attackPower);
            }
        }
        
        //Destroy(instance, 2.0f); TODO: get self-destruction working
        
    }
}
