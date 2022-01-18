using UnityEngine;

public class Player : MonoBehaviour
{
    private Energy _energy;
    private Inventory _inventory;

    private void Awake()
    {
        EventManager.OnPressedAbilityKeyboard += OnGetPressedAbility;
    }

    public void Initialize(Energy energy, Inventory inventory)
    {
        _energy = energy;
        _inventory = inventory;
    }

    private void OnGetPressedAbility(int id)
    {
        var ability = _inventory.GetItem(id);
        float cost = ability.Cost;

        if (_energy.CanCast(cost))
            CallUseAbility(ability, cost);
            
    }

    private void CallUseAbility(IAbility ability, float cost)
    {
        if (ability.CanUse())
        {
            ability.Use();
            _energy.StealEnergy(cost);
        }
            
    }
}