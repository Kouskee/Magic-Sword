using UnityEngine;

public class Slot
{
    public readonly IAbility Ability;
    public readonly Sprite Sprite;
    
    public Slot(IAbility ability, Sprite icon)
    {
        Ability = ability;
        Sprite = icon;
    }
}
