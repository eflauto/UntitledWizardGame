using System;
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

    public void Awake()
    {
        ReadSettings();
    }
    
    public void UpdateFPSCap()
    {
        MainManager.Instance.SetFPSCap(fpsCapToggle.isOn);
    }

    private void UpdateFPSCap(bool fpsCap)
    {
        MainManager.Instance.SetFPSCap(fpsCapToggle.isOn);
        fpsCapToggle.isOn = fpsCap;
    }

    public void UpdateFPS()
    {
        MainManager.Instance.SetFPSTarget((int)fpsSlider.value);
        fpsSlider.transform.Find("Value").GetComponent<TextMeshProUGUI>().text = MainManager.Instance.fpsTarget.ToString();
    }

    private void UpdateFPS(int fpsTarget)
    {
        MainManager.Instance.SetFPSTarget(fpsTarget);
        fpsSlider.value = fpsTarget;
        fpsSlider.transform.Find("Value").GetComponent<TextMeshProUGUI>().text = MainManager.Instance.fpsTarget.ToString();
    }

    public void UpdateInvertX()
    {
        MainManager.Instance.invertX = invertXToggle.isOn;
    }

    private void UpdateInvertX(bool invertX)
    {
        MainManager.Instance.invertX = invertX;
        invertXToggle.isOn = invertX;
    }

    public void UpdateInvertY()
    {
        MainManager.Instance.invertY = invertYToggle.isOn;
    }

    private void UpdateInvertY(bool invertY)
    {
        MainManager.Instance.invertY = invertY;
        invertYToggle.isOn = invertY;
    }

    public void UpdateSensitivity()
    {
        MainManager.Instance.mouseSensitivityX =  sensitivitySlider.value;
        MainManager.Instance.mouseSensitivityY = sensitivitySlider.value;
        sensitivitySlider.transform.Find("Value").GetComponent<TextMeshProUGUI>().text =
            MainManager.Instance.mouseSensitivityX.ToString(".0");
    }

    private void UpdateSensitivity(float sensitivity)
    {
        MainManager.Instance.mouseSensitivityX = sensitivity;
        MainManager.Instance.mouseSensitivityY = sensitivity;
        sensitivitySlider.value = sensitivity;
        sensitivitySlider.transform.Find("Value").GetComponent<TextMeshProUGUI>().text = sensitivity.ToString(".0");
    }

    public void UpdateVolume()
    {
        MainManager.Instance.musicVolume = (int)volumeSlider.value;
        MainManager.Instance.sfxVolume = (int)volumeSlider.value;
        volumeSlider.transform.Find("Value").GetComponent<TextMeshProUGUI>().text = MainManager.Instance.musicVolume.ToString();
    }

    private void UpdateVolume(int volume)
    {
        MainManager.Instance.musicVolume = volume;
        MainManager.Instance.sfxVolume = volume;
        volumeSlider.value = MainManager.Instance.musicVolume;
        volumeSlider.transform.Find("Value").GetComponent<TextMeshProUGUI>().text = volume.ToString();
    }

    private void ReadSettings()
    {
        var fpsCap = PlayerPrefs.GetInt("FPSCap", 1);
        var fpsTarget = PlayerPrefs.GetInt("FPSTarget", 60);
        var invertX = PlayerPrefs.GetInt("InvertX", 0);
        var invertY = PlayerPrefs.GetInt("InvertY", 0);
        var sensitivity = PlayerPrefs.GetFloat("Sensitivity", 2.0f);
        var volume = PlayerPrefs.GetInt("Volume", 100);
        
        UpdateFPSCap(Convert.ToBoolean(fpsCap));
        UpdateFPS(fpsTarget);
        UpdateInvertX(Convert.ToBoolean(invertX));
        UpdateInvertY(Convert.ToBoolean(invertY));
        UpdateSensitivity(sensitivity);
        UpdateVolume(volume);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("FPSCap", MainManager.Instance.fpsCap ? 1 : 0);
        PlayerPrefs.SetInt("FPSTarget", MainManager.Instance.fpsTarget);
        PlayerPrefs.SetInt("InvertX", MainManager.Instance.invertX ? 1 : 0);
        PlayerPrefs.SetInt("InvertY", MainManager.Instance.invertY ? 1 : 0);
        PlayerPrefs.SetFloat("Sensitivity", MainManager.Instance.mouseSensitivityX);
        PlayerPrefs.SetInt("Volume", MainManager.Instance.musicVolume);
    }
}
