using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAttack : IAiState
{
    private static readonly int AnimDifAttack = Animator.StringToHash("DifferentAttack");
    private static readonly int AnimAttack = Animator.StringToHash("IsAttack");

    public AiStateId GetId()
    {
        return AiStateId.Attack;
    }

    public void Enter(AiAgent agent)
    {
        agent.Animator.SetBool(AnimAttack, true);
    }

    public void Update(AiAgent agent)
    {
        
    }
    public void Exit(AiAgent agent)
    {
        agent.Animator.SetBool(AnimAttack, false);
    }

}
