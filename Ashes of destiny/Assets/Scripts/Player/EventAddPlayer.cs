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
        playerMovement.ApplyJumpForce();
    }
}
