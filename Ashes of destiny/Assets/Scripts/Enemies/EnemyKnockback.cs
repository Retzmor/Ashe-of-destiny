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
        agent.isStopped = true;

        Vector3 dir = transform.position - attackerPosition;
        dir.y = 0;

        float timer = 0.25f;

        while (timer > 0)
        {
            agent.Move(dir.normalized * force * Time.deltaTime);
            timer -= Time.deltaTime;

            yield return null;
        }

        agent.isStopped = false;
        controller.enabled = true;

        isKnocked = false;
    }

}
