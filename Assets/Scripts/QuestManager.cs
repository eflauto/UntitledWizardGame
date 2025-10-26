using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private int _previousUnlockedSpellsValue = 0;
    
    private TextMeshProUGUI _questGoal;
    
    private void Start()
    {
        _questGoal = GameObject.Find("QuestGoal").GetComponent<TextMeshProUGUI>();
        
        UpdateQuest();
    }
    
    private void FixedUpdate()
    {
        if (MainManager.Instance.unlockedSpellsCount == _previousUnlockedSpellsValue) return;
        
        UpdateQuest();
        _previousUnlockedSpellsValue = MainManager.Instance.unlockedSpellsCount;
    }

    private void UpdateQuest()
    {
        if (MainManager.Instance.unlockedSpellsCount == 3)
        {
            _questGoal.text = "Reach the tower throne";
        }
        else
        {
            var pluralizedSpell = MainManager.Instance.unlockedSpellsCount == 2 ? "spell" : "spells";
            
            _questGoal.text = $"Collect {3 -  MainManager.Instance.unlockedSpellsCount} {pluralizedSpell}";
        }
    }
}
