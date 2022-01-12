using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private Slot[] _slots;

    public Inventory(Slot[] slots)
    {
        _slots = slots;
    }

    public IAbility GetItem(int idPressedButton)
    {
        if (_slots.Length > idPressedButton)
            return _slots[idPressedButton].Ability;

        return null;
    }
}