using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifferendStrafe : StateMachineBehaviour
{
    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        animator.SetInteger("StrafeID", Random.Range(0, 2));
    }

}
