using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

public class EnemyMovement : Enemies
{
    [SerializeField] List<Transform> patrolPoints;
    int currentPositionPatrol;
    EnemyDetector detector;
    new void Start()
    {
        base.Start();
        currentPositionPatrol = Random.Range(0, patrolPoints.Count);
        Debug.Log(patrolPoints[currentPositionPatrol]);
        TryGetComponent(out detector);
    }

    void Update()
    {
        agent.SetDestination(patrolPoints[currentPositionPatrol].transform.position);

        if(detector.PlayerDetected == true)
        {
            agent.SetDestination(detector.PlayerPosition.transform.position);
        }
    }
}
