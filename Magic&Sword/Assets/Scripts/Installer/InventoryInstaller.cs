using UnityEngine;

public class InventoryInstaller
{
    public Inventory Install(IAbility[] abilities, Sprite[] icons, int capacity )
    {
        var slots = new Slot[capacity];
        for (var i = 0; i < capacity; i++)
        {
            slots[i] = new Slot(abilities[i], icons[i]);
        }

        var inventory = new Inventory(slots);
        return inventory;
    }
}