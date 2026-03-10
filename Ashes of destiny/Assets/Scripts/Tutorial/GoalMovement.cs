using UnityEngine;
using Zenject;
using System.Collections;

public class GoalMovement : MonoBehaviour
{
    [Inject] TutorialManager tutorialManager;
    [SerializeField] PlayerComponent player;
    [SerializeField] PlayerMovement movement;
    bool playerDetected = false;
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if(other.gameObject.CompareTag("Player") && playerDetected == false)
        {
            playerDetected = true;
            movement.StopMovement();
            StartCoroutine(WaitAndActive());
        }
    }
    IEnumerator WaitAndActive()
    {
        yield return new WaitForSeconds(0.2f);
        tutorialManager.TutorialJump();
    }
}
