using UnityEngine;

public class Slot
{
    private readonly IAbility _ability;
    private readonly Sprite _sprite;
    
    public Slot(IAbility ability, Sprite icon)
    {
        _ability = ability;
        _sprite = icon;
    }

    public Sprite GetIcon()
    {
        return _sprite;
    }

    public IAbility GetAbility()
    {
        return _ability;
    }
}
