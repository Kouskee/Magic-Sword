using System.Collections.Generic;
using System.Linq;

namespace Patterns.Factory.AFactories
{
    public class FreezingFactory : IAbilityFactory
    {
        private readonly AbilityConfig[] _freezingConfigs;

        public FreezingFactory(IEnumerable<AbilityConfig> freezingConfigs)
        {
            _freezingConfigs = freezingConfigs.ToArray();
        }

        public bool CanCreate(string id)
        {
            var canCreate = false;
            foreach (var config in _freezingConfigs)
            {
                canCreate = canCreate || config.Id == id;
            }

            return canCreate;
        }

        public IAbility Create(string id)
        {
            var config = _freezingConfigs.Single(s => s.Id == id);
            return new Freezing(config.TypeDamage, config.Cooldown, config.Cost);
        }
    }
}