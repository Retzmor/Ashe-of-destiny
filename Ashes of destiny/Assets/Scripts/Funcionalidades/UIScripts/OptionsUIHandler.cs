using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class OptionsUIHandler : BaseUIPanel
{
    [Inject] AudioManager audioManager;
    [Inject] DisplaySettingsManager displayManager;
    [SerializeField] Slider sliderMusic;
    [SerializeField] Slider sliderSFX;
    [SerializeField] Slider sliderMaster;
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] TMP_Dropdown qualityDropdown;
    [SerializeField] Toggle fullScreenToggle;

    public override void Start()
    {
        base.Start();

        LoadSliders();
        FullScreenToggle();

        if (resolutionDropdown != null && qualityDropdown != null)
        {
            LoadResolution();
            LoadQualityChange();
        }
    }

    private void OnEnable()
    {
        sliderMusic.value = audioManager.GetMusicVolume();
        sliderSFX.value = audioManager.GetSFXVolume();
        sliderMaster.value = audioManager.GetMasterVolume();

        sliderMusic.onValueChanged.RemoveAllListeners();
        sliderMusic.onValueChanged.AddListener(audioManager.ChangeMusicVolume);

        sliderSFX.onValueChanged.RemoveAllListeners();
        sliderSFX.onValueChanged.AddListener(audioManager.ChangeSFXVolume);

        sliderMaster.onValueChanged.RemoveAllListeners();
        sliderMaster.onValueChanged.AddListener(audioManager.ChangeMasterVolume);
    }



    public void LoadQualityChange()
    {
        qualityDropdown.ClearOptions(); 
        qualityDropdown.AddOptions(displayManager.GetQualityName());

        qualityDropdown.value = displayManager.GetQualityLevel();
        qualityDropdown.RefreshShownValue();

        qualityDropdown.onValueChanged.RemoveAllListeners();
        qualityDropdown.onValueChanged.AddListener(delegate
        {
            displayManager.QualityChange(qualityDropdown.value);
        });
    }

    public void FullScreenToggle()
    {
        fullScreenToggle.isOn = displayManager.GetFullScreen();

        fullScreenToggle.onValueChanged.RemoveAllListeners();
        fullScreenToggle.onValueChanged.AddListener(delegate
        {
            displayManager.SetFullScreen(fullScreenToggle.isOn);
        });
    }

    public void LoadResolution()
    {
        resolutionDropdown.ClearOptions();
        resolutionDropdown.onValueChanged.RemoveAllListeners();

        List<string> resolutions = new();
        int currentResIndex = 0;

        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            var res = Screen.resolutions[i];
            resolutions.Add(resolutionGame(res));

            if (res.width == Screen.currentResolution.width &&
                res.height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }
        resolutionDropdown.AddOptions(resolutions);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.onValueChanged.AddListener(delegate
        {
            displayManager.SetResolution(resolutionDropdown.value);
        });
    }

    public string resolutionGame(Resolution resolution)
    {
        return resolution.width + " x " + resolution.height;
    }

    public void LoadSliders()
    {
        sliderMusic.onValueChanged.AddListener(delegate
        {
            audioManager.ChangeMusicVolume(sliderMusic.value);
        });

        sliderSFX.onValueChanged.AddListener(delegate
        {
            audioManager.ChangeSFXVolume(sliderSFX.value);
        });

        sliderMaster.onValueChanged.AddListener(delegate
        {
            audioManager.ChangeMasterVolume(sliderMaster.value);
        });

        sliderMusic.value = audioManager.GetMusicVolume();
        sliderSFX.value = audioManager.GetSFXVolume();
        sliderMaster.value = audioManager.GetMasterVolume();
    }
}

