using UnityEngine;
using Zenject;

public class SceneMusic : MonoBehaviour
{
    [SerializeField] AudioClip levelMusic;
    [SerializeField] AudioClip musicCombat;
    [Inject] AudioManager audioManager;
    private bool isCombatMusicPlaying = false;
    
    void Start()
    {
        audioManager.PlayMusic(levelMusic);
        audioManager.StopLoop(); 
    }

    public void StopMusic()
    {
        audioManager.StopMusic();
    }

    public void MusicCombat()
    {
        if (isCombatMusicPlaying) return;
        audioManager.StopMusic();
        audioManager.PlayMusic(musicCombat);
    }
}
