using UnityEngine;
using Zenject;

public class ChangeMusic : MonoBehaviour
{
    [Inject] AudioManager audioManager;
    [SerializeField] AudioClip musicFight;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            audioManager.StopMusic();
            audioManager.StopLoop();
            audioManager.PlayLoop(musicFight);
        }
    }
}
