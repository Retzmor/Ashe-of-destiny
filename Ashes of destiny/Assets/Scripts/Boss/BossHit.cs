using FirstGearGames.SmoothCameraShaker;
using UnityEngine;

public class BossHit : StateMachineBehaviour
{
    [SerializeField] private float waitTime = 3f;
    [SerializeField] ShakeData shakeData;
    private float timer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0; 
        RaycastHit hit;
        if (Physics.Raycast(animator.transform.position + Vector3.up, Vector3.down, out hit, 10f))
        {
            animator.transform.position = hit.point;
            CameraShakerHandler.Shake(shakeData);
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;

        if (timer >= waitTime)
        {
            animator.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            animator.SetTrigger("Recovered");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Recovered");
        animator.ResetTrigger("HitGround");
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
