using System;
using UnityEngine;

public class Knight_FirstSpell : AbstractSpell
{
    [SerializeField] private AbilityConfig _attribute;
    public override AbilityConfig AbilityConfig => _attribute;
    public override void Use() 
    {
        Debug.Log(_attribute.Name + " activated");
    }

    private float _canCastAfterTimerr = float.MinValue;
    public override void CanUse()
    {
        Debug.Log(_canCastAfterTimerr + " " + Time.time);
        if (_canCastAfterTimerr <= Time.time)
        {
            _canCastAfterTimerr = _attribute.Cooldown + Time.time;
        }
    }
}
