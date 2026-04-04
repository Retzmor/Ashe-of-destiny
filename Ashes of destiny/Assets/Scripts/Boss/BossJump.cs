using UnityEngine;
using UnityEngine.AI;
public class BossJump : StateMachineBehaviour
{
    private BossController controller;
    private Vector3 startPosition;
    private Vector3 targetPosition;

    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private float arcHeight = 5f; 
    private float progress;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        progress = 0;
        controller = animator.GetComponent<BossController>();
        startPosition = animator.transform.position;
        targetPosition = controller.Player.transform.position;
        controller.Agent.enabled = false; 
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (progress < 1f)
        {
            progress += Time.deltaTime * (jumpSpeed / Vector3.Distance(startPosition, targetPosition));
            Vector3 currentPos = Vector3.Lerp(startPosition, targetPosition, progress);
            currentPos.y += arcHeight * Mathf.Sin(progress * Mathf.PI);

            animator.transform.position = currentPos;
        }
        else
        {
            animator.ResetTrigger("JumpAttack");
            animator.SetTrigger("HitGround");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // animator.ResetTrigger("JumpAttack");
    }

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
