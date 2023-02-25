using System;
using System.Linq;
using Ability_System.AConfigs;
using Ability_System.Factory.AFactories;
using Character;
using Data;
using Installer;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class GameInstaller : MonoBehaviour
{
    [FoldoutGroup("TEST")] [SerializeField] private bool _itTest;
    [FoldoutGroup("TEST")] [ShowIf("_itTest")] [SerializeField] private AbilityConfig[] _abilityConfigsForTest;
    
    [FoldoutGroup("MainObjects")] [SerializeField] private GameManager _gameManager;
    
    [FoldoutGroup("Characters")][SerializeField] private CharacterMovementController _characterMovementController;
    [FoldoutGroup("Characters")][SerializeField] private Animator _animator;
    
    [FoldoutGroup("Ability")][SerializeField] private PlayerAbility _playerAbility;
    [FoldoutGroup("Ability")][SerializeField] private SpawnAbility _spawnAbility;
    [FoldoutGroup("Ability")][SerializeField] private Energy _energy;

    [FoldoutGroup("Inventory")] [SerializeField] private Image _healthUi;
    [FoldoutGroup("Inventory")] [SerializeField] private Image _energyUi;
    [FoldoutGroup("Inventory")] [SerializeField] private Image[] _inventorySlots;
    [FoldoutGroup("Inventory")] [SerializeField] private Image[] _slotsShadow;
    [FoldoutGroup("Inventory")] [SerializeField] private Image _strafeShadow;
    
    private GameController _gameController;
    private InventoryInstaller _inventoryInstaller;
    private Inventory _inventory;
    private ChoiceEnemyInstaller _choiceEnemyInstaller;
    private SpawnInstaller _spawnInstaller;
    private AbilityFactory _factory;

    private AbilityConfig[] _abilityConfigs;

    void OnValidate()
    {
        if (_abilityConfigsForTest.Length == 4) return;
        
        Debug.LogWarning("Don't change field's array size!");
        Array.Resize(ref _abilityConfigsForTest, 4);
    }

    private void Awake()
    {
        _inventoryInstaller = new InventoryInstaller();
        TryGetComponent(out _spawnInstaller);
        TryGetComponent(out _choiceEnemyInstaller);

        if (_itTest)
        {
            _abilityConfigs = _abilityConfigsForTest;
            DataActiveAbilities.Abilities = _abilityConfigs;
        }
        else
            _abilityConfigs = DataActiveAbilities.Abilities;

        _factory = new AbilityFactory(_abilityConfigs);
    }

    private void Start()
    {
        Install();
        
        Destroy(gameObject);
    }

    private void Install()
    {
        var health = _characterMovementController.GetComponent<HealthCharacter>();
        
        _gameController = GameController.Singleton;
        
        health.Init(_gameController, _healthUi);
        _spawnInstaller.Spawn();
        var switchTargetUnit = _choiceEnemyInstaller.Install(_characterMovementController.transform, 
            _spawnInstaller.GetRootsCamera(), _spawnInstaller.GetUnits(), _gameController);

        var abilities = _factory.GetAbilities();
        var icons = new Sprite[_abilityConfigs.Length];
        
        for (var i = 0; i < _abilityConfigs.Length; i++)
        {
            icons[i] = _abilityConfigs[i].Icon;
        }
        
        _inventory = _inventoryInstaller.Install(abilities, icons, _abilityConfigs.Length);

        var poolObject = new PoolObject(_abilityConfigs.ToDictionary(config => config.Prefab.name,config => config.Prefab));
        var inventoryVis = new InventoryVisualisation(_inventory, _inventorySlots, _slotsShadow);
        var strafeVis = new StrafeVisualisation(_strafeShadow);
        _gameManager.Init(inventoryVis, strafeVis, switchTargetUnit);
        _energy.Init(_energyUi);
        _playerAbility.Init(_energy, _inventory, _spawnAbility, _animator);
        _spawnAbility.Init(_characterMovementController.transform, poolObject);
    }
}