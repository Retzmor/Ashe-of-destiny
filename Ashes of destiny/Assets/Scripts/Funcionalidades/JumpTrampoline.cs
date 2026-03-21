using UnityEngine;

public class JumpTrampoline : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private ParticleSystem jumpParticles;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                ApplySuperJump(playerMovement);
                //PlayEffects();
            }
        }
    }

    private void ApplySuperJump(PlayerMovement player)
    {
        player.ExternalJump(jumpForce);
    }

    private void PlayEffects()
    {
        if (jumpParticles != null)
        {
            jumpParticles.Play();
        }
    }
}
