using System.Collections;
using Character;
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

        private readonly int _isAttack = Animator.StringToHash("isAttack");
        private readonly int _attack = Animator.StringToHash("Attack");
        private readonly int _isMoving = Animator.StringToHash("isMoving");

        public override void Init(float duration = default)
        {
            Unit.TryGetComponent(out _agent);
            Unit.TryGetComponent(out _animator);
            _unitTransform = Unit.transform;
            _player = Player;

            _animator.SetTrigger(_attack);
            EnableAttack(true);
        }

        public override IEnumerator UpdateDelay()
        {
            while (!IsFinished)
            {
                yield return base.UpdateDelay();
               
                if (_agent.stoppingDistance >= Vector3.Distance(_unitTransform.position, _player.position)) continue;
                EnableAttack(false);
                IsFinished = true;
            }
        }

        private void EnableAttack(bool enable)
        {
            _animator.SetBool(_isAttack, enable);
            _animator.SetBool(_isMoving, !enable);
        }
    }
}