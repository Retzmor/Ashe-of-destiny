using UnityEngine;
using Zenject;

public class SceneMusic : MonoBehaviour
{
    [SerializeField] AudioClip levelMusic;
    [Inject] AudioManager audioManager;
    void Start()
    {
        audioManager.PlayMusic(levelMusic);
    }
}
