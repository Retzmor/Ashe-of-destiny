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
        LoadResolution();
        LoadSliders();
        FullScreenToggle();
        LoadQualityChange();
    }

    public void LoadQualityChange()
    {
        qualityDropdown.AddOptions(displayManager.GetQualityName());
        qualityDropdown.onValueChanged.AddListener(delegate
        {
            displayManager.QualityChange(qualityDropdown.value);
        });

        qualityDropdown.value = displayManager.GetQualityLevel();
    }

    public void FullScreenToggle()
    {
        fullScreenToggle.onValueChanged.AddListener(delegate
        {
            displayManager.SetFullScreen(fullScreenToggle.isOn);
        });

        fullScreenToggle.isOn = displayManager.GetFullScreen();

    }
    public void LoadResolution()
    {
        List<string> resolutions = new();

        foreach (var item in Screen.resolutions)
        {
            resolutions.Add(resolutionGame(item));
        }
        resolutionDropdown.AddOptions(resolutions);

        resolutionDropdown.onValueChanged.AddListener(delegate
        {
            displayManager.SetResolution(resolutionDropdown.value);
        });
    }
    public string resolutionGame(Resolution resolution)
    {
        return resolution.width + " x " + resolution.height + " rate " + resolution.refreshRateRatio.value.ToString("F0");
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

