using UnityEngine;

[CreateAssetMenu()]
public class UnitMoveConfig : ScriptableObject
{
    [SerializeField] [Range(0.0f, 3f)] private float _speed = 3f;
    [SerializeField] private float _speedChange = 10f;
    [SerializeField] [Range(0.0f, 0.3f)] private float _turnSmoothTime = 0.12f;

    public float Speed => _speed;
    public float SpeedChange => _speedChange;
    public float TurnSmoothTime => _turnSmoothTime;
}
