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
        return _slots.Length > idPressedButton ? _slots[idPressedButton].GetAbility() : null;
    }
    
    public Sprite GetIcon(int id)
    {
        return _slots.Length > id ? _slots[id].GetIcon() : null;
    }
}