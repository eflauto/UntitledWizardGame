using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedSpellHUD : MonoBehaviour
{
    public Sprite[] spellIcons;

    private int _lastSelectedSpell = 0;

    private Image _spellImage;
    private TextMeshProUGUI _manaCost;

    private WandController _wandController;
        
    void Start()
    {
        _spellImage = transform.Find("SpellIcon").GetComponent<Image>();
        _manaCost = transform.Find("ManaCost").GetComponent<TextMeshProUGUI>();
        _wandController = GameObject.Find("Player").transform.Find("Wand Camera").transform.Find("Wand").GetComponent<WandController>();

        UpdateHUD();
    }
    
    void Update()
    {
        if (MainManager.Instance.selectedSpell == _lastSelectedSpell) return;
        
        UpdateHUD();
        _lastSelectedSpell = MainManager.Instance.selectedSpell;
    }

    void UpdateHUD()
    {
        Sprite spellIcon;
        float manaCost;

        if (MainManager.Instance.selectedSpell > MainManager.Instance.unlockedSpellsCount)
        {
            spellIcon = spellIcons[^1];
            manaCost = 0;
        }
        else
        {
            spellIcon = spellIcons[MainManager.Instance.selectedSpell];
            manaCost = _wandController.attackObjects[MainManager.Instance.selectedSpell]
                .GetComponent<Attack>().manaCost;
        }

        _spellImage.sprite = spellIcon;
        _manaCost.text = "MP: " + manaCost;
    }
}
