using UnityEngine;
using Zenject;

public class EventAddPlayer : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] TutorialController tutorialController;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] AttackPlayer playerAttack;
    [SerializeField] PlayerCollisions playerCollisions;
    [SerializeField] PlayerAudio playerAudio;
    [InjectOptional] TutorialManager manager;

    public void EndAnimationTakeAshe()
    {
        if (playerCollisions != null)
        {
            playerCollisions.EndPickAshAnimation();
        }

        if (manager == null || !manager.BlockPlayerInput)
        {
            if (playerController != null)
            {
                playerController.EnableInputs();
            }
            if (tutorialController != null)
            {
                tutorialController.StartPlayer();
            }
        }
        Debug.Log("Final de animación de ceniza procesado. Manager: " + (manager != null));
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
