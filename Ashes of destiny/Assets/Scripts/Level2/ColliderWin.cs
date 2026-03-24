using UnityEngine;
using Zenject;

public class ColliderWin : MonoBehaviour
{
    [SerializeField] GameObject panelCreditos;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] CreditsController creditsController;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerMovement.CanMoving = false;
            panelCreditos.SetActive(true);
            creditsController.enabled = true;
        }
    }
}
