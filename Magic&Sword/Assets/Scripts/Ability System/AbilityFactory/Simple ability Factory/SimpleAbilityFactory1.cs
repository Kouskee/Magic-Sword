using System.Collections.Generic;
using System.Linq;

public class DimpleAbilityFactory : IAbilityFactory
{
    private readonly DimpleAbilityConfig[] _dimpleAbilityConfigs;

    public DimpleAbilityFactory(IEnumerable<DimpleAbilityConfig> dimpleAbilityConfigs)
    {
        _dimpleAbilityConfigs = dimpleAbilityConfigs.ToArray();
    }

    public bool CanCreate(string id)
    {
        var canCreate = false;
        foreach (var config in _dimpleAbilityConfigs)
        {
            canCreate = canCreate || config.Id == id;
        }

        return canCreate;
    }

    public IAbility Create(string id)
    {
        var config = _dimpleAbilityConfigs.Single(s => s.Id == id);
        return new DimpleSpeelAbility(config.Damage, config.Cooldown, config.Cost);
    }
}