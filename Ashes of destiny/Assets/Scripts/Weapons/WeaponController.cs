using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] WorldCrossHairController crosshairController;

    public void Shoot()
    {
        Vector3 direction =
            (crosshairController.CurrentAimPoint - bulletSpawnPoint.position).normalized;

        Quaternion rotation = Quaternion.LookRotation(direction);

        Instantiate(bulletPrefab, bulletSpawnPoint.position, rotation);
    }
}
