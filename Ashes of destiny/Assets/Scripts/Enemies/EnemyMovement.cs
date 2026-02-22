using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : Enemies
{
    [SerializeField] List<Transform> _patrolPoints;
    int _currentPositionPatrol;
    EnemyDetector detector;
    [SerializeField] float forceMovementImpact;
    bool isKnockbacking = false;
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
        if (detector.PlayerDetected)
        {
            _anim.SetBool("Follow", true);
        }
        else
        {
            _anim.SetBool("Follow", false);
        }
    }

    public void TakeDamageEffect()
    {
        // aqui iria la animacion de recibir daño
        Agent.isStopped = true;
        Rb.isKinematic = true;
        StartCoroutine(StopEnemy());
    }

    public IEnumerator StopEnemy()
    {
        yield return new WaitForSeconds(0.5f);
        Agent.isStopped = false;
        Rb.isKinematic = false;
        Debug.Log(Agent.isStopped);
    }
}
