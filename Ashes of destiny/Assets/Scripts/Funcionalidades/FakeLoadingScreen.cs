using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class FakeLoadingScreen : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] GameObject loadingUI;
    [SerializeField] CanvasGroup textCanvasGroup; // Arrastra el Canvas Group del texto aquí
    [SerializeField] float fadeDuration = 1.5f; // Tiempo de desvanecimiento
    [SerializeField] MonoBehaviour tutorialManager;
    [SerializeField] GameObject imageBlack;

    private static bool _alreadyLoadedOnce = false;

    void Start()
    {
        if (_alreadyLoadedOnce)
        {
            loadingUI.SetActive(false);
            if (imageBlack != null) imageBlack.SetActive(false);
            if (tutorialManager != null) tutorialManager.enabled = true;
            return;
        }

        _alreadyLoadedOnce = true;
        StartCoroutine(PlayLoading());
    }

    IEnumerator PlayLoading()
    {
        Time.timeScale = 0f;
        loadingUI.SetActive(true);

        if (textCanvasGroup != null) textCanvasGroup.alpha = 0f;

        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        videoPlayer.Play();
        yield return new WaitForSecondsRealtime(0.1f);

        if (imageBlack != null) imageBlack.SetActive(false);
        StartCoroutine(FadeCanvasGroup(0, 1, fadeDuration));

        float videoLength = (float)videoPlayer.length;
        float waitTimeBeforeFadeOut = videoLength - fadeDuration;

        yield return new WaitForSecondsRealtime(waitTimeBeforeFadeOut);

        yield return StartCoroutine(FadeCanvasGroup(1, 0, fadeDuration));

        loadingUI.SetActive(false);
        videoPlayer.Stop();
        Time.timeScale = 1f;

        if (tutorialManager != null)
            tutorialManager.enabled = true;
    }

    private IEnumerator FadeCanvasGroup(float start, float end, float duration)
    {
        if (textCanvasGroup == null) yield break;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime; // Crucial para que se mueva durante la pausa
            textCanvasGroup.alpha = Mathf.Lerp(start, end, elapsed / duration);
            yield return null;
        }
        textCanvasGroup.alpha = end;
    }
}