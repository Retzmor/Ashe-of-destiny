using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

public class TutorialController : MonoBehaviour
{
    [Inject] TutorialManager manager;
    [SerializeField] PlayerMovement player;
    [SerializeField] CameraSwitcher cameraSwitcher;
    [SerializeField] Weapon[] ashes;
    int countAshe;
    Rigidbody rbPlayer;
    Vector3 positionPlayerStart;

    private void Start()
    {
        player.TryGetComponent<Rigidbody>(out Rigidbody rb);
        rbPlayer = rb;
        positionPlayerStart = player.transform.position;
    }

    public void AshesRecolected()
    {
                
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
}
