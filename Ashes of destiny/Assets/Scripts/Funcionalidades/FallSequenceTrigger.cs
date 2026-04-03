using UnityEngine;
using Zenject;

public class FallSequenceTrigger : MonoBehaviour
{
    [SerializeField] string fallAnimName; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement movement = other.GetComponent<PlayerMovement>();
            PlayerComponent components = other.GetComponent<PlayerComponent>();
            movement.CanMoving = false;
            movement.StopMovement();
            components.Animator.Play(fallAnimName);
            StartCoroutine(WaitUntilLanded(movement));
        }
    }

    private System.Collections.IEnumerator WaitUntilLanded(PlayerMovement move)
    {
        yield return new WaitForSeconds(1.0f);
        bool landed = false;
        while (!landed)
        {
            landed = Physics.Raycast(move.transform.position, Vector3.down, 0.5f);
            yield return null;
        }
        move.CanMoving = true;
        move.GetComponent<PlayerComponent>().Animator.Play("Idle");
    }
}