using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AiAgentConfig : ScriptableObject
{
    [SerializeField] [Range(0.0f, 3f)] private float _speed = 3f;
    [SerializeField] private float _speedChange = 10f;
    [SerializeField] [Range(0.0f, 0.3f)] private float _turnSmoothTime = 0.12f;
    [SerializeField] private Transform _player;

    public float Speed => _speed;
    public float SpeedChange => _speedChange;
    public float TurnSmoothTime => _turnSmoothTime;
    public Transform Player => _player;
}
