using System.Collections;
using UnityEngine;

public class SlimeBall : MonoBehaviour
{
    public ParticleSystem explodeParticles;
    public Color particleColor = new(0.74f, 0.91f, 0.33f);

    public float attackPower = 3f;
    
    public float expirationTime = 5f;
    public float scaleDuration = 2f;
    
    void Start()
    {
        var particleMain = explodeParticles.main;

        particleMain.startColor = particleColor;
        StartCoroutine(ExpirationCoroutine());
    }

    private void OnCollisionEnter(Collision other)
    {
        Instantiate(explodeParticles, other.GetContact(0).point, Quaternion.identity);
        Destroy(gameObject);
    }

    private IEnumerator ExpirationCoroutine()
    {
        var timer = 0f;

        while (timer < expirationTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(ScaleToZeroCoroutine());
    }
    
    private IEnumerator ScaleToZeroCoroutine()
    {
        var timer = 0f;
        var initialScale = transform.localScale;
        var targetScale = Vector3.zero;

        while (timer < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(initialScale, targetScale, timer / scaleDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        
        transform.localScale = targetScale;
        Destroy(gameObject);
    }
}
