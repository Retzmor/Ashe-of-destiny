using UnityEngine;
using Zenject;

public class EventAddPlayer : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] TutorialController tutorialController;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] AttackPlayer playerAttack;
    [SerializeField] PlayerAudio playerAudio;
    [InjectOptional] TutorialManager manager;
  
    public void EndAnimationTakeAshe()
    {
        if (manager == null || !manager.BlockPlayerInput)
        {
            playerController.EnableInputs();
            if(tutorialController == null) return;
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

    public void SoundWalk()
    {
        playerAudio.PlayFootstep();
    }

    public void SoundRun()
    {
        playerAudio.PlayRunStep();
    }

    public void SoundTakeAshe()
    {
        playerAudio.TakeAshes();
    }

    public void CanAttack()
    {
        playerAttack.isAttacking();
    }

    public void NotCanAttack()
    {
        playerAttack.IsntAttacking();
    }
}
