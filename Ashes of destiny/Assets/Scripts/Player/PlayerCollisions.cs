using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    bool _canInteract = false;

    [SerializeField] AttackPlayer attackPlayer;

    public bool CanInteract { get => _canInteract; set => _canInteract = value; }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Weapon") && _canInteract == true)
        {
            collision.gameObject.TryGetComponent(out Weapon weapon);
            attackPlayer.CurrentWeapon = collision.gameObject;
            collision.gameObject.transform.SetParent(transform, true);
        }
    }
}
