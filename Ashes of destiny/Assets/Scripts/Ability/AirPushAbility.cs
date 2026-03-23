using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Unity.VisualScripting;

public class AirPushAbility : MonoBehaviour
{
    public float force;
    public LayerMask enemyLayer;
    [SerializeField] float rotationSpeed = 800f;
    [SerializeField] float initialScale = 0.5f;
    [SerializeField] float maxScale = 3.0f;
    [SerializeField] float growthSpeed = 2.0f;
    [SerializeField]float currentDetectionRadius;
    void Start()
    {
        transform.localScale = Vector3.one * initialScale;
        Destroy(gameObject, 3);
    }
    private void Update()
    {
        // Crecimiento del visual
        if (transform.localScale.x < maxScale)
        {
            transform.localScale += Vector3.one * growthSpeed * Time.deltaTime;
        }
        Collider[] enemiesHit = Physics.OverlapSphere(transform.position, currentDetectionRadius, enemyLayer);
        foreach (Collider enemy in enemiesHit)
        {
            if (enemy.TryGetComponent(out EnemyKnockback knock))
            {
                Vector3 pushDirection = (enemy.transform.position - transform.position).normalized;
                pushDirection.y = 0.1f;
                knock.Push(pushDirection, force);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, currentDetectionRadius);
    }
}
