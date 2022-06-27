using UnityEngine;

public class AttackAnimation : StateMachineBehaviour
{
    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        animator.SetBool("isAttack", false);
        animator.SetBool("isMoving", true);
    }
}
