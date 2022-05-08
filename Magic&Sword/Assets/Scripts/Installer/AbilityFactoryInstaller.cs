using System;
using Patterns.Factory.AFactories;
using UnityEngine;

public class AbilityFactoryInstaller : MonoBehaviour
{
    [SerializeField] private AbilityConfig[] _flyingStoneConfig;
    [SerializeField] private AbilityConfig[] _freezingConfig;
    [SerializeField] private AbilityConfig[] _dimpleSpellConfig;

    public AbilityFactoryFacade Install()
    {
        var flyingStoneFactory = new FlyingStoneFactory(_flyingStoneConfig);
        var freezingFactory = new FreezingFactory(_freezingConfig);
        var dimpleFactory = new DimpleAbilityFactory(_dimpleSpellConfig);
        var abilityFactories = new IAbilityFactory[]
        {
            dimpleFactory,
            flyingStoneFactory,
            freezingFactory
        };
        var abilityFacade = new AbilityFactoryFacade(abilityFactories);

        return abilityFacade;
    }
}