using UnityEngine;
using Zenject;

public class ColliderWin : MonoBehaviour
{
    [Inject] GameManager gameManager;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            gameManager.StartCredits();
        }
    }
}
