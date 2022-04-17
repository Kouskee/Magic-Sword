using UnityEngine;

public class InventoryInstaller
{
    public Inventory Install(IAbility[] abilities, int capacity, Sprite[] icons)
    {
        var slots = new Slot[capacity];
        for (int i = 0; i < capacity; i++)
        {
            slots[i] = new Slot(abilities[i], icons[i]);
        }

        var inventory = new Inventory(slots);
        return inventory;
    }
}