using System.Collections.Generic;
using Data;
using Installer;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class GameInstaller : MonoBehaviour
{
    [FoldoutGroup("Characters")][SerializeField] private PlayerController _playerController;
    
    [FoldoutGroup("Ability")][SerializeField] private PlayerAbility _playerAbility;
    [FoldoutGroup("Ability")][SerializeField] private SpawnAbility _spawnAbility;
    [FoldoutGroup("Ability")][SerializeField] private Animator _animator;
    [FoldoutGroup("Ability")][SerializeField] private Energy _energy;
    
    [FoldoutGroup("Settings Ability")][SerializeField] private GameObject[] _prefabAbility;
    [FoldoutGroup("Settings Ability")][SerializeField, Min(4)] private string[] _id;

    [FoldoutGroup("Inventory")] [SerializeField] private Image[] _inventorySlots;
    [FoldoutGroup("Inventory")] [SerializeField] private Image[] _slotsShadow;
    [FoldoutGroup("Inventory")] [SerializeField] private Image _strafeShadow;

    private GameController _gameController;
    private AbilityFactoryInstaller _abilityFactoryInstaller;
    private AbilityFactoryFacade _abilityFacade;
    private InventoryInstaller _inventoryInstaller;
    private Inventory _inventory;
    private ChoiceEnemyInstaller _choiceEnemyInstaller;
    private SpawnInstaller _spawnInstaller;

    private void Awake()
    {
        _inventoryInstaller = new InventoryInstaller();
        TryGetComponent(out _abilityFactoryInstaller);
        TryGetComponent(out _spawnInstaller);
        TryGetComponent(out _choiceEnemyInstaller);
        DataActivePrefabs.Prefabs = _prefabAbility;
    }

    private void Start()
    {
        Install();
        
        Destroy(gameObject);
    }

    private void Install()
    {
        var health = _playerController.GetComponent<HealthCharacter>();
        
        _gameController = GameController.Singleton;
        
        health.Initialize(_gameController);
        _abilityFacade = _abilityFactoryInstaller.Install();
        _spawnInstaller.Install();
        _choiceEnemyInstaller.Install(_playerController.transform, _spawnInstaller.GetRootsCamera(), _spawnInstaller.GetUnits(), _gameController);

        var abilities = new IAbility[_id.Length];
        var icons = new Sprite[_id.Length];
        var configs = Resources.LoadAll<AbilityConfig>("Abilities");
        var images = new Dictionary<string, Sprite>();

        foreach (var config in configs)
        {
            images.Add(config.Id, config.Icon);
        }
        
        for (var i = 0; i < _id.Length; i++)
        {
            if (images.ContainsKey(_id[i]))
                icons[i] = images[_id[i]];

            var ability = _abilityFacade.Create(_id[i]);
            abilities[i] = ability;
        }

        _inventory = _inventoryInstaller.Install(abilities, icons, _id.Length);

        new StrafeVisualisation(_strafeShadow);
        new InventoryVisualisation(_inventory, _inventorySlots, _slotsShadow);
        _playerAbility.Initialize(_energy, _inventory, _spawnAbility, _animator);
        _spawnAbility.Initialize(_playerController.transform);
    }
}