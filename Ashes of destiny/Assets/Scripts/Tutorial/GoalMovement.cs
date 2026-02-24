using UnityEngine;
using Zenject;
using System.Collections;

public class GoalMovement : MonoBehaviour
{
    [Inject] TutorialManager tutorialManager;
    bool playerDetected = false;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && playerDetected == false)
        {
            playerDetected = true;
            StartCoroutine(WaitAndActive());
           
        }
    }

    IEnumerator WaitAndActive()
    {
        yield return new WaitForSeconds(1f);

        tutorialManager.TutorialJump();
        Debug.Log("Jugador Detectado");
    }
}
