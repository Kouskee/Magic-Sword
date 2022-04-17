using Manager;
using UnityEngine;

namespace Enemy
{
    public class UnitHitBox : MonoBehaviour
    {
        private AiAgent _agent;

        private void Start()
        {
            _agent = GetComponent<AiAgent>();
        }

        private void DefaultVisit(IAbility ability)
        {
            GlobalEventManager.OnEnemyHit.Invoke(ability.Damage);
            Debug.Log(ability.Damage);
        }
        
        public IAbility Ability { get; }
    }
}
