using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoLevelChanger : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private string nextSceneName;
    [SerializeField] private float delayAfterVideo = 2.0f;

    void Awake()
    {
        if (videoPlayer == null)
            videoPlayer = GetComponent<VideoPlayer>();
    }

    void OnEnable()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnDisable()
    {
        videoPlayer.loopPointReached -= OnVideoFinished;
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        StartCoroutine(WaitAndChangeScene());
    }

    IEnumerator WaitAndChangeScene()
    {
        // Esperamos los segundos configurados
        yield return new WaitForSeconds(delayAfterVideo);

        // Cargamos la siguiente escena
        SceneManager.LoadScene(nextSceneName);
    }
}
