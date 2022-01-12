using UnityEngine;

public class Slot
{
    public IAbility Ability;
    
    public Slot(IAbility ability)
    {
        Ability = ability;
    }

    public void SetAbilityToSlot(IAbility ability)
    {
        Ability = ability;
    }
}
