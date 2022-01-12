using UnityEngine;

public class InventoryInstaller : MonoBehaviour
{
    public Inventory Install(IAbility[] abilities, int capacity)
    {
        var slots = new Slot[capacity];
        for (int i = 0; i < capacity; i++)
        {
            slots[i] = new Slot(abilities[i]);
        }

        var inventory = new Inventory(slots);
        return inventory;
    }
}