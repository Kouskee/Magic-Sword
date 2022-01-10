using UnityEngine;

public class SlotManager : MonoBehaviour
{
    [SerializeField] private Slot[] _slots;

    public AbstractSpell GetAbilityFromSlot(int idPressedButton)
    {
        return _slots[idPressedButton]._ability;
    }
}
