using System;
using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Energy _energy;
    [SerializeField] private string[] _id;

    [SerializeField] private AbilityFactoryInstaller _abilityFactoryInstaller;
    [SerializeField] private InventoryInstaller _inventoryInstaller;

    private AbilityFactoryFacade _abilityFacade;
    private Inventory _inventory;

    private void Start()
    {
        Install();
    }

    private void Install()
    {
        _abilityFacade = _abilityFactoryInstaller.Install();

        var abilities = new IAbility[_id.Length];
        for (int i = 0; i < _id.Length; i++)
        {
            var ability = _abilityFacade.Create(_id[i]);
            abilities[i] = ability;
        }

        _inventory = _inventoryInstaller.Install(abilities, _id.Length);

        _player.Initialize(_energy, _inventory);
    }
}