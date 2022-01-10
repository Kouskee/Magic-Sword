using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonMovement : MonoBehaviour
{
    [Header("Main settings")]
    [SerializeField]
    private CharacterController Controller;
    [SerializeField]
    private Transform MainCamera;

    [Header("Movement")]
    private float _speedWalk = 6f;
    readonly float _minSpeedW = 4f;
    private float _speedRun = 8f;
    private float _maxSpeedRun = 11f;
    readonly float _minSpeedR = 3f;
    private float _slowDowns = 0.5f;
    float _defaultSpeedW, _defaultSpeedR;
    float _targetAnlge, _smoothTurn;
    Vector3 _moveDir;

    Vector2 _currentMovementInput;
    Vector3 _currentMovement;
    bool _isMovementPressed, _isRunPressed, _isJumpPressed;

    [Header("Jump settings")]
    private float _jumpHeight = 1f;
    private float _gravityValue = -9.81f;
    bool _isFalling = false, _isFlying = false;

    [Header("Smooth camera")]
    private float _turnSmoothTime = 0.05f;
    float _turnSmoothVelocity;

    [Header("Is grounded check")]
    [SerializeField]
    private Transform GroundCheck;
    private bool _isGroundedCheck;
    [SerializeField]
    private LayerMask GroundMask;
    private float _groundDist = 0.1f;

    private void Awake()
    {
        _defaultSpeedW = _speedWalk;
        _defaultSpeedR = _speedRun;
    }

    public void OnMovementInput(InputAction.CallbackContext obj)
    {
        _currentMovementInput = obj.ReadValue<Vector2>().normalized;
        _currentMovement.x = _currentMovementInput.x;
        _currentMovement.z = _currentMovementInput.y;

        _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
    }
    public void OnRun(InputAction.CallbackContext obj)
    {
        _isRunPressed = obj.ReadValueAsButton();
    }
    public void OnJump(InputAction.CallbackContext obj)
    {
        _isJumpPressed = obj.ReadValueAsButton();
    }

    void Update()
    {
        _isGroundedCheck = Physics.CheckSphere(GroundCheck.position, _groundDist, GroundMask);

        _isFalling = _currentMovement.y < -1 && _currentMovement.y > -3;
        _isFlying = _currentMovement.y < -3;

        handleGravity();
        handleJump();

        handleMove();

        handleRotation();

        if ((!_isMovementPressed && _isGroundedCheck) || _isFalling)
        {
            ClearSpeed();
        }
    }

    void handleMove()
    {
        if (_currentMovement.magnitude > 0.1f)
        {
            _targetAnlge = Mathf.Atan2(_currentMovement.x, _currentMovement.z) * Mathf.Rad2Deg + MainCamera.eulerAngles.y;
            _moveDir = Quaternion.Euler(0f, _targetAnlge, 0f) * Vector3.forward;

            handleWalk();
            handleRun();
        }
    }
    void handleRotation()
    {
        _smoothTurn = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAnlge, ref _turnSmoothVelocity, _turnSmoothTime);
        float noSmoothTurn = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAnlge, ref _turnSmoothVelocity, 0);
        transform.rotation = Quaternion.Euler(0f, _smoothTurn, 0f);

        #region резкий поворот
        float Difference = Mathf.Round(Mathf.Abs(noSmoothTurn - _smoothTurn));
        if (Difference > 60 && _isRunPressed)
            SlowDown();
        else if (Difference > 100)
            SlowDown();
        #endregion
    }
    void handleJump()
    {
        if (_isJumpPressed && _isGroundedCheck) // прыжок
        {
            ClearSpeed();

            _currentMovement.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
            _currentMovement.y += _gravityValue * Time.deltaTime;
            Controller.Move(_currentMovement * Time.deltaTime);  
        }
    }
    void handleRun()
    {
        if (_isRunPressed)
        {
            Controller.Move(_moveDir.normalized * _speedRun * Time.deltaTime); // бег
            _speedRun = Mathf.Clamp(_speedRun + 3f * Time.deltaTime, _speedWalk, _maxSpeedRun);
        }
    }
    void handleWalk()
    {
        if (_isMovementPressed && !_isRunPressed)
        {
            Controller.Move(_moveDir.normalized * _speedWalk * Time.deltaTime); // ходьба
            _speedWalk = Mathf.Clamp(_speedWalk + 2f * Time.deltaTime, _defaultSpeedW / 2, _defaultSpeedW);
            _speedRun = _defaultSpeedR;
        }
    }
    void handleGravity()
    {
        if (_isGroundedCheck)
        {
            float groundedGravity = -0.05f;
            _currentMovement.y = groundedGravity * Time.deltaTime;
        }
        else
        {
            _currentMovement.y += _gravityValue * Time.deltaTime;
            Controller.Move(_currentMovement * Time.deltaTime);
        }
    }
    async void SlowDown()
    {
        float end = Time.time + _slowDowns;
        while (Time.time < end)
        {
            ClearSpeed();

            await Task.Yield();
        }
        _speedWalk = _defaultSpeedW;
        _speedRun = _defaultSpeedR;
    }
    void ClearSpeed()
    {
        _speedRun = _speedWalk + _minSpeedR;
        _speedWalk = _minSpeedW;
    }
}
