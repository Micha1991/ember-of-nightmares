using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetWeight : StateMachineBehaviour
{
    private float weightReset = 0.5f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, UnityEngine.AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        //weightReset = animator.GetLayerWeight(layerIndex);
        weightReset = 0.5f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, UnityEngine.AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        weightReset -= Time.deltaTime;
        animator.SetLayerWeight(layerIndex, weightReset);
        if(weightReset < 0.3f)
        {
            weightReset = 0f;
            animator.SetLayerWeight(layerIndex, weightReset);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, UnityEngine.AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetLayerWeight(layerIndex, 0f);
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
