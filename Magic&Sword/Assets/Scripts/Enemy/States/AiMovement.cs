using UnityEngine;

public class AiMovement : IAiState
{
    private Transform _player;
    private Transform _transform;

    private float _turnSmoothVelocity;

    private static readonly int AnimSpeed = Animator.StringToHash("Speed");

    public AiStateId GetId()
    {
        return AiStateId.ChasePlayer;
    }

    public void Enter(AiAgent agent)
    {
        _player = agent.Config.Player;
        _transform = agent.transform;
    }

    public void Update(AiAgent agent)
    {
        if (!agent.startAnimationIsOver) return;

        Rotation(agent);
        Follow(agent);
    }

    public void Exit(AiAgent agent)
    {
        
    }

    private void Rotation(AiAgent agent)
    {
        var subtractVectors = (_player.position - _transform.position).normalized;
        var angle = Quaternion.LookRotation(subtractVectors, Vector3.right).eulerAngles.y;
        var smoothTurn = Mathf.SmoothDampAngle(_transform.eulerAngles.y, angle, ref _turnSmoothVelocity, agent.Config.TurnSmoothTime);
        agent.transform.rotation = Quaternion.Euler(0f, smoothTurn, 0f);
    }

    private void Follow(AiAgent agent)
    {
        var meshSpeed = agent.NavMesh.speed;

        if (agent.NavMesh.stoppingDistance >= Vector3.Distance(_transform.position, _player.position))
            meshSpeed = ChangeSpeed(meshSpeed, 0, agent);
        else
            meshSpeed = ChangeSpeed(meshSpeed, agent.Config.Speed, agent);

        agent.NavMesh.speed = meshSpeed;
        agent.NavMesh.SetDestination(_player.position);

        agent.Animator.SetFloat(AnimSpeed, meshSpeed);
    }

    private float ChangeSpeed(float meshSpeed, float speed, AiAgent agent)
    {
        var meshSpeedReturn = Mathf.Lerp(meshSpeed, speed, Time.deltaTime * agent.Config.SpeedChange);
        return meshSpeedReturn;
    }

}
