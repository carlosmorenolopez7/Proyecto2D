using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    void Start()
    {
        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 1f);
        }

        if (!PlayerPrefs.HasKey("fullscreen"))
        {
            PlayerPrefs.SetInt("fullscreen", Screen.fullScreen ? 1 : 0);
        }

        if (!PlayerPrefs.HasKey("quality"))
        {
            PlayerPrefs.SetInt("quality", QualitySettings.GetQualityLevel());
        }

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
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("quality"));
    }

    private void Save() 
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
        PlayerPrefs.SetInt("fullscreen", Screen.fullScreen ? 1 : 0);
        PlayerPrefs.SetInt("quality", QualitySettings.GetQualityLevel());
    }
}