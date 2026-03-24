using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using Zenject;

public class Level1LoadingScreen : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] GameObject loadingUI;
    [SerializeField] MonoBehaviour levelController;
    [SerializeField] AudioClip level1Music;
    [Inject] AudioManager audioManager;

    private static bool _level1AlreadyLoaded = false;

    void Start()
    {
        if (_level1AlreadyLoaded)
        {
            loadingUI.SetActive(false);
            if (levelController != null)
            {
                levelController.enabled = true;
            }
            return;
        }
        _level1AlreadyLoaded = true;
        StartCoroutine(PlayLoadingLevel1());
    }

    IEnumerator PlayLoadingLevel1()
    {
        Time.timeScale = 0f;
        if (levelController != null) levelController.enabled = false;
        loadingUI.SetActive(true);
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }
        videoPlayer.Play();
        yield return new WaitForSecondsRealtime((float)videoPlayer.length);
        loadingUI.SetActive(false);
        videoPlayer.Stop();
        Time.timeScale = 1f;
        if (levelController != null)
            levelController.enabled = true;
    }
}