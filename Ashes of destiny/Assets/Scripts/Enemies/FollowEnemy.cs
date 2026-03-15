using UnityEngine;

public class FollowEnemy : StateMachineBehaviour
{
    EnemyMovement movement;
    EnemyDetector detector;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        movement = animator.GetComponent<EnemyMovement>();
        detector = animator.GetComponent<EnemyDetector>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (detector.PlayerDetected && detector.PlayerPosition != null)
        {
            if (!movement.IsKnocked && movement.Agent != null && movement.Agent.enabled)
            {
                movement.Agent.SetDestination(detector.PlayerPosition.transform.position);
            }
        }
        else
        {
            animator.SetBool("Follow", false);
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
