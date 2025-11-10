using UnityEngine;

public class PatrolEnemy : StateMachineBehaviour
{
    EnemyMovement anim;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        anim = animator.GetComponent<EnemyMovement>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        anim.Agent.SetDestination(anim.PatrolPoints[anim.CurrentPositionPatrol].transform.position);

        if(!anim.Agent.pathPending && anim.Agent.remainingDistance <= anim.Agent.stoppingDistance)
        {
            anim.CurrentPositionPatrol = Random.Range(0, anim.PatrolPoints.Count);
            anim.Agent.SetDestination(anim.PatrolPoints[anim.CurrentPositionPatrol].transform.position);
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
