using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifferentCast : StateMachineBehaviour
{
    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        animator.SetBool("Cast", false);
    }
}
