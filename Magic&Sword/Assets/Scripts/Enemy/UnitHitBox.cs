using System.Collections.Generic;
using System.Linq;
using Manager;
using UnityEngine;

namespace Enemy
{
    public class UnitHitBox : MonoBehaviour, IUnitDebuff
    {
        [SerializeField] private ImmunityConfig _immunity;

        private Dictionary<TypeDamage, float> _debuffResist;
        private List<IDebuff> _debuffs;
        private List<Coroutine> _coroutines;

        private Unit _unit;

        private int _stack;
        private float _resist;
        private float _health;

        private void Awake()
        {
            _debuffs = new List<IDebuff>();
            _coroutines = new List<Coroutine>();
            TryGetComponent(out _unit);
        }

        private void Start()
        {
            _debuffResist = _immunity.DebuffResist;
            _health = _immunity.Health;
        }

        public void AddDebuff(IDebuff debuff)
        {
            var key = debuff.TypeDamage;
            float resist = 0;

            _debuffs.Add(debuff);
            if (_debuffResist.ContainsKey(key))
                resist = _debuffResist[key];

            var matches = _debuffs.GroupBy(x => x.TypeDamage)
                .Where(g => g.Count() > 1);
            _stack = matches.Count();

            debuff.Activate(this, _unit, _stack);
            _coroutines.Add(StartCoroutine(debuff.Tick(resist, _health)));
        }

        public void RemoveDebuff(IDebuff debuff) => _debuffs.Remove(debuff);

        public void SetHealth(float health) => _health = Mathf.Round(health);

        public void Death()
        {
            foreach (var coroutine in _coroutines)
            {
                StopCoroutine(coroutine);
            }
            
            _unit.Death();
            Destroy(this);
            
            GlobalEventManager.OnEnemyKilled.Invoke();
        }
    }
}