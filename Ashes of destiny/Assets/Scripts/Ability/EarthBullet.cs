using UnityEngine;

public class EarthBullet : MonoBehaviour
{
    [SerializeField] float damage = 4f;
    [SerializeField] float stunDuration = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HealthEnemy enemy))
        {
            enemy.TakeDamage(damage);

            if (other.TryGetComponent(out EnemyController movement))
            {
                movement.ApplyStun(stunDuration);
            }
        }

        Destroy(gameObject);
    }
}
