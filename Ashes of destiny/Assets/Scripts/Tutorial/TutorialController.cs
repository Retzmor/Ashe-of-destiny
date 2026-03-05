using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

public class TutorialController : MonoBehaviour
{
    [Inject] TutorialManager manager;
    [SerializeField] PlayerMovement player;
    [SerializeField] CameraSwitcher cameraSwitcher;
    [SerializeField] Weapon[] ashes;
    [SerializeField] TextMeshProUGUI textSpace;
    int countAshe;
    Rigidbody rbPlayer;
    Vector3 positionPlayerStart;
    bool _skipTutorial = false;

    public bool SkipTutorial { get => _skipTutorial; set => _skipTutorial = value; }

    private void Start()
    {
        player.TryGetComponent<Rigidbody>(out Rigidbody rb);
        rbPlayer = rb;
        positionPlayerStart = player.transform.position;
    }

    public void AshesRecolected()
    {
        if(SkipTutorial == false)
        manager.TutorialWeapons();        
    }

    public void StopPlayer()
    {
        rbPlayer.isKinematic = true;
        player.CanMoving = false;
        cameraSwitcher.inputAxisController.enabled = false;
    }

    public void StartPlayer()
    {
        rbPlayer.isKinematic = false;
        player.CanMoving = true;
        cameraSwitcher.inputAxisController.enabled = true;
    }

    public void ResetPositionPlayer()
    {
        player.transform.position = positionPlayerStart;
    }

    public void DesactiveTextSpace()
    {
        textSpace.gameObject.SetActive(false);
    }

    public void ActiveTextSpace()
    {
        textSpace.gameObject.SetActive(true);
    }
}
