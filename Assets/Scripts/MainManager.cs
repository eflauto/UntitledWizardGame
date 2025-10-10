using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    [Header("Gameplay Variables")] 
    public int health = 30;
    public int selectedSpell = 0;

    [Header("Settings")] 
    public bool fpsCap = true;
    public int fpsTarget = 60;
    public int musicVolume = 100;
    public int sfxVolume = 100;
    public float mouseSensitivityX = 3f;
    public float mouseSensitivityY = 3f;
    public bool invertX =  false;
    public bool invertY = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void NewSpellSelected(int spellIndex)
    {
        Instance.selectedSpell = spellIndex;
    }

    public void SetFPSCap(bool setFPSCap)
    {
        Instance.fpsCap = setFPSCap;

        if (Instance.fpsCap)
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = Instance.fpsTarget;
        }
        else
        {
            QualitySettings.vSyncCount = 1;
        }
    }

    public void SetFPSTarget(int target)
    {
        Instance.fpsTarget = target;
        Application.targetFrameRate = Instance.fpsTarget;
    }
}
