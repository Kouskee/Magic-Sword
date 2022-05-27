using DG.Tweening;
using UnityEngine;

namespace Character
{
    public class CharacterMovementController : MonoBehaviour
    {
        [Header("Player")] 
        [SerializeField] private float _moveSpeed = 1f;
        [SerializeField] private float _speedChangeRate = 10.0f;
        [SerializeField] private float _strafeSpeed = 4f;
        [SerializeField] private float _gravity = -15.0f;
        
        private float _speed;
        private float _verticalVelocity;
        private float _animVelocity;

        private CharacterController _controller;
        private Transform _player;

        private const float TERMINAL_VELOCITY = 53.0f;
        
        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _player = transform;
        }

        private void Start() => DOTween.Init();
        
        private void Update() => Gravity();

        public float Move(Vector2 move, float targetRotation)
        {
            const float SPEED_OFFSET = 0.1f;
            const float INPUT_MAGNITUDE = 1f;
            var targetSpeed = _moveSpeed;

            if (move == Vector2.zero) targetSpeed = 0.0f;

            var velocity = _controller.velocity;
            var currentHorizontalSpeed = new Vector3(velocity.x, 0.0f, velocity.z).magnitude;
            
            if (currentHorizontalSpeed < targetSpeed - SPEED_OFFSET || currentHorizontalSpeed > targetSpeed + SPEED_OFFSET)
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * INPUT_MAGNITUDE, Time.deltaTime * _speedChangeRate);
            else
                _speed = targetSpeed;

            var targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
            
            _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                             new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
            _animVelocity = Mathf.Lerp(_animVelocity, targetSpeed, Time.deltaTime * _speedChangeRate);
            return _animVelocity;
        }

        public void Strafe(float durationStrafe, float targetRotation )
        {
            var targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
            _controller.Move(targetDirection.normalized * (_strafeSpeed * Time.deltaTime) +
                             new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

            _player.DORotate(new Vector3(0, _player.transform.eulerAngles.y + 360, 0), durationStrafe, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear);
            _player.DOScale(new Vector3(0.7f, 0.7f, 1), durationStrafe * 0.5f).SetLoops(2, LoopType.Yoyo);
        }

        private void Gravity()
        {
            if (_verticalVelocity < TERMINAL_VELOCITY)
                _verticalVelocity += _gravity * Time.deltaTime;
        }
    }
}