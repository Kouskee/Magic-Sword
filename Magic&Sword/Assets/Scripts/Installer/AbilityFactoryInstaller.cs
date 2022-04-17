using System;
using UnityEngine;

public class AbilityFactoryInstaller : MonoBehaviour
{
    [SerializeField] private AbilityConfig[] _flyingStoneConfig;
    [SerializeField] private AbilityConfig[] _dimpleSpellConfig;

    public AbilityFactoryFacade Install()
    {
        var flyingStoneFactory = new FlyingStoneFactory(_flyingStoneConfig);
        var dimpleFactory = new DimpleAbilityFactory(_dimpleSpellConfig);
        var abilityFactories = new IAbilityFactory[]
        {
            dimpleFactory,
            flyingStoneFactory,
        };
        var abilityFacade = new AbilityFactoryFacade(abilityFactories);

        return abilityFacade;
    }
}