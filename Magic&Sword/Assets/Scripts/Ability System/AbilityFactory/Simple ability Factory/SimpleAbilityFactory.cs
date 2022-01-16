using System.Collections.Generic;
using System.Linq;

public class SimpleAbilityFactory : IAbilityFactory
{
    private readonly SimpleAbilityConfig[] _simpleAbilityConfigs;

    public SimpleAbilityFactory(IEnumerable<SimpleAbilityConfig> simpleAbilityConfigs)
    {
        _simpleAbilityConfigs = simpleAbilityConfigs.ToArray();
    }

    public bool CanCreate(string id)
    {
        var canCreate = false;
        foreach (var config in _simpleAbilityConfigs)
        {
            canCreate = canCreate || config.Id == id;
        }

        return canCreate;
    }

    public IAbility Create(string id)
    {
        var config = _simpleAbilityConfigs.Single(s => s.Id == id);
        float cooldown = config.Cooldown;
        return new SimpleSpeelAbility(config.Damage, cooldown);
    }
}