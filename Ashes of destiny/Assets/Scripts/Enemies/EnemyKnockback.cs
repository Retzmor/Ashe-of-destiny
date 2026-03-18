using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyKnockback : MonoBehaviour
{
    NavMeshAgent agent;
    EnemyController controller;
    bool isKnocked;
    bool _playerCollision = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        controller = GetComponent<EnemyController>();
    }
    private Coroutine knockbackRoutine;

    public bool PlayerCollision { get => _playerCollision; set => _playerCollision = value; }

    public void Push(Vector3 attackerPosition, float force)
    {
        if (knockbackRoutine != null) StopCoroutine(knockbackRoutine);
        knockbackRoutine = StartCoroutine(Knockback(attackerPosition, force));
    }

    IEnumerator Knockback(Vector3 attackerPosition, float force)
    {
        isKnocked = true;
        controller.enabled = false;
        if (agent.isOnNavMesh) agent.isStopped = true;
        Vector3 dir = (transform.position - attackerPosition).normalized;
        dir.y = 0;

        float timer = 0.2f;
        float startTimer = timer;
        while (timer > 0)
        {
            float currentForce = force * (timer / startTimer);
            if (agent.isOnNavMesh)
                agent.Move(dir * currentForce * Time.deltaTime);

            timer -= Time.deltaTime;
            yield return null;
        }

        if (agent.isOnNavMesh)
        {
            agent.isStopped = false;
            NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 2.0f, NavMesh.AllAreas);
            agent.Warp(hit.position);
        }

        controller.enabled = true;
        isKnocked = false;
        knockbackRoutine = null;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerCollision = true;
            agent.velocity = Vector3.zero;
            if (agent.isOnNavMesh)
            {
                agent.nextPosition = transform.position;
            }
        }

        else
        {
            _playerCollision = false;
        }
    }
}
