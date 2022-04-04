using Manager;
using UnityEngine;

namespace Enemy
{
    public class UnitHitBox : MonoBehaviour, IAbilityVisitor
    {
        private AiAgent _agent;

        private void Start()
        {
            _agent = GetComponent<AiAgent>();
        }

        public void Visit(FlyingStone ability)
        {
            DefaultVisit(ability);
        }

        public void Visit(DimpleAbility ability)
        {
            DefaultVisit(ability);
        }

        private void DefaultVisit(IAbility ability)
        {
            GlobalEventManager.OnEnemyHit.Invoke(ability.Damage);
            Debug.Log(ability.Damage);
        }
    }
}
