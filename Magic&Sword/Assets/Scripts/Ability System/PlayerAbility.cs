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
    
    private static readonly int IsCast = Animator.StringToHash("Cast");
    private static readonly int SwapCast = Animator.StringToHash("SwapCast");

    private void Awake()
    {
        EventManager.OnPressedAbilityKeyboard += OnGetPressedAbility;
    }

    public void Initialize(Energy energy, Inventory inventory, SpawnAbility spawnAbility, Animator animator)
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

        var isCast = _animator.GetBool(IsCast);
        if(isCast) return;
        
        GlobalEventManager.OnUseAbility.Invoke(id);
        
        _spawnAbility.SpawnAbilityPrefab(prefab, ability);
        _energy.StealEnergy(cost);

        _animator.SetBool(IsCast, true);
        _animator.SetInteger(SwapCast, id);
    }

    private void OnDestroy()
    {
        Debug.Assert(EventManager.OnPressedAbilityKeyboard != null, "EventManager.OnPressedAbilityKeyboard != null");
        EventManager.OnPressedAbilityKeyboard -= OnGetPressedAbility;
    }
}