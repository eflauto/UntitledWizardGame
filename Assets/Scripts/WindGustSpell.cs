using UnityEngine;

/*

*/
public class WindGustSpell : Attack
{
    public GameObject windCapsuleObject;
    public float windDistance = 4.0f;
    public float windRadius = 0.5f;
    public float windForce = 100;
    public LayerMask layerMask;

    public WindGustSpell()
    {
        attackPower = 0;
    }
    
    public void WindGust(Transform callerTransform)
    {
        var instance = Instantiate(windCapsuleObject, callerTransform.position, callerTransform.rotation);
        var fwd = callerTransform.TransformDirection(Vector3.forward);
        var windSpawnPosition = fwd * 1.5f + callerTransform.position;
        var windBlastedHits = Physics.SphereCastAll(windSpawnPosition, windRadius, fwd, windDistance, layerMask);
        
        foreach (var rayHit in windBlastedHits)
        {
            rayHit.transform.GetComponent<Rigidbody>().AddForce(fwd.normalized * windForce, ForceMode.Impulse);
        }
        
        //Destroy(instance, 2.0f); TODO: get self-destruction working
        
    }
}
