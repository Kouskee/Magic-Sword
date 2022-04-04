using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateMachine
{
    private IAiState[] _states;
    private AiAgent _agent;
    private AiStateId _currentState;

    public AIStateMachine(AiAgent agent)
    {
        _agent = agent;
        var numStates = System.Enum.GetNames(typeof(AiStateId)).Length;
        _states = new IAiState[numStates];
    }

    public void RegisterState(IAiState state)
    {
        var index = (int)state.GetId();
        _states[index] = state;
    }

    public IAiState GetState(AiStateId stateId)
    {
        var index = (int)stateId;
        return _states[index];
    }

    public void Update()
    {
        GetState(_currentState)?.Update(_agent);
    }

    public void ChangeState(AiStateId newState)
    {
        GetState(_currentState)?.Exit(_agent);
        _currentState = newState;
        GetState(_currentState)?.Enter(_agent);
    }
}
