using System.Collections.Generic;
using System.Linq;

public class FlyingStoneFactory : IAbilityFactory
{
    private readonly FlyingStoneConfig[] _simpleAbilityConfigs;

    public FlyingStoneFactory(IEnumerable<FlyingStoneConfig> simpleAbilityConfigs)
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
        return new FlyingStone(config.Damage, config.Cooldown, config.Cost);
    }
}