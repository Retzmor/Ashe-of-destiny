using UnityEngine;
using Zenject;

public class PlayerAudio : MonoBehaviour
{
    [Inject] AudioManager audioManager;

    [SerializeField] AudioClip footstep;
    [SerializeField] AudioClip[] attack;
    [SerializeField] AudioClip hitEnemy;
    [SerializeField] AudioClip hitWood;
    [SerializeField] AudioClip jump;
    [SerializeField] AudioClip breath;
    [SerializeField] AudioClip[] runStep;
    [SerializeField] AudioClip TakeAshe;
    [SerializeField] AudioClip hitPlayer;

    public void PlayFootstep()
    {
        audioManager.PlaySFX3D(footstep, transform.position);
    }

    public void PlayRunStep()
    {
        audioManager.PlaySFX3D(runStep[Random.Range(0,runStep.Length)], transform.position);
    }

    public void PlayAttack()
    {
        audioManager.PlaySFX(attack[Random.Range(0,attack.Length)], 0.3f);
    }

    public void TakeAshes()
    {
        audioManager.PlaySFX3D(TakeAshe, transform.position);
    }

    public void PlayHitEnemy()
    {
      audioManager.PlaySFX(hitEnemy, 1f);
    }

    public void PlayHitWood()
    {
        audioManager.PlaySFX(hitWood, 1f);
    }

    public void PlayJump()
    {
       audioManager.PlaySFX(jump, 1f);
    }
}
