using UnityEngine;
using Zenject;

public class EventAddPlayer : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] TutorialController tutorialController;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] AttackPlayer playerAttack;
    [InjectOptional] TutorialManager manager;
  
    public void EndAnimationTakeAshe()
    {
        if (manager == null || !manager.BlockPlayerInput)
        {
            playerController.EnableInputs();
            tutorialController.StartPlayer();
        }
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
