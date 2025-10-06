using UnityEngine;

/*
Currently Broken
TODO: fix this madness
- currently struggling both with orientation of the created prefab, getting the prefab to add force of some kind to a rigidbody, and with self deletion.
*/
public class WindGustSpell : MonoBehaviour
{
    public GameObject windCapsuleObject;
    public float windDistance = 4.0f;
    public float windRadius = 0.5f;
    public float windForce = 100;
    public LayerMask layerMask;

    public void WindGust(Transform callerTransform)
    {
        //Debug.Log(callerTransform.transform, callerTransform.forward);
        UnityEngine.Object instance = Instantiate(WindCapsuleObject, callerTransform.position, callerTransform.rotation);
        //Collider[] windBlastedColliders = Physics.OverlapCapsule(WindCapsuleObject.GetComponent<Collider>().);
        Vector3 fwd = callerTransform.TransformDirection(Vector3.forward);
        var windBlastedColliders = Physics.SphereCastAll(callerTransform.position, windRadius, fwd, windDistance);
        foreach (var collider in windBlastedColliders)
        {
            collider.rigidbody.AddForce(fwd * windForce, ForceMode.Impulse);
        }
        //Destroy(instance, 2.0f); TODO: get self-destruction working
        
    }
}
