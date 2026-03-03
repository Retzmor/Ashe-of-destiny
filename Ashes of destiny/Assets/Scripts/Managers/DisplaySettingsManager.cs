using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DisplaySettingsManager : MonoBehaviour
{
    int qualityLevel;
    string qualityKey = "QualityLevel";

    private void Awake()
    {
        QualityChange(PlayerPrefs.HasKey(qualityKey) ? PlayerPrefs.GetInt(qualityKey) : 0);
        //SetResolution(PlayerPrefs.HasKey("ResRate") ? PlayerPrefs.GetInt("ResRate") : 0);
    }

    public void SetResolution(int index)
    {
        Resolution selectedResolution = Screen.resolutions[index];
        PlayerPrefs.SetInt("ResRate", (int)selectedResolution.refreshRateRatio.numerator);
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
    }

    public void QualityChange(int quality)
    {
        qualityLevel = quality;
        PlayerPrefs.SetInt("QualityLevel", quality);
        QualitySettings.SetQualityLevel(quality);
    }

    public int GetQualityLevel()
    {
        return qualityLevel;
    }

    public List<string> GetQualityName()
    {
        return QualitySettings.names.ToList();
    }

    public void SetFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    public bool GetFullScreen()
    {
        return Screen.fullScreen;
    }
}
