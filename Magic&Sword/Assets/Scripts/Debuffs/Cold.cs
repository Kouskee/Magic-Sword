using System.Collections;
using Ability_System.AConfigs;
using Enemy;
using UnityEngine;

namespace Debufs
{
    public class Cold : IDebuff
    {
        private IUnitDebuff _unitDebuff;
        private Unit _unit;
        private float _timeLeft;
        private int _stack;

        private const float TICK = 1f;

        public Cold(TypeDamage typeDamage) => TypeDamage = typeDamage;

        public void Activate(IUnitDebuff unitDebuff, Unit unit, int stack)
        {
            _unitDebuff = unitDebuff;
            _unit = unit;
            _stack = stack;
            _timeLeft = 5;

            VarietyEvents();
        }

        public IEnumerator Tick(float resist, float health)
        {
            while (_timeLeft > 0)
            {
                _timeLeft -= TICK;
                yield return new WaitForSeconds(TICK);
            }

            Destroy();
        }

        private void VarietyEvents()
        {
            if (_stack == 0)
               _unit.SlowedMove(_timeLeft);
            else
                _unit.Stun(_timeLeft);
        }

        public void Destroy()
        {
            _unitDebuff.RemoveDebuff(this);
        }

        public TypeDamage TypeDamage { get; }
    }
}