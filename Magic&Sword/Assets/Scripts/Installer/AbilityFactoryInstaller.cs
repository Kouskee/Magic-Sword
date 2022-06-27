using UnityEngine;

public class AbilityFactoryInstaller : MonoBehaviour
{
    [SerializeField] private AbilityConfig[] _flyingStoneConfig;
    [SerializeField] private AbilityConfig[] _freezingConfig;
    [SerializeField] private AbilityConfig[] _dimpleSpellConfig;

    public AbilityFactoryFacade Install()
    {
        var flyingStoneFactory = new AbilityFactory(_flyingStoneConfig);
        var freezingFactory = new AbilityFactory(_freezingConfig);
        var dimpleFactory = new AbilityFactory(_dimpleSpellConfig);
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