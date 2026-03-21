using UnityEngine;
using Zenject;

public class ChangeMusic : MonoBehaviour
{
    [Inject] AudioManager audioManager;
    [SerializeField] SceneMusic sceneMusic;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //sceneMusic.MusicCombat();
        }
    }
}
