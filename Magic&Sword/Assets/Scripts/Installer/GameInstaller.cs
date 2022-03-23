using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class GameInstaller : MonoBehaviour
{
    [FoldoutGroup("Players")][SerializeField] private PlayerController _playerController;
    [FoldoutGroup("Players")][SerializeField] private RandomMoveBot _enemy;
    
    [FoldoutGroup("Ability")][SerializeField] private PlayerAbility _playerAbility;
    [FoldoutGroup("Ability")][SerializeField] private SpawnAbility _spawnAbility;
    [FoldoutGroup("Ability")][SerializeField] private Animator _animator;
    [FoldoutGroup("Ability")][SerializeField] private Energy _energy;
    
    [FoldoutGroup("Settings Ability")][SerializeField] private GameObject[] _prefabAbility;
    [FoldoutGroup("Settings Ability")][SerializeField, Min(4)] private string[] _id;

    private AbilityFactoryInstaller _abilityFactoryInstaller;
    private AbilityFactoryFacade _abilityFacade;
    private InventoryInstaller _inventoryInstaller;
    private Inventory _inventory;
    

    private void Awake()
    {
        _inventoryInstaller = new InventoryInstaller();
        _abilityFactoryInstaller = GetComponent<AbilityFactoryInstaller>();
    }

    private void Start()
    {
        Install();
        
        Destroy(gameObject);
    }

    private void Install()
    {
        _abilityFacade = _abilityFactoryInstaller.Install();

        var abilities = new IAbility[_id.Length];
        for (int i = 0; i < _id.Length; i++)
        {
            var ability = _abilityFacade.Create(_id[i]);
            abilities[i] = ability;
            abilities[i].Prefab = _prefabAbility[i];
        }

        _inventory = _inventoryInstaller.Install(abilities, _id.Length);

        _playerAbility.Initialize(_energy, _inventory, _spawnAbility, _animator);
        _spawnAbility.Initialize(_playerController, _enemy);
    }
}