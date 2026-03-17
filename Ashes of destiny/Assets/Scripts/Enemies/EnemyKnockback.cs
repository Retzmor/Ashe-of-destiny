using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyKnockback : MonoBehaviour
{
    NavMeshAgent agent;
    EnemyController controller;
    bool isKnocked;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        controller = GetComponent<EnemyController>();
    }
    public void Push(Vector3 attackerPosition, float force)
    {
        if (isKnocked) return;

        StartCoroutine(Knockback(attackerPosition, force));
    }

    IEnumerator Knockback(Vector3 attackerPosition, float force)
    {
        isKnocked = true;
        controller.enabled = false;
        agent.ResetPath();
        Vector3 dir = transform.position - attackerPosition;
        dir.y = 0;

        if (dir == Vector3.zero)
        {
            dir = transform.forward;
        }
        float timer = 0.25f;
        while (timer > 0)
        {
            agent.Move(dir.normalized * force * Time.deltaTime);
            timer -= Time.deltaTime;
            yield return null;
        }
        if (!agent.isOnNavMesh)
        {
            NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 2.0f, NavMesh.AllAreas);
            agent.Warp(hit.position);
        }
        controller.enabled = true;
        isKnocked = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            agent.velocity = Vector3.zero;
            if (agent.isOnNavMesh)
            {
                agent.nextPosition = transform.position;
            }
        }
    }
}
