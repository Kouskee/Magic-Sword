using Manager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    [RequireComponent(typeof(CharacterMovementController))]
    [RequireComponent(typeof(CharacterAnimationController))]
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerInput))]
    
    public class Character : MonoBehaviour
    {
        [SerializeField] [Range(0.0f, 0.3f)] private float _turnSmoothTime = 0.12f;
        [Header("Strafe")]
        [SerializeField] private float _durationStrafe;
        [SerializeField] private float _cooldownStrafe;
        
        private CharacterMovementController _movementController;
        private CharacterAnimationController _animationController;

        private Camera _camera;
        
        private Vector2 _direction;
        private bool _strafe; 
        
        // Rotation
        private float _turnSmoothVelocity;
        private float _smoothTurn;
        private float _targetRotation;
        private bool _canRotate;
        
        // Strafe
        private float _timerStrafe;
        private float _canStrafeAfterTime;
        private bool _canStrafe;

        private void Awake()
        {
            TryGetComponent(out _animationController);
            TryGetComponent(out _movementController);
            _camera = Camera.main;
        }

        private void Update()
        {
            if (_canRotate) Ratition();
            Move();
            CanStrafe();
        }

        private void CanStrafe()
        {
            if (_canStrafeAfterTime <= 0)
            {
                if (_strafe && _canStrafe)
                {
                    GlobalEventManager.OnStrafe.Invoke(_cooldownStrafe);
                    _canStrafeAfterTime = _cooldownStrafe;
                    _canRotate = false;
                    
                    _movementController.Strafe(_durationStrafe, _targetRotation);
                    _animationController.SetStrafe(true);
                    
                    _canStrafe = false;
                    _timerStrafe = _durationStrafe;
                }
                else if (!_strafe)
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
                _animationController.SetStrafe(false);
                _canRotate = true;
            }
        }

        private void Ratition()
        {
            var angle = Quaternion.LookRotation(_camera.transform.forward, Vector3.right).eulerAngles.y;
            _targetRotation = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg + angle;

            _smoothTurn = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, _smoothTurn, 0f);
        }
        
        private void Move()
        {
            var velocity = _movementController.Move(_direction, _targetRotation);
            _animationController.SetMove(velocity);
        }

        public void OnMove(InputValue value)
        {
            _direction = value.Get<Vector2>();
        }

        public void OnStrafe(InputValue value)
        {
            _strafe = value.isPressed;
        }

        private void OnDeath()
        {
            
        }
    }
}