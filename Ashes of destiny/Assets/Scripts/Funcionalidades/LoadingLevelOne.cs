using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class LoadingLevelOne : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] GameObject loadingUI;
    [SerializeField] MonoBehaviour levelOneLogic; 
    [SerializeField] PlayerMovement player;
    [SerializeField] CameraManager cameraManager;
    private static bool _levelOneIntroDone = false;

    void Start()
    {
        if (_levelOneIntroDone)
        {
            loadingUI.SetActive(false);
            if (levelOneLogic != null) levelOneLogic.enabled = true;
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
        yield return new WaitForSecondsRealtime((float)videoPlayer.length);
        cameraManager.CameraLevelOne();
        loadingUI.SetActive(false);
        videoPlayer.Stop();
        Time.timeScale = 1f;
        player.CanMoving = false;
        if (levelOneLogic != null) levelOneLogic.enabled = true;
    }
}