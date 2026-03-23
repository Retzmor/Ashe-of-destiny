using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class FakeLoadingScreen : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] GameObject loadingUI;
    [SerializeField] MonoBehaviour tutorialManager;
    private static bool _alreadyLoadedOnce = false;
    void Start()
    {
        if (_alreadyLoadedOnce)
        {
            loadingUI.SetActive(false);
            if (tutorialManager != null)
            {
                tutorialManager.enabled = true;
                Debug.Log("Video saltado, tutorial activado directamente.");
            }
            return;
        }

        _alreadyLoadedOnce = true;
        StartCoroutine(PlayLoading());
    }

    IEnumerator PlayLoading()
    {
        Time.timeScale = 0f;
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
        if (tutorialManager != null)
            tutorialManager.enabled = true;
        Debug.Log("Pantalla de carga finalizada, iniciando tutorial.");
    }
}