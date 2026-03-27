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
    private Vector3 worldDirection;
    private bool hasDirection = false;

    private HashSet<Collider> enemiesAlreadyHit = new HashSet<Collider>();

    void Start()
    {
        transform.localScale = Vector3.one * initialScale;
        Destroy(gameObject, 3);
    }

    private void Update()
    {
        if (transform.localScale.x < maxScale)
        {
            transform.localScale += Vector3.one * growthSpeed * Time.deltaTime;
        }
        currentDetectionRadius = transform.localScale.x;

        Collider[] enemiesHit = Physics.OverlapSphere(transform.position, currentDetectionRadius, enemyLayer);

        foreach (Collider enemy in enemiesHit)
        {
            if (!enemiesAlreadyHit.Contains(enemy))
            {
                enemiesAlreadyHit.Add(enemy);
                if (enemy.TryGetComponent(out EnemyStatus status))
                {
                    status.ApplyElement("Air", airIcon);
                }
                if (enemy.TryGetComponent(out EnemyKnockback knock))
                {
                    Vector3 pushDir = hasDirection ? worldDirection : transform.forward;
                    knock.Push(pushDir.normalized, force);
                }

            }
        }
    }

    public void SetWorldDirection(Vector3 dir)
    {
        worldDirection = dir.normalized;
        worldDirection.y = 0;
        hasDirection = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, currentDetectionRadius);
    }
}