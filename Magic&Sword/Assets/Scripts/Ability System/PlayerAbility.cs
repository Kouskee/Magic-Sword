using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    private Energy _energy;
    private Inventory _inventory;
    private SpawnAbility _spawnAbility;

    private void Awake()
    {
        EventManager.OnPressedAbilityKeyboard += OnGetPressedAbility;
    }

    public void Initialize(Energy energy, Inventory inventory, SpawnAbility spawnAbility)
    {
        _energy = energy;
        _inventory = inventory;
        _spawnAbility = spawnAbility;
    }

    private void OnGetPressedAbility(int id)
    {
        var ability = _inventory.GetItem(id);
        var cost = ability.Cost;
        var prefabAbility = ability.Prefab;

        if (_energy.CanCast(cost))
            CallUseAbility(ability, cost, prefabAbility);
    }

    private void CallUseAbility(IAbility ability, float cost, GameObject prefab)
    {
        if (!ability.CanUse()) return;
        
        _spawnAbility.SpawnAbilityPrefab(prefab);
        ability.Use();
        _energy.StealEnergy(cost);
    }
}