using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerCollisions : MonoBehaviour
{
    [Inject] GameplayUIController gameplayUIController;
    [Inject] Inventory inventory;
    bool _canInteract = false;
    bool canCollision = true;

    AttackPlayer attackPlayer;
    AbilitiesPlayer abilitiesPlayer;

    private void Start()
    {
        attackPlayer = GetComponent<AttackPlayer>();
        abilitiesPlayer = GetComponent<AbilitiesPlayer>();
    }

    public bool CanInteract { get => _canInteract; set => _canInteract = value; }
    GameObject currentItem;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ceniza"))
        {
            currentItem = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ceniza"))
        {
            currentItem = null;
        }
    }

    public void TryInteract()
    {
        if (currentItem == null) return;

        inventory.addItemInventory(currentItem);
        attackPlayer.CurrentWeapon = currentItem;
        gameplayUIController.UpdateCount();

        currentItem.SetActive(false);
        currentItem = null;
    }

}
