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
        if (transform.localScale.x < maxScale)
        {
            transform.localScale += Vector3.one * growthSpeed * Time.deltaTime;
        }
        Collider[] enemiesHit = Physics.OverlapSphere(transform.position, currentDetectionRadius, enemyLayer);
        foreach (Collider enemy in enemiesHit)
        {
            Debug.Log(enemiesHit.Length);
            if (enemy.TryGetComponent(out EnemyKnockback knock))
            {
                Debug.Log("No hay script");
                //knock.Push(transform.position, force * Time.deltaTime);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, currentDetectionRadius);
    }
}
