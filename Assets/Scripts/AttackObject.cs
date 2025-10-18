using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class AttackObject : Attack
{
    public float objectForce = 1000f;

    public ParticleSystem explodeParticles;
    public Color particleColor = new(0.31f, 0f, 0.48f);

    public float expirationTime = 5f;
    public float scaleDuration = 2f;
    
    public AttackObject()
    {
        attackPower = 3f;
        manaCost = 25f;
    }

    private void Start()
    {
        var particleMain = explodeParticles.main;
        
        particleMain.startColor = particleColor;
        StartCoroutine(ExpirationCoroutine());
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) return;
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
