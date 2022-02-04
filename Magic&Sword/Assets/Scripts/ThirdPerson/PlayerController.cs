using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _enemyRoot;
    
    [Header("Player")] 
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] [Range(0.0f, 0.3f)] private float _turnSmoothTime = 0.12f;
    [SerializeField] private float _speedChangeRate = 10.0f;
    [SerializeField] private float _jumpHeight = 1f;
    [SerializeField]  private float _gravity = -15.0f;
    
    [Header("Player Grounded")] 
    [SerializeField] private bool _grounded = true;
    [SerializeField] private float _groundedOffset = -0.14f;
    [SerializeField] private float _groundedRadius = 0.28f;
    [SerializeField] private LayerMask _groundLayers;

    // Rotation
    private float _turnSmoothVelocity;
    private float _targetRotation;
    private float _targetRotationMove;
    private float _smoothTurn;

    // player
    private float _speed;
    private float _rotationVelocity;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;
    
    // animator
    private float _velocityX, _velocityY;
    private bool _hasAnimator;
    private static readonly int Grounded = Animator.StringToHash("Grounded");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Jump = Animator.StringToHash("Jump");
    
    private Animator _animator;
    private CharacterController _controller;
    private InputController _input;

    void Start()
    {
        _hasAnimator = TryGetComponent(out _animator);
        _input = GetComponent<InputController>();
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //_hasAnimator = TryGetComponent(out _animator);

        HandleRotation();
        JumpAndGravity();
        GroundedCheck();
        Move();
    }

    private void GroundedCheck()
    {
        var position = transform.position;
        Vector3 spherePosition = new Vector3(position.x, position.y - _groundedOffset, position.z);
        _grounded = Physics.CheckSphere(spherePosition, _groundedRadius, _groundLayers, QueryTriggerInteraction.Ignore);

        if (_hasAnimator)
            _animator.SetBool(Grounded, _grounded);
    }

    private void HandleRotation()
    {
        var subtractVectors = (_enemyRoot.position - transform.position).normalized;
        var angle = Quaternion.LookRotation(subtractVectors, Vector3.right).eulerAngles.y;
        
        _targetRotation = Mathf.Atan2(_input.move.x, Math.Abs(_input.move.y)) * Mathf.Rad2Deg + angle;
        _targetRotationMove = Mathf.Atan2(_input.move.x, _input.move.y) * Mathf.Rad2Deg + angle;

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
        {
            _speed = targetSpeed;
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotationMove, 0.0f) * Vector3.forward;
        
        _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
        
        _velocityX = MLerp(_velocityX, _input.move.x);
        _velocityY = MLerp(_velocityY, _input.move.y);

        if (_hasAnimator)
        {
            _animator.SetFloat(Vertical, _velocityY);
            _animator.SetFloat(Horizontal, _velocityX);
        }
    }

    private float MLerp(float velocity, float input)
    {
        return Mathf.Lerp(velocity, input, Time.deltaTime * _speedChangeRate);
    }

    private void JumpAndGravity()
    {
        if (_grounded)
        {
            if (_hasAnimator)
                _animator.SetBool(Jump, false);
            if (_verticalVelocity < 0.0f)
                _verticalVelocity = -2f;
            
            if (_input.jump)
            {
                _verticalVelocity = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
                
                if (_hasAnimator)
                    _animator.SetBool(Jump, true);
            }
        }
        else
        {
            _input.jump = false;
        }

        if (_verticalVelocity < _terminalVelocity)
            _verticalVelocity += _gravity * Time.deltaTime;
    }
}
