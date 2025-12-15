using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerCollisions : MonoBehaviour
{
    [Inject] GameplayUIController gameplayUIController;
    [Inject] Inventory inventory;
    bool _canInteract = false;

    AttackPlayer attackPlayer;
    AbilitiesPlayer abilitiesPlayer;

    private void Start()
    {
        attackPlayer = GetComponent<AttackPlayer>();
        abilitiesPlayer = GetComponent<AbilitiesPlayer>();
    }

    public bool CanInteract { get => _canInteract; set => _canInteract = value; }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ceniza") && _canInteract == true)
        {
            inventory.addItemInventory(collision.gameObject);
            _canInteract = false;
            attackPlayer.CurrentWeapon = collision.gameObject;
            gameplayUIController.UpdateCount();
        }
    }
}
