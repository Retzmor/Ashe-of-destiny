using UnityEngine;

public class BossFollowState : StateMachineBehaviour
{
    BossController controller;
    private float jumpCooldown = 5f;
    private float lastJumpTime;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller = animator.GetComponent<BossController>();
        lastJumpTime = Time.time;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (controller == null || controller.Player == null) return;

        float distance = Vector3.Distance(animator.transform.position, controller.Player.transform.position);

        if (distance <= controller.distanceToBasicAttack)
        {
            animator.SetTrigger("Attack");
        }
        else if (distance >= controller.distanceToJumpAttack && Time.time > lastJumpTime + jumpCooldown)
        {
            if (Random.value < 0.7f)
            {
                animator.SetTrigger("JumpAttack");
                animator.ResetTrigger("HitGround");
                lastJumpTime = Time.time;
                return; 
            }
            else
            {
                lastJumpTime = Time.time - 2f;
            }
        }

        if (controller.Agent.isActiveAndEnabled && controller.Agent.isOnNavMesh)
        {
            controller.Agent.SetDestination(controller.Player.transform.position);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (controller != null && controller.Agent.isActiveAndEnabled && controller.Agent.isOnNavMesh)
        {
            controller.Agent.ResetPath();
        }
    }
}