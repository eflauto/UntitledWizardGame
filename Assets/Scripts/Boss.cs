using System.Collections;
using TMPro;
using UnityEngine;

public class Boss : EnemyManager
{
    public float winTime = 5f;
    private float _winTimeTimer = 0f;

    public TextMeshProUGUI resultText;
    
    private GameObject _resultsScreen;

    private new void Start()
    {
        base.Start();
        
        _resultsScreen = GameObject.Find("UI").transform.Find("ResultsScreen").gameObject;
    }
    
    private new void Update()
    {
        base.Update();

        if (isDead)
        {
            StartCoroutine(WinCountdownCoroutine());
        }
    }

    private IEnumerator WinCountdownCoroutine()
    {
        while (_winTimeTimer < winTime)
        {
            _winTimeTimer += Time.deltaTime;
            yield return null;
        }
        
        if (_winTimeTimer >= winTime) { SetResultText(); }
    }

    private void SetResultText()
    {
        if (resultText is null) return;
        
        resultText.text = "You win!";
        resultText.colorGradient = new VertexGradient(Color.green, Color.green, new Color(0f, 0.15f, 0f), new Color(0f, 0.15f, 0f));
        MainManager.Instance.PauseGame(_resultsScreen);
    }
}
