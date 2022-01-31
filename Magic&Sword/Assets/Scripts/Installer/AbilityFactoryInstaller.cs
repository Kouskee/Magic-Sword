using System;
using UnityEngine;

public class AbilityFactoryInstaller : MonoBehaviour
{
    [SerializeField] private FlyingStoneConfig[] _flyingStoneConfig;
    [SerializeField] private DimpleAbilityConfig[] _dimpleSpellConfig;
    
    [SerializeField] private InventoryInstaller _inventoryInstaller;
    
    public AbilityFactoryFacade Install()
    {
        var flyingStoneFactory = new FlyingStoneFactory(_flyingStoneConfig);
        var dimpleFactory = new DimpleAbilityFactory(_dimpleSpellConfig);
        var abillityFactories = new IAbilityFactory[]
        {
            dimpleFactory,
            flyingStoneFactory,
        };
        var abilityFacade = new AbilityFactoryFacade(abillityFactories);

        return abilityFacade;
    }
}