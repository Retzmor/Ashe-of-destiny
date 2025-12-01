using UnityEngine;

public class PatrolEnemy : StateMachineBehaviour
{
    EnemyMovement movement;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        movement = animator.GetComponent<EnemyMovement>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (movement.PatrolPoints.Count == 0)
            return;

        movement.Agent.SetDestination(movement.PatrolPoints[movement.CurrentPositionPatrol].position);

        if (!movement.Agent.pathPending && movement.Agent.remainingDistance <= movement.Agent.stoppingDistance)
        {
            movement.CurrentPositionPatrol = Random.Range(0, movement.PatrolPoints.Count);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
