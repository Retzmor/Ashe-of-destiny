using System.Collections;
using UnityEngine;

public class JumpTrampoline : MonoBehaviour
{
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private string animationName = "CaidaNivel";
    [SerializeField] private Transform targetLandingPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement pm = other.GetComponent<PlayerMovement>();
            PlayerComponent pc = other.GetComponent<PlayerComponent>();

            if (pm != null && pc != null && targetLandingPoint != null)
            {
                StopAllCoroutines();
                StartCoroutine(PrecisionJumpSequence(pm, pc));
            }
        }
    }
    private IEnumerator PrecisionJumpSequence(PlayerMovement player, PlayerComponent component)
    {
        player.IsTrampolineJumping = true;
        player.CanMoving = false;
        component.Animator.Play(animationName);
        float gravity = Mathf.Abs(Physics.gravity.y);
        float timeToLand = (2f * jumpForce) / gravity;

        Vector3 startPos = player.transform.position;
        Vector3 targetPos = targetLandingPoint.position;
        Vector3 diff = targetPos - startPos;
        diff.y = 0;
        Vector3 horizontalVelocity = diff / timeToLand;

        player.Rb.linearVelocity = new Vector3(horizontalVelocity.x, jumpForce, horizontalVelocity.z);

        float elapsed = 0;
        while (true)
        {
            elapsed += Time.deltaTime;
            if (elapsed > 0.3f && player.Rb.linearVelocity.y <= 0.1f && player.IsGrounded())
            {
                break;
            }

            yield return null;
        }


        float adjustTime = 0.1f; 
        float currentTime = 0;
        Vector3 landPos = player.transform.position;
        Vector3 finalPos = new Vector3(targetPos.x, player.transform.position.y, targetPos.z);

        while (currentTime < adjustTime)
        {
            currentTime += Time.deltaTime;
            player.transform.position = Vector3.Lerp(landPos, finalPos, currentTime / adjustTime);
            yield return null;
        }
        player.IsTrampolineJumping = false;
        player.CanMoving = true;
        player.Rb.linearVelocity = Vector3.zero;
        component.Animator.CrossFade("Idle", 0.1f);
        player.StopMovement();
    }
}