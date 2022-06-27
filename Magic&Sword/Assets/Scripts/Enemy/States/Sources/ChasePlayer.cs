using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.States.Sources
{
    [CreateAssetMenu(menuName = "States/ChasePlayer")]
    public class ChasePlayer : State
    {
        private Camera _camera;
        private Vector3 _target;
        private Transform _player;
        private Transform _unitTransform;
        private UnitMoveConfig _config;
        private NavMeshAgent _agent;
        private NavMeshPath _navMeshPath;
        private Animator _animator;
        private float _durationSlowed;
        private float _speed;
        private float _distanceToPlayer;
        private bool _isCurrentUnit;

        private readonly int _animSpeed = Animator.StringToHash("Velocity");

        private const float STOPPING_DISTANCE = 2;
        private const float RANDOM_POINT_RADIUS = 4f;
        private const float RADIUS_BEHIND_CAMERA = 8f;

        public override void Init(float duration = default)
        {
            Unit.TryGetComponent(out _agent);
            Unit.TryGetComponent(out _animator);
            _unitTransform = Unit.transform;
            _player = Player;
            _config = Config;
            _speed = _config.Speed;
            _durationSlowed = duration;
            _navMeshPath = new NavMeshPath();
            _camera = Camera.main;
            _isCurrentUnit = Unit.CurrentUnit;
        }

        public override IEnumerator UpdateDelay()
        {
            while (true)
            {
                yield return base.UpdateDelay();

                if (IsFinished) continue;

                _distanceToPlayer = Vector3.Distance(_unitTransform.position, _player.position);

                if (_agent.stoppingDistance >= _distanceToPlayer)
                {
                    Exit();
                    continue;
                }

                WhereToFollow();
                Follow();
            }
        }

        public override void Update()
        {
            Debug.DrawLine(_player.position, _target, Color.yellow);
            Debug.DrawLine(_unitTransform.position, _target, Color.blue);
        }

        private void WhereToFollow()
        {
            _agent.stoppingDistance = 0;
            var distancePlayerToTarget = Vector3.Distance(_target, _player.position);
            var disToTarget = Vector3.Distance(_unitTransform.position, _target);

            switch (_isCurrentUnit)
            {
                case true:
                    if (_distanceToPlayer > RANDOM_POINT_RADIUS)
                    {
                        if (distancePlayerToTarget > RANDOM_POINT_RADIUS)
                            _target = RandomPoint(_player.position);
                    }
                    else
                        Else();

                    break;
                case false:
                    if (disToTarget > 0.5f && _distanceToPlayer > STOPPING_DISTANCE)
                    {
                        if (distancePlayerToTarget > RADIUS_BEHIND_CAMERA)
                            _target = RandomPoint(_player.position - _camera.transform.forward * RADIUS_BEHIND_CAMERA);
                    }
                    else
                        Else();

                    break;
            }

            void Else()
            {
                _agent.stoppingDistance = STOPPING_DISTANCE;
                _target = _player.position;
            }
        }

        private Vector3 RandomPoint(Vector3 positionPoint)
        {
            var randomPoint = Vector3.zero;
            var isGeneratePoint = false;

            while (!isGeneratePoint)
            {
                NavMesh.SamplePosition(Random.onUnitSphere * RANDOM_POINT_RADIUS + positionPoint, out var navMeshHit,
                    RANDOM_POINT_RADIUS,
                    NavMesh.AllAreas);
                randomPoint = navMeshHit.position;

                if (float.IsInfinity(randomPoint.y)) continue;

                _agent.CalculatePath(randomPoint, _navMeshPath);
                isGeneratePoint = true;
            }

            return randomPoint;
        }

        private void Follow()
        {
            float speed;
            var meshSpeed = _agent.speed;

            if (_durationSlowed > 0)
            {
                _durationSlowed -= Time.deltaTime;
                speed = _speed / 2;
            }
            else
                speed = _speed;

            meshSpeed = ChangeSpeed(meshSpeed, speed);

            _agent.SetDestination(_target);
            _agent.speed = meshSpeed;

            _animator.SetFloat(_animSpeed, meshSpeed);
        }

        private float ChangeSpeed(float meshSpeed, float speed)
        {
            var meshSpeedReturn = Mathf.Lerp(meshSpeed, speed, Time.deltaTime * _config.SpeedChange);
            return meshSpeedReturn;
        }

        public override void Exit()
        {
            _agent.speed = 0;
            _animator.SetFloat(_animSpeed, 0);
            IsFinished = true;
        }
    }
}