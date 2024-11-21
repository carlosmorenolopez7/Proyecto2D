using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public TMP_Dropdown graphicsDropdown;
    public Toggle fullscreenToggle;

    void Start()
    {
        Load();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        Save();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
        Screen.fullScreen = PlayerPrefs.GetInt("fullscreen") == 1;
        int qualityIndex = PlayerPrefs.GetInt("quality");
        QualitySettings.SetQualityLevel(qualityIndex);
        graphicsDropdown.value = qualityIndex;
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
        PlayerPrefs.SetInt("fullscreen", Screen.fullScreen ? 1 : 0);
        PlayerPrefs.SetInt("quality", QualitySettings.GetQualityLevel());
    }
}