using System.Collections.Generic;
using System.Linq;

public class AbilityFactory : IAbilityFactory
{
    private readonly AbilityConfig[] _abilityConfigs;

    public AbilityFactory(IEnumerable<AbilityConfig> abilityConfigs)
    {
        _abilityConfigs = abilityConfigs.ToArray();
    }

    public bool CanCreate(string id)
    {
        var canCreate = false;
        foreach (var config in _abilityConfigs)
        {
            canCreate = canCreate || config.Id == id;
        }

        return canCreate;
    }

    public IAbility Create(string id)
    {
        var config = _abilityConfigs.Single(s => s.Id == id);
        return new AbilityLogic(config.TypeDamage, config.Cooldown, config.Cost);
    }
}