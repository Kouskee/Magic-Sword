using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private readonly Slot[] _slots;

    public Inventory(Slot[] slots)
    {
        _slots = slots;
    }

    public IAbility GetItem(int idPressedButton)
    {
        return _slots.Length > idPressedButton ? _slots[idPressedButton].Ability : null;
    }
}