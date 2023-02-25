using System.Collections;
using Ability_System.AConfigs;
using Enemy;
using UnityEngine;

namespace Debuffs
{
    public class Burning : IDebuff
    {
        private IUnitDebuff _unitDebuff;
        private float _timeLeft;
        private float _damage;

        private const float TICK = 1f;

        public Burning(TypeDamage typeDamage) => TypeDamage = typeDamage;

        public void Activate(IUnitDebuff unitDebuff, Unit unit, int stack)
        {
            _unitDebuff = unitDebuff;
            _timeLeft = 2f;
            _damage = 60;

            //To DO можно сделать публичный метод в юните с необязательными параметрами и и что-нибудь менять (шейдер огня)
        }

        public IEnumerator Tick(float resist, float health)
        {
            _damage -= _damage * resist;

            while (_timeLeft > 0)
            {
                _timeLeft -= TICK;
                health -= _damage;

                if (Mathf.Round(health) <= 0)
                {
                    _unitDebuff.Death();
                    yield break;
                }

                yield return new WaitForSeconds(TICK);
            }

            _unitDebuff.SetHealth(health);
            Destroy();
        }

        public void Destroy() => _unitDebuff.RemoveDebuff(this);

        public TypeDamage TypeDamage { get; }
    }
}