using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class AirPushAbility : MonoBehaviour
{
    public float radius = 6f;
    public float force = 20f;
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
            EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
            Rigidbody rb = enemy.GetComponent<Rigidbody>();

            if (movement != null && rb != null)
            {
                StartCoroutine(Knockback(movement, rb));
            }
        }
    }
    IEnumerator Knockback(EnemyMovement movement, Rigidbody rb)
    {
        movement.Agent.enabled = false;

        rb.isKinematic = false;

        Vector3 dir = rb.transform.position - transform.position;
        dir.y = 0;

        rb.AddForce(dir.normalized * 8f, ForceMode.Impulse);

        yield return new WaitForSeconds(0.35f);

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.isKinematic = true;

        movement.Agent.enabled = true;
        movement.Agent.Warp(rb.transform.position);
        movement.Agent.enabled = true;
    }

}
