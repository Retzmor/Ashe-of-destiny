using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] TypeWeapon TypeWeapon;
    bool _isUsed = false;

    public bool IsUsed { get => _isUsed; set => _isUsed = value; }

    public void AsheTaked()
    {
        IsUsed = true;
    }
}
