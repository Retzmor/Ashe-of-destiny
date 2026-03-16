using UnityEngine;
using Zenject;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [Inject] AudioManager audioManager;

    [SerializeField] AudioClip clickSound;
    Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PlaySound);
    }

    void PlaySound()
    {
        audioManager.PlaySFX(clickSound, 1f);
    }
}
