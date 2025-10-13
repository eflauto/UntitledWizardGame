using UnityEngine;

public class FireBreathSpell : Attack
{
    public GameObject fireBreathObject;
    public float fireDistance = 2.0f;
    public LayerMask layerMask;

    public FireBreathSpell()
    {
        attackPower = 100f;
    }
    
    public void FireBreath(Transform callerTransform)
    {
        var instance = Instantiate(fireBreathObject, callerTransform.position, callerTransform.rotation);
        var fwd = callerTransform.TransformDirection(Vector3.forward);
        var fireSpawnPosition = fwd * 1.5f + callerTransform.position;
        var fireBlastedHits = Physics.BoxCastAll(fireSpawnPosition, new Vector3(1, .5f, 1), fwd, callerTransform.rotation, fireDistance, layerMask);

        foreach (var rayHit in fireBlastedHits)
        {
            // TODO: implement damage, other fire-related effects
            continue;
        }
        
        //Destroy(instance, 2.0f); TODO: get self-destruction working
        
    }
}
