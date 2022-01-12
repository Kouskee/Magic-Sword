using System;
using UnityEngine;

public class AbilityFactoryInstaller : MonoBehaviour
{
    [SerializeField] private SimpleAbilityConfig[] _simpleSpellConfig;
    [SerializeField] private DimpleAbilityConfig[] _dimpleSpellConfig;
    
    [SerializeField] private InventoryInstaller _inventoryInstaller;
    
    public AbilityFactoryFacade Install()
    {
        var abilityFactory = new SimpleAbilityFactory(_simpleSpellConfig);
        var dimpleFactory = new DimpleAbilityFactory(_dimpleSpellConfig);
        var abillityFactories = new IAbilityFactory[]
        {
            dimpleFactory,
            abilityFactory,
        };
        var abilityFacade = new AbilityFactoryFacade(abillityFactories);

        return abilityFacade;
    }
}