using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    EnemyDetector detector;

    [SerializeField] Transform[] patrolPoints;

    int currentPoint;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        detector = GetComponent<EnemyDetector>();

        currentPoint = Random.Range(0, patrolPoints.Length);

        GoToNextPoint();
    }

    void Update()
    {
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
}
