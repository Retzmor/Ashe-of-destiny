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

        if (PlayerPrefs.HasKey("ResWidth") && PlayerPrefs.HasKey("ResHeight"))
        {
            int width = PlayerPrefs.GetInt("ResWidth");
            int height = PlayerPrefs.GetInt("ResHeight");

            Screen.SetResolution(width, height, Screen.fullScreen);
        }
    }

    public void SetResolution(Resolution resolution)
    {
        PlayerPrefs.SetInt("ResWidth", resolution.width);
        PlayerPrefs.SetInt("ResHeight", resolution.height);

        Screen.SetResolution(
            resolution.width,
            resolution.height,
            Screen.fullScreenMode,
            resolution.refreshRateRatio
        );
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

