using System;

public class AbilityFactoryFacade
{
    private readonly IAbilityFactory[] _abilityFactories;

    public AbilityFactoryFacade(IAbilityFactory[] abilityFactories)
    {
        _abilityFactories = abilityFactories;
    }

    public IAbility Create(string id)
    {
        foreach (var abilityFactory in _abilityFactories)
        {
            if (abilityFactory.CanCreate(id))
                return abilityFactory.Create(id);
        }
        
        throw new Exception("Doesn't contains id: " + id);
    }
    
    // public IAbility Create(int idFactory, int idAbility)
    // {
    //     if (_abilityFactories[idFactory].CanCreate(idAbility))
    //         return _abilityFactories[idFactory].Create(idAbility);
    //     
    //     throw new Exception("Doesn't contains id: " + idAbility);
    // }
}