using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class CreditsScript : MonoBehaviour
{
    private Button _nextButton;
    private Button _prevButton;

    private List<TextMeshProUGUI> _creditsPages = new();
    private int _pageIndex = 0;
    
    private void Awake()
    {
        _nextButton = GameObject.Find("Next Button").GetComponent<Button>();
        _prevButton = GameObject.Find("Prev Button").GetComponent<Button>();
        
        foreach (var textObject in FindObjectsByType<TextMeshProUGUI>(FindObjectsSortMode.None))
        {
            if (!textObject.name.Contains("Credits Page")) continue;
            
            _creditsPages.Add(textObject);
            textObject.gameObject.SetActive(false);
        }
        
        _creditsPages = _creditsPages.Select(s => new { TMP = s, Str = s.name, Split = s.name.Split(' ') })
            .OrderByDescending(x => int.Parse(x.Split[2]))
            .Select(x => x.TMP)
            .ToList();

        _creditsPages.Reverse();
        _creditsPages[_pageIndex].gameObject.SetActive(true);
        _prevButton.interactable = false;
    }

    public void NextPage()
    {
        if (_pageIndex == _creditsPages.Count - 1) return;
        
        _creditsPages[_pageIndex].gameObject.SetActive(false);
        _pageIndex++;
        _creditsPages[_pageIndex].gameObject.SetActive(true);

        if (_pageIndex == _creditsPages.Count - 1)
        {
            _nextButton.interactable = false;
        }

        if (_pageIndex != 0 && !_prevButton.interactable)
        {
            _prevButton.interactable = true;
        }
    }

    public void PrevPage()
    {
        if (_pageIndex == 0) return;
        
        _creditsPages[_pageIndex].gameObject.SetActive(false);
        _pageIndex--;
        _creditsPages[_pageIndex].gameObject.SetActive(true);

        if (_pageIndex == 0)
        {
            _prevButton.interactable = false;
        }

        if (_pageIndex != _creditsPages.Count - 1 && !_nextButton.interactable)
        {
            _nextButton.interactable = true;
        }
    }
}
