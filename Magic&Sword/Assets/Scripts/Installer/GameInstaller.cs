using System.Linq;
using Character;
using Data;
using Installer;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class GameInstaller : MonoBehaviour
{
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
    private AbilityFactoryInstaller _abilityFactoryInstaller;
    private AbilityFactoryFacade _abilityFacade;
    private InventoryInstaller _inventoryInstaller;
    private Inventory _inventory;
    private ChoiceEnemyInstaller _choiceEnemyInstaller;
    private SpawnInstaller _spawnInstaller;

    private AbilityConfig[] _abilityConfigs;
    private readonly string[] _id = new string[4];

    private void Awake()
    {
        _inventoryInstaller = new InventoryInstaller();
        TryGetComponent(out _abilityFactoryInstaller);
        TryGetComponent(out _spawnInstaller);
        TryGetComponent(out _choiceEnemyInstaller);
        _abilityConfigs = DataActiveAbilities.Abilities;
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
        _abilityFacade = _abilityFactoryInstaller.Install();
        _spawnInstaller.Spawn();
        var switchTargetUnit = _choiceEnemyInstaller.Install(_characterMovementController.transform, 
            _spawnInstaller.GetRootsCamera(), _spawnInstaller.GetUnits(), _gameController);

        for (var i = 0; i < _abilityConfigs.Length; i++)
        {
            _id[i] = _abilityConfigs[i].Id;
        }
        
        var abilities = new IAbility[_id.Length];
        var icons = new Sprite[_id.Length];
        var configs = Resources.LoadAll<AbilityConfig>("Abilities");
        var images = configs.ToDictionary(config => config.Id, config => config.Icon);

        for (var i = 0; i < _id.Length; i++)
        {
            if (images.ContainsKey(_id[i]))
                icons[i] = images[_id[i]];

            var ability = _abilityFacade.Create(_id[i]);
            abilities[i] = ability;
        }

        _inventory = _inventoryInstaller.Install(abilities, icons, _id.Length);

        var poolObject = new PoolObject(_abilityConfigs.ToDictionary(config => config.Prefab.name,config => config.Prefab));
        var inventoryVis = new InventoryVisualisation(_inventory, _inventorySlots, _slotsShadow);
        var strafeVis = new StrafeVisualisation(_strafeShadow);
        _gameManager.Init(inventoryVis, strafeVis, switchTargetUnit);
        _energy.Init(_energyUi);
        _playerAbility.Init(_energy, _inventory, _spawnAbility, _animator);
        _spawnAbility.Init(_characterMovementController.transform, poolObject);
    }
}