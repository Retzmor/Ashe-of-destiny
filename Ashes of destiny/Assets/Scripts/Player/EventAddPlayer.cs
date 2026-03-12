using UnityEngine;

public class EventAddPlayer : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] TutorialController tutorialController;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] AttackPlayer playerAttack;
  
    public void EndAnimationTakeAshe()
    {
            playerController.EnableInputs();
            tutorialController.StartPlayer();
    }

    public void JumpEvent()
    {
        playerMovement.ApplyJumpForce();
    }

    public void AttackAnim()
    {
        playerAttack.MeleeHit();
    }
}
