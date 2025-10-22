using System;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Worthy : MonoBehaviour
{
    private GameObject _worthy;
    private Image _worthyImage;
    private TextMeshProUGUI _worthyText;
    
    private GameObject _resultsScreen;
    private TextMeshProUGUI _resultText;

    public float worthyFadeTime = 3f;
    public float winTime = 2f;
    
    private bool _worthyFade = false;

    private void Start()
    {
        _worthy = GameObject.Find("UI").transform.Find("Worthy").gameObject;
        _worthyImage = _worthy.transform.Find("Image").GetComponent<Image>();
        _worthyText = _worthy.transform.Find("Worthy Text").GetComponent<TextMeshProUGUI>();
        _resultsScreen = GameObject.Find("UI").transform.Find("ResultsScreen").gameObject;
        _resultText = _resultsScreen.transform.Find("Result Text").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_worthyFade) return;

        if (!other.CompareTag("Player")) return;
        
        if (MainManager.Instance.unlockedSpellsCount != 3)
        {
            _worthyFade = true;
            _worthy.SetActive(true);
            StartCoroutine(WorthyFadeInCoroutine());
        }
        else
        {
            StartCoroutine(WinCoroutine());
        }
    }

    private IEnumerator WorthyFadeInCoroutine()
    {
        var timer = 0f;
        var imageColor = _worthyImage.color;
        var textColor = _worthyText.color;

        while (timer < worthyFadeTime)
        {
            var newOpacity = timer / worthyFadeTime;
            
            imageColor.a = newOpacity;
            textColor.a = newOpacity;
            _worthyImage.color = imageColor;
            _worthyText.color = textColor;
            timer += Time.deltaTime;
            yield return null;
        }
        
        _worthy.SetActive(false);
        MainManager.Instance.SetWaypoint("ForestSpawn");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private IEnumerator WinCoroutine()
    {
        var timer = 0f;

        while (timer < winTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        SetResultText();
    }

    private void SetResultText()
    {
        if (_resultText is null) return;
        
        _resultText.text = "You win!";
        _resultText.colorGradient = new VertexGradient(Color.green, Color.green, new Color(0f, 0.15f, 0f), new Color(0f, 0.15f, 0f));
        MainManager.Instance.PauseGame(_resultsScreen);
    }
}
