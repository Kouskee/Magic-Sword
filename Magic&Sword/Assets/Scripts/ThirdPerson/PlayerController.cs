using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _enemyRoot;

    [Header("Player")] [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] [Range(0.0f, 0.3f)] private float _turnSmoothTime = 0.12f;
    [SerializeField] private float _speedChangeRate = 10.0f;
    [SerializeField] private float _strafeSpeed = 4f;
    [SerializeField] private float _gravity = -15.0f;

    [Space(10)] 
    [SerializeField] private float _durationStrafe = .5f;
    [SerializeField] private float _cooldownStrafe;

    // Rotation
    private float _turnSmoothVelocity;
    private float _targetRotation;
    private float _smoothTurn;
    private bool _canRotate;

    // player
    private float _speed;
    private float _rotationVelocity;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;
    private float _timerStrafe;
    private float _canStrafeAfterTime;
    private bool _canStrafe;

    // animator
    private float _velocity, _velocityY;
    private bool _hasAnimator;
    private static readonly int anim_Speed = Animator.StringToHash("Speed");
    private static readonly int anim_Strafe = Animator.StringToHash("Strafe");

    //common
    private Animator _animator;
    private CharacterController _controller;
    private InputController _input;
    private Transform _player;

    void Start()
    {
        _hasAnimator = TryGetComponent(out _animator);
        _input = GetComponent<InputController>();
        _controller = GetComponent<CharacterController>();
        _player = GetComponent<Transform>();
        DOTween.Init();
    }

    void Update()
    {
        if(_canRotate)
            HandleRotation();
        Gravity();
        Move();
        Strafe();
    }

    private void HandleRotation()
    {
        var subtractVectors = (_enemyRoot.position - transform.position).normalized;
        var angle = Quaternion.LookRotation(subtractVectors, Vector3.right).eulerAngles.y;
        _targetRotation = Mathf.Atan2(_input.move.x, _input.move.y) * Mathf.Rad2Deg + angle;

        _smoothTurn = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _turnSmoothVelocity, _turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, _smoothTurn, 0f);
    }

    private void Move()
    {
        float targetSpeed = _moveSpeed;

        if (_input.move == Vector2.zero) targetSpeed = 0.0f;

        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = 1f;

        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * _speedChangeRate);
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
            _speed = targetSpeed;

        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                         new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

        _velocity = Mathf.Lerp(_velocity, Mathf.Abs(_input.move.magnitude), Time.deltaTime * _speedChangeRate);

        if (_hasAnimator)
            _animator.SetFloat(anim_Speed, _velocity);
    }

    private void Strafe()
    {
        if (_canStrafeAfterTime <= 0)
        {
            if (_input.strafe && _canStrafe)
            {
                _canStrafeAfterTime = _cooldownStrafe;
                _canRotate = false;
            
                Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
                _controller.Move(targetDirection.normalized * (_strafeSpeed * Time.deltaTime) +
                                 new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

                _player.DORotate(new Vector3(0, _player.transform.eulerAngles.y + 360, 0), _durationStrafe, RotateMode.FastBeyond360).SetEase(Ease.Linear);
                _player.DOScale(new Vector3(0.7f, 0.7f, 1), _durationStrafe * 0.5f).SetLoops(2, LoopType.Yoyo);
            
                if (_hasAnimator) _animator.SetBool(anim_Strafe, true);

                _timerStrafe = _durationStrafe;
                _canStrafe = false;
            }
            else if (!_input.strafe)
            {
                if (_timerStrafe <= 0)
                    _canStrafe = true;
            }
        }
        else
        {
            _canStrafeAfterTime -= Time.deltaTime;
        }
        
        if (_timerStrafe > 0)
            _timerStrafe -= Time.deltaTime;
        else
        {
            if (_hasAnimator) _animator.SetBool(anim_Strafe, false);
            _canRotate = true;
        }
    }

    private void Gravity()
    {
        if (_verticalVelocity < _terminalVelocity)
            _verticalVelocity += _gravity * Time.deltaTime;
    }
}