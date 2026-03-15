using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Enemies
{
    [SerializeField] List<Transform> _patrolPoints;
    int _currentPositionPatrol;
    EnemyDetector detector;

    public List<Transform> PatrolPoints { get => _patrolPoints; set => _patrolPoints = value; }
    public int CurrentPositionPatrol { get => _currentPositionPatrol; set => _currentPositionPatrol = value; }

    new void Start()
    {
        base.Start();

        TryGetComponent(out detector);
        _currentPositionPatrol = Random.Range(0, _patrolPoints.Count);
        Agent.speed += Random.Range(-0.5f, 0.5f);
    }

    void Update()
    {
        if (detector.PlayerDetected)
        {
            _anim.SetBool("Follow", true);
        }
        else
        {
            _anim.SetBool("Follow", false);
        }
    }
}
