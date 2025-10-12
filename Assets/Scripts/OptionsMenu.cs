using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Toggle fpsCapToggle;
    public Slider fpsSlider;
    public Toggle invertXToggle;
    public Toggle invertYToggle;
    public Slider sensitivitySlider;
    public Slider volumeSlider;
    
    public void UpdateFPSCap()
    {
        MainManager.Instance.fpsCap = fpsCapToggle.isOn;
    }

    public void UpdateFPS()
    {
        MainManager.Instance.fpsTarget = (int)fpsSlider.value;
        fpsSlider.transform.Find("Value").GetComponent<TextMeshProUGUI>().text = MainManager.Instance.fpsTarget.ToString();
    }

    public void UpdateInvertX()
    {
        MainManager.Instance.invertX = invertXToggle.isOn;
    }

    public void UpdateInvertY()
    {
        MainManager.Instance.invertY = invertYToggle.isOn;
    }

    public void UpdateSensitivity()
    {
        MainManager.Instance.mouseSensitivityX =  sensitivitySlider.value;
        MainManager.Instance.mouseSensitivityY = sensitivitySlider.value;
        sensitivitySlider.transform.Find("Value").GetComponent<TextMeshProUGUI>().text =
            MainManager.Instance.mouseSensitivityX.ToString(".0");
    }

    public void UpdateVolume()
    {
        MainManager.Instance.musicVolume = (int)volumeSlider.value;
        MainManager.Instance.sfxVolume = (int)volumeSlider.value;
        volumeSlider.transform.Find("Value").GetComponent<TextMeshProUGUI>().text = MainManager.Instance.musicVolume.ToString();
    }
}
