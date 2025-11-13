using UnityEngine;
using Zenject;

public class PlayerCollisions : MonoBehaviour
{
    [Inject] GameplayUIController gameplayUIController;
    bool _canInteract = false;

    [SerializeField] AttackPlayer attackPlayer;

    public bool CanInteract { get => _canInteract; set => _canInteract = value; }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ceniza") && _canInteract == true)
        {
            collision.gameObject.TryGetComponent(out Weapon weapon);
            attackPlayer.CurrentWeapon = collision.gameObject;
            collision.gameObject.transform.SetParent(transform, true);
            gameplayUIController.UpdateCount();
        }
    }
}
