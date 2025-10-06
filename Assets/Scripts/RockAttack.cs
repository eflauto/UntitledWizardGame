using UnityEngine;

public class RockAttack : MonoBehaviour
{
    public Rigidbody rockObject;
    public float power = 1500f;
    public float moveSpeed = 2f;


    // A method that takes the caller's transform data, instantiates a rock object that flies forward
    public void RockAttackSpell(Transform callerTransform)
    {
        Rigidbody instance = Instantiate(rockObject, callerTransform.position, callerTransform.rotation) as Rigidbody;
        Vector3 fwd = callerTransform.TransformDirection(Vector3.forward);
        instance.AddForce(fwd * power);
        //Destroy(instance, 8.0f); TODO: get self-destruction working
    }
}
