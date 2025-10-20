using UnityEngine;

public class ParticleExpiration : MonoBehaviour
{
    private ParticleSystem _particles;
    private AudioManager _audioManager;
    
    void Start()
    {
        _particles = gameObject.GetComponent<ParticleSystem>();
        _audioManager = gameObject.GetComponent<AudioManager>();
        var duration = _particles.main.duration;
        var lifetime = _particles.main.startLifetime.constantMax;
        
        _audioManager.PlaySound("impact", transform.position);
        Destroy(gameObject, duration + lifetime);
    }
}
