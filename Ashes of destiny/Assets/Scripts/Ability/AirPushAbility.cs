using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class AirPushAbility : MonoBehaviour
{
    public float radius = 6f;
    public float force = 25f;
    public LayerMask enemyLayer;

    void Start()
    {
        PushEnemies();
        Destroy(gameObject, 0.2f);
    }

    void PushEnemies()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, radius, enemyLayer);

        foreach (Collider enemy in enemies)
        {
            if (enemy.TryGetComponent(out EnemyKnockback knock))
            {
                knock.Push(transform.position, force);
            }
        }
    }
}
