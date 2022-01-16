using System;
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

        if (ability != null)
            CallUseAbility(ability);
    }

    private void CallUseAbility(IAbility ability)
    {
        if(ability.CanUse())
            ability.Use();
    }
}