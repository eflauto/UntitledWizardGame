using UnityEngine;

public class RockAttack : Attack
{
    public Rigidbody rockObject;
    public ParticleSystem explosionParticles;
    public Color particleColor = new Color(0.42f, 0.26f, 0.26f);
    public float power = 1500f;
    public float moveSpeed = 2f;

    public RockAttack()
    {
        attackPower = 5f;
    }

    public void Start()
    {
        var particleMain = explosionParticles.main;
        particleMain.startColor = particleColor;
    }

    // A method that takes the caller's transform data, instantiates a rock object that flies forward
    public void RockAttackSpell(Transform callerTransform)
    {
        Rigidbody instance = Instantiate(rockObject, callerTransform.position, callerTransform.rotation) as Rigidbody;
        Vector3 fwd = callerTransform.TransformDirection(Vector3.forward);
        instance.AddForce(fwd * power);
        //Destroy(instance, 8.0f); TODO: get self-destruction working
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player")) return;
        
        Instantiate(explosionParticles, collision.GetContact(0).point, Quaternion.identity);
        Destroy(gameObject);
    }
}
