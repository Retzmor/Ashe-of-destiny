using UnityEngine;
using Zenject;

public class GoalJump : MonoBehaviour
{
    [Inject] TutorialManager tutorialManager;
    bool playerDetected = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerDetected == false)
        {
            playerDetected = true;
            tutorialManager.TutorialAshes();
        }
    }
}

