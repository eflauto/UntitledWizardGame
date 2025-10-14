using UnityEngine;

public class ParticleExpiration : MonoBehaviour
{
    private ParticleSystem _particles;
    
    void Start()
    {
        _particles = gameObject.GetComponent<ParticleSystem>();
        var duration = _particles.main.duration;
        var lifetime = _particles.main.startLifetime.constantMax;
        
        Destroy(gameObject, duration + lifetime);
    }
}
