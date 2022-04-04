using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public AiStateId InitialState;
    public AiAgentConfig Config;
    [HideInInspector] public AIStateMachine StateMachine;
    [HideInInspector] public NavMeshAgent NavMesh;
    [HideInInspector] public Animator Animator;
    [HideInInspector] public bool startAnimationIsOver;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        NavMesh = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StateMachine = new AIStateMachine(this);
        RegisterState();
        StateMachine.ChangeState(InitialState);
    }

    private void RegisterState()
    {
        StateMachine.RegisterState(new AiMovement());
        StateMachine.RegisterState(new AiAttack());
    }

    private void Update()
    {
        if (NavMesh.stoppingDistance >= Vector3.Distance(transform.position, Config.Player.position))
            StateMachine.ChangeState(AiStateId.Attack);
        else
            StateMachine.ChangeState(AiStateId.ChasePlayer);

        StateMachine.Update();
    }

    private void StartAnimationIsOver()
    {
        startAnimationIsOver = true;
    }
}
