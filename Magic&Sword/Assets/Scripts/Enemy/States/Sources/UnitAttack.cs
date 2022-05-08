using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.States.Sources
{
    [CreateAssetMenu(menuName = "States/UnitAttack")]
    public class UnitAttack : State
    {
        private Transform _player;
        private Transform _unitTransform;
        private Animator _animator;
        private NavMeshAgent _agent;

        private readonly int _isAttack = Animator.StringToHash("IsAttack");

        public override void Init(float duration = default)
        {
            Unit.TryGetComponent(out _agent);
            Unit.TryGetComponent(out _animator);
            _unitTransform = Unit.transform;
            _player = Player;
            _animator.SetBool(_isAttack, true);
        }

        public override IEnumerator UpdateDelay()
        {
            while (!IsFinished)
            {
                yield return base.UpdateDelay();
               
                if (_agent.stoppingDistance >= Vector3.Distance(_unitTransform.position, _player.position)) continue;
                _animator.SetBool(_isAttack, false);
                IsFinished = true;
            }
        }
    }
}