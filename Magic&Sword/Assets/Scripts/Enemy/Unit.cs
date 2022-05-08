using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private State _chasePlayer;
        [SerializeField] private State _stunUnit;
        [SerializeField] private State _attack;
        [SerializeField] private State _die;

        private State _currentState;
        private Transform _player;
        private UnitMoveConfig _config;
        private NavMeshAgent _agent;

        private float _turnSmoothVelocity;

        public void Initialize(Transform player)
        {
            _player = player;
        }

        private void Awake() => TryGetComponent(out _agent);

        private void Start()
        {
            GlobalEventManager.OnSwapTargetEnemy.AddListener(transform => _currentState.Unit = this);
            _config = Resources.Load<UnitMoveConfig>("AIConfigs/Ai Agent Config");
            
            SetState(_chasePlayer);
        }

        private void Update()
        {
            Rotation();
            
            if (!_currentState.IsFinished)
                _currentState.Update();
            else
                SetState(_agent.stoppingDistance >= Vector3.Distance(transform.position, _player.position) 
                    ? _attack 
                    : _chasePlayer);
        }

        private void SetState(State state, float duration = default)
        {
            _currentState?.Exit();
            _currentState = Instantiate(state);
            InitVariables();
            _currentState.Init(duration);
            StartCoroutine(_currentState.UpdateDelay());
        }

        private void InitVariables()
        {
            _currentState.Player = _player;
            _currentState.Unit = this;
            _currentState.Config = _config;
        }

        private void Rotation()
        {
            var player = _player.position;
            var subtractVectors = (player - transform.position).normalized;
            var angle = Quaternion.LookRotation(subtractVectors, Vector3.right).eulerAngles.y;
            var smoothTurn = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref _turnSmoothVelocity, _config.TurnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, smoothTurn, 0f);
        }

        public void Death() => SetState(_die);

        public void SlowedMove(float time) => SetState(_chasePlayer, time);
        
        public void Stun(float time) => SetState(_stunUnit, time);

        public bool AnimationIsOver { get; private set; }
        
        public bool CurrentUnit { get; set; }

        public void StartAnimationIsOver()
        {
            AnimationIsOver = true;
        }
    }
}