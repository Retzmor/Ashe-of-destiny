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
    [SerializeField] float combatStoppingDistance = 0.5f;
    [SerializeField] float patrolStoppingDistance = 0.5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        detector = GetComponent<EnemyDetector>();
        originalSpeed = agent.speed;
        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            agent.enabled = true;

            Invoke(nameof(GoToNextPoint), 0.1f);
        }
    }

    void Update()
    {
        if (agent == null || !agent.enabled || !agent.isOnNavMesh) return;
        if (isStunned) return;

        if (detector.PlayerDetected)
        {
            agent.stoppingDistance = combatStoppingDistance;
            agent.SetDestination(detector.Player.position);
            agent.speed = 5;
        }
        else
        {
            agent.stoppingDistance = patrolStoppingDistance;
            //agent.speed = 3.5f;
            Patrol();
        }
    }

    void Patrol()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            agent.destination = transform.position; 
            return;
        }
        if (agent == null || !agent.enabled || !agent.isOnNavMesh) return;
        if (agent.pathPending) return;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
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
        if (!gameObject.activeInHierarchy) return;
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
        if (!gameObject.activeInHierarchy) return;
        StartCoroutine(StunCoroutine(duration));
    }
    public void SetPatrolPoints(Transform[] newPoints)
    {
        patrolPoints = newPoints;
        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            currentPoint = Random.Range(0, patrolPoints.Length);

            if (agent != null && agent.enabled && agent.isOnNavMesh)
            {
                agent.SetDestination(patrolPoints[currentPoint].position);
            }
        }
    }
    IEnumerator StunCoroutine(float duration)
    {
        isStunned = true;
        if (agent != null && agent.isActiveAndEnabled && agent.isOnNavMesh)
        {
            agent.isStopped = true;
            agent.ResetPath();
            agent.velocity = Vector3.zero;
        }
        yield return new WaitForSeconds(duration);
        if (agent != null && agent.isActiveAndEnabled && agent.isOnNavMesh)
        {
            agent.isStopped = false;
        }
        isStunned = false;
    }
}
