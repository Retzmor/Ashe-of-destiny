using UnityEngine;

public class WaterAbility : MonoBehaviour
{
    [SerializeField] float slowAmount = 0.5f;
    [SerializeField] float slowDuration = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HealthEnemy enemy))
        {
            enemy.TakeDamage(2);

            if (other.TryGetComponent(out EnemyController movement))
            {
                movement.ApplySlow(slowAmount, slowDuration);
            }
        }

        Destroy(gameObject);
    }
}
