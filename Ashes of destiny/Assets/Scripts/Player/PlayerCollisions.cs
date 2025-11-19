using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerCollisions : MonoBehaviour
{
    [Inject] GameplayUIController gameplayUIController;
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
            collision.gameObject.TryGetComponent(out Weapon weapon);
            collision.gameObject.TryGetComponent(out Image image);
            Debug.Log(image);    
            abilitiesPlayer.AddAbility(image);
            attackPlayer.CurrentWeapon = collision.gameObject;
            collision.gameObject.transform.SetParent(transform, true);
            gameplayUIController.UpdateCount();
        }
    }
}
