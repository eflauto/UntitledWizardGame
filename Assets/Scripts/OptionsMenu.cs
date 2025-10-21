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
        fpsCapToggle.isOn = fpsCap;
    }

    public void UpdateFPS()
    {
        MainManager.Instance.SetFPSTarget((int)fpsSlider.value);
        fpsSlider.transform.Find("Value").GetComponent<TextMeshProUGUI>().text = MainManager.Instance.fpsTarget.ToString();
    }

    private void UpdateFPS(int fpsTarget)
    {
        fpsSlider.value = fpsTarget;
        fpsSlider.transform.Find("Value").GetComponent<TextMeshProUGUI>().text = MainManager.Instance.fpsTarget.ToString();
    }

    public void UpdateInvertX()
    {
        MainManager.Instance.invertX = invertXToggle.isOn;
    }

    private void UpdateInvertX(bool invertX)
    {
        invertXToggle.isOn = invertX;
    }

    public void UpdateInvertY()
    {
        MainManager.Instance.invertY = invertYToggle.isOn;
    }

    private void UpdateInvertY(bool invertY)
    {
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
        sensitivitySlider.value = sensitivity;
        sensitivitySlider.transform.Find("Value").GetComponent<TextMeshProUGUI>().text = sensitivity.ToString(".0");
    }

    public void UpdateVolume()
    {
        MainManager.Instance.musicVolume = (int)volumeSlider.value;
        MainManager.Instance.sfxVolume = (int)volumeSlider.value;
        volumeSlider.transform.Find("Value").GetComponent<TextMeshProUGUI>().text = MainManager.Instance.musicVolume.ToString();
        
        var musicManagers = FindObjectsByType<MusicManager>(FindObjectsSortMode.None);
        var audioManagers = FindObjectsByType<AudioManager>(FindObjectsSortMode.None);
        
        foreach (var musicManager in musicManagers)
        {
            musicManager.SetVolume();
        }

        foreach (var audioManager in audioManagers)
        {
            audioManager.SetVolume();
        }
    }

    private void UpdateVolume(int volume)
    {
        volumeSlider.value = MainManager.Instance.musicVolume;
        volumeSlider.transform.Find("Value").GetComponent<TextMeshProUGUI>().text = volume.ToString();
    }

    private void ReadSettings()
    {
        UpdateFPSCap(MainManager.Instance.fpsCap);
        UpdateFPS(MainManager.Instance.fpsTarget);
        UpdateInvertX(MainManager.Instance.invertX);
        UpdateInvertY(MainManager.Instance.invertY);
        UpdateSensitivity(MainManager.Instance.mouseSensitivityX);
        UpdateVolume(MainManager.Instance.musicVolume);
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
