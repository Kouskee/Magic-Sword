using System;
using System.Collections.Generic;
using Data;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class GameInstaller : MonoBehaviour
{
    [FoldoutGroup("Characters")][SerializeField] private PlayerController _playerController;
    [FoldoutGroup("Characters")][SerializeField] private AiAgent _enemy;
    
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
        
        DataActivePrefabs.Prefabs = _prefabAbility;
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
        var icons = new Sprite[_id.Length];
        var configs = Resources.LoadAll<AbilityConfig>("RAbilities/Wizard");
        var sprites = new Dictionary<string, Sprite>();

        foreach (var config in configs)
        {
            sprites.Add(config.Id, config.Icon);
        }
        
        for (var i = 0; i < _id.Length; i++)
        {
            if (sprites.ContainsKey(_id[i]))
                icons[i] = sprites[_id[i]];

            var ability = _abilityFacade.Create(_id[i]);
            abilities[i] = ability;
        }

        _inventory = _inventoryInstaller.Install(abilities, _id.Length, icons);

        _playerAbility.Initialize(_energy, _inventory, _spawnAbility, _animator);
        _spawnAbility.Initialize(_playerController.transform, _enemy.transform);

        Instantiate(_playerAbility.gameObject);
    }
}