using Manager;
using Patterns.Factory;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    private Animator _animator;
    private Energy _energy;
    private Inventory _inventory;
    private SpawnAbility _spawnAbility;
    private AbilityPrefabFactory _prefabFactory;
    
    private static readonly int Moving = Animator.StringToHash("isMoving");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int IsAttack = Animator.StringToHash("isAttack");

    private void Awake()
    {
        EventManager.OnPressedAbilityKeyboard += OnGetPressedAbility;
    }

    public void Init(Energy energy, Inventory inventory, SpawnAbility spawnAbility, Animator animator)
    {
        _animator = animator;
        _energy = energy;
        _inventory = inventory;
        _spawnAbility = spawnAbility;
    }

    private void Start()
    {
        _prefabFactory = new AbilityPrefabFactory();
    }

    private void OnGetPressedAbility(int id)
    {
        var ability = _inventory.GetItem(id);
        var cost = ability.Cost;
        var prefabAbility = _prefabFactory.GetPrefab(id);
        if (_energy.CanCast(cost))
            CallUseAbility(ability, cost, prefabAbility, id);
    }

    private void CallUseAbility(IAbility ability, float cost, GameObject prefab, int id)
    {
        if (!ability.CanUse()) return;

        var isCast = _animator.GetBool(IsAttack);
        if(isCast) return;
        
        GlobalEventManager.OnUseAbility.Invoke(id);
        
        _spawnAbility.SpawnAbilityPrefab(prefab, ability);
        _energy.StealEnergy(cost);

        _animator.SetBool(Moving, false);
        _animator.SetBool(IsAttack, true);
        _animator.SetTrigger(Attack);
    }

    private void OnDestroy()
    {
        Debug.Assert(EventManager.OnPressedAbilityKeyboard != null, "EventManager.OnPressedAbilityKeyboard != null");
        EventManager.OnPressedAbilityKeyboard -= OnGetPressedAbility;
    }
}