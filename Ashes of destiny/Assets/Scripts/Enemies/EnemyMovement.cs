using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

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
        _currentPositionPatrol = Random.Range(0, _patrolPoints.Count);
        TryGetComponent(out detector);
    }

    void Update()
    {
        if (detector.PlayerDetected == true)
        {
            _anim.SetBool("Follow", true);
        }

        else
        {
            _anim.SetBool("Follow", false);
            _anim.Play("Patrol");
        }
    }
}
