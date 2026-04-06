using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class LoadingLevelOne : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] GameObject loadingUI;
    [SerializeField] CanvasGroup textCanvasGroup;
    [SerializeField] float fadeDuration = 1.5f;
    [SerializeField] MonoBehaviour levelOneLogic;
    [SerializeField] PlayerMovement player;
    [SerializeField] CameraManager cameraManager;
    private static bool _levelOneIntroDone = false;

    // Se ejecuta ANTES que el Start, para asegurar que sea invisible desde el segundo 0
    void Awake()
    {
        if (textCanvasGroup != null) textCanvasGroup.alpha = 0f;
    }

    void Start()
    {
        if (_levelOneIntroDone)
        {
            loadingUI.SetActive(false);
            if (cameraManager != null) cameraManager.CameraLevelOnePlayer();
            if (levelOneLogic != null) levelOneLogic.enabled = true;
            player.CanMoving = true;
            Time.timeScale = 1f;
            return;
        }
        _levelOneIntroDone = true;
        StartCoroutine(PlayLoading());
    }

    IEnumerator PlayLoading()
    {
        Time.timeScale = 0f;
        if (levelOneLogic != null) levelOneLogic.enabled = false;

        loadingUI.SetActive(true);

        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared) yield return null;

        videoPlayer.Play();

        // Pequeña espera de seguridad para que el video no tape el inicio del fade
        yield return new WaitForSecondsRealtime(0.2f);

        // 1. APARECER (Fade In)
        yield return StartCoroutine(FadeCanvasGroup(0, 1, fadeDuration));

        float videoLength = (float)videoPlayer.length;

        // Calculamos cuánto tiempo quedarse totalmente visible
        // Restamos el tiempo que ya pasó (0.2 + fadeDuration) y el tiempo del fade de salida
        float waitTime = videoLength - (fadeDuration * 2) - 0.2f;

        if (waitTime > 0)
        {
            yield return new WaitForSecondsRealtime(waitTime);
        }

        // 2. DESAPARECER (Fade Out)
        yield return StartCoroutine(FadeCanvasGroup(1, 0, fadeDuration));

        cameraManager.CameraLevelOne();
        loadingUI.SetActive(false);
        videoPlayer.Stop();

        Time.timeScale = 1f;
        player.CanMoving = false;
        if (levelOneLogic != null) levelOneLogic.enabled = true;
    }

    private IEnumerator FadeCanvasGroup(float start, float end, float duration)
    {
        if (textCanvasGroup == null) yield break;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            textCanvasGroup.alpha = Mathf.Lerp(start, end, elapsed / duration);
            yield return null;
        }
        textCanvasGroup.alpha = end;
    }
}