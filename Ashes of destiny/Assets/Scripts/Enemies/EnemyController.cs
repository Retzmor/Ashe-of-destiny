using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    EnemyDetector detector;
    float originalSpeed;
    bool isStunned;

    [SerializeField] Transform[] patrolPoints;
    int currentPoint;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        detector = GetComponent<EnemyDetector>();
        originalSpeed = agent.speed;
        currentPoint = Random.Range(0, patrolPoints.Length);

        GoToNextPoint();
    }

    void Update()
    {
        if (agent.velocity.sqrMagnitude < 0.1f)
        {
            agent.nextPosition = transform.position;
        }

        if (isStunned) return;

        if (detector.PlayerDetected)
        {
            agent.SetDestination(detector.Player.position);
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (agent.pathPending) return;

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            int newPoint;
            do
            {
                newPoint = Random.Range(0, patrolPoints.Length);
            }
            while (newPoint == currentPoint);
            currentPoint = newPoint;
            agent.SetDestination(patrolPoints[currentPoint].position);
        }
    }


    void GoToNextPoint()
    {
        agent.SetDestination(patrolPoints[currentPoint].position);
    }

    public void ApplySlow(float slowAmount, float duration)
    {
        StartCoroutine(SlowCoroutine(slowAmount, duration));
    }

    IEnumerator SlowCoroutine(float slowAmount, float duration)
    {
        agent.speed = originalSpeed * slowAmount;
        agent.ResetPath();
        yield return new WaitForSeconds(duration);
        agent.speed = originalSpeed;
    }

    public void ApplyStun(float duration)
    {
        StartCoroutine(StunCoroutine(duration));
    }
    IEnumerator StunCoroutine(float duration)
    {
        isStunned = true;
        agent.ResetPath();
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        yield return new WaitForSeconds(duration);
        agent.isStopped = false;
        isStunned = false;
    }
}
