using Manager;
using UnityEngine;

namespace Enemy.States.Sources
{
    [CreateAssetMenu(menuName = "States/UnitDie")]
    public class UnitDie : State
    {
        private Animator _animator;

        private readonly int _animDying = Animator.StringToHash("Dying");

        public override void Init(float duration = default)
        {
            GlobalEventManager.OnDestroyTargetEnemy.Invoke(Unit);
            
            Unit.TryGetComponent(out _animator);
            _animator.SetTrigger(_animDying);
            Destroy(Unit);
        }
    }
}