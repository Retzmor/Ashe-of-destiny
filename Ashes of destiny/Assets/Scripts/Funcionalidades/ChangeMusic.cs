using UnityEngine;
using Zenject;

public class ChangeMusic : MonoBehaviour
{
    [Inject] AudioManager audioManager;
    [SerializeField] SceneMusic sceneMusic;
    bool musicActive = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !musicActive)
        {
            musicActive = true;
            sceneMusic.MusicCombat();
        }
    }
}
