using System;
using TMPro;
using UnityEngine;
using System.Collections;

public class SpellPickupScript : MonoBehaviour
{
    private TextMeshProUGUI _newSpellText;
    private bool _triggered = false;

    private void Start()
    {
        _newSpellText = GameObject.Find("UI").transform.Find("NewSpell").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_triggered) return;
        
        if (!other.CompareTag("Player")) return;
        
        _triggered = true;
        MainManager.Instance.unlockedSpellsCount += 1;
        MainManager.Instance.health = MainManager.Instance.maxHealth;
        GameObject.FindAnyObjectByType<HealthSystem>().HealthRestore();
        transform.localScale = Vector3.zero;
        
        StartCoroutine(ChangeTextOpacityCoroutine(1f));
        StartCoroutine(DelayedChangeTextOpacityCoroutine(0f, 4f));
        StartCoroutine(DestroyObjectCoroutine(7f));
    }

    private IEnumerator ChangeTextOpacityCoroutine(float targetOpacity)
    {
        var fadingColor = _newSpellText.color;
        const float endTime = 2f;
        var timer = 0f;

        while (timer < endTime)
        {
            fadingColor.a = Mathf.Lerp(fadingColor.a, targetOpacity, timer / endTime);
            _newSpellText.color = fadingColor;
            timer += Time.deltaTime;
            
            yield return null;
        }
        
        fadingColor.a = targetOpacity;
        _newSpellText.color = fadingColor;
    }

    private IEnumerator DelayedChangeTextOpacityCoroutine(float targetOpacity, float delay)
    {
        var timer = 0f;

        while (timer < delay)
        {
            timer += Time.deltaTime;

            yield return null;
        }
        
        StartCoroutine(ChangeTextOpacityCoroutine(targetOpacity));
    }

    private IEnumerator DestroyObjectCoroutine(float delay)
    {
        var timer = 0f;

        while (timer < delay)
        {
            timer += Time.deltaTime;
            
            yield return null;
        }
        
        Destroy(gameObject);
    }
}
