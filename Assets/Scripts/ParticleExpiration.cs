using UnityEngine;

public class ParticleExpiration : MonoBehaviour
{
    public float extraTime = 0f;
    
    private ParticleSystem _particles;
    private AudioManager _audioManager;
    
    void Start()
    {
        _particles = gameObject.GetComponent<ParticleSystem>();
        _audioManager = gameObject.GetComponent<AudioManager>();
        var duration = _particles.main.duration;
        var lifetime = _particles.main.startLifetime.constantMax;
        
        if (_audioManager != null) { _audioManager.PlaySound("impact", transform.position); }
        Destroy(gameObject, duration + lifetime + extraTime);
    }
}
