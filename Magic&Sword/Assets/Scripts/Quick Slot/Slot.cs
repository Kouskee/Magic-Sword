using UnityEngine;

public class Slot : MonoBehaviour
{
    public AbstractSpell _ability;

    public void SetAbilityToSlot(AbstractSpell ability)
    {
        _ability = ability;
    }
}
