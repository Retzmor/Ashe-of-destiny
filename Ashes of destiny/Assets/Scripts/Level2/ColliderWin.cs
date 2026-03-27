using System.Collections;
using UnityEngine;
using Zenject;

public class ColliderWin : MonoBehaviour
{
    [SerializeField] GameObject panelCreditos;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] CreditsController creditsController;
    [SerializeField] GameObject panelContinue;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerMovement.CanMoving = false;
            panelContinue.SetActive(true);
            creditsController.enabled = true;
            StartCoroutine(WaitForCreditsPanel());
        }
    }

    IEnumerator WaitForCreditsPanel()
    {
        yield return new WaitForSeconds(5f);
        panelContinue.SetActive(false);
        panelCreditos.SetActive(true);
    }
}
