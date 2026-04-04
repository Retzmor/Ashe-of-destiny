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

    private HashSet<Transform> hitRoots = new HashSet<Transform>();

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

        Collider[] colliders = Physics.OverlapSphere(transform.position, currentDetectionRadius, enemyLayer);

        foreach (Collider col in colliders)
        {
            EnemyKnockback knock = col.GetComponentInParent<EnemyKnockback>();

            if (knock != null && !hitRoots.Contains(knock.transform))
            {
                hitRoots.Add(knock.transform);

                if (knock.TryGetComponent(out EnemyStatus status))
                {
                    status.ApplyElement("Air", airIcon);
                }
                Vector3 pushDir = hasDirection ? worldDirection : transform.forward;
                knock.Push(pushDir.normalized, force);
            }

            if (col.CompareTag("Wood"))
            {
                if (col.TryGetComponent(out WoodCollision wood))
                    wood.AnimationWoodBroke();
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