using UnityEngine;
using Zenject;

public class SceneMusic : MonoBehaviour
{
    [SerializeField] AudioClip levelMusic;
    [SerializeField] AudioClip musicCombat;
    [Inject] AudioManager audioManager;
    void Start()
    {
        audioManager.PlayMusic(levelMusic);
    }

    public void StopMusic()
    {
        audioManager.StopMusic();
    }

    public void MusicCombat()
    {
        audioManager.StopMusic();
        audioManager.PlayLoop(musicCombat);
    }
}
