using UnityEngine;

public class EventAddPlayer : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] TutorialController tutorialController;
    [SerializeField] PlayerMovement playerMovement;
  
    public void EndAnimationTakeAshe()
    {
        playerController.EnableInputs();
        tutorialController.StartPlayer();
    }

    public void JumpEvent()
    {
        if (!playerMovement.IsJumping) return;
        if (!playerMovement.IsGrounded()) return;
        playerMovement.ApplyJumpForce();
    }
}
