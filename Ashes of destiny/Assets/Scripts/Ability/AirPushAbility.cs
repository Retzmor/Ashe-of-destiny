using UnityEngine;
using System.Collections.Generic; 

public class AirPushAbility : MonoBehaviour
{
    public float force;
    public LayerMask enemyLayer;
    [SerializeField] Sprite airIcon;
    [SerializeField] float initialScale = 0.5f;
    [SerializeField] float maxScale = 3.0f;
    [SerializeField] float growthSpeed = 2.0f;
    [SerializeField] float currentDetectionRadius;

    private List<Collider> enemiesAlreadyHit = new List<Collider>();

    void Start()
    {
        transform.localScale = Vector3.one * initialScale;
        Destroy(gameObject, 3);
    }

    private void Update()
    {
        // 1. Crecimiento visual
        if (transform.localScale.x < maxScale)
        {
            transform.localScale += Vector3.one * growthSpeed * Time.deltaTime;
        }

        // 2. Detección de enemigos
        Collider[] enemiesHit = Physics.OverlapSphere(transform.position, currentDetectionRadius, enemyLayer);

        foreach (Collider enemy in enemiesHit)
        {
            if (!enemiesAlreadyHit.Contains(enemy))
            {
                if (enemy.TryGetComponent(out EnemyStatus status))
                {
                    status.ApplyElement("Air", airIcon);
                }
                if (enemy.TryGetComponent(out EnemyKnockback knock))
                {
                    Vector3 pushDirection = (enemy.transform.position - transform.position).normalized;
                    pushDirection.y = 0.1f;
                    knock.Push(pushDirection, force);
                }

                enemiesAlreadyHit.Add(enemy);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, currentDetectionRadius);
    }
}