using System.Collections.Generic;
using System.Linq;
using Ability_System.AConfigs;

namespace Ability_System.Factory.AFactories
{
    public class AbilityFactory : IAbilityFactory
    {
        private readonly AbilityConfig[] _abilityConfigs;

        public AbilityFactory(IEnumerable<AbilityConfig> abilityConfigs)
        {
            _abilityConfigs = abilityConfigs.ToArray();
        }

        public IAbility[] GetAbilities()
        {
            var abilities = new IAbility[_abilityConfigs.Length];
            for (var i = 0; i < _abilityConfigs.Length; i++)
            {
                abilities[i] = new AbilityLogic(_abilityConfigs[i].TypeDamage, _abilityConfigs[i].Cooldown, _abilityConfigs[i].Cost);
            }

            return abilities;
        }
    }
}