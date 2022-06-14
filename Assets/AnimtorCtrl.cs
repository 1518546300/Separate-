using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimtorCtrl : StateMachineBehaviour
{
    public string[] clearAtEnter;
    public string[] cleatAtExit;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var signal in clearAtEnter)
        {
            animator.ResetTrigger(signal);//重置trigger
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var signal in clearAtEnter)
        {
            animator.ResetTrigger(signal);//重置trigger
        }
    }
}
