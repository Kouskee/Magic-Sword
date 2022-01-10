using UnityEngine;

public class Knight_FirstSpell1 : AbstractSpell
{
    [SerializeField] private AbilityConfig _attribute;
    public override AbilityConfig AbilityConfig => _attribute;
    public override void Use() 
    {
        Debug.Log(_attribute.Name + " activated");
    }
    
    private float _canCastAfterTime = float.MinValue;
    public override void CanUse()
    {
        if (_canCastAfterTime <= Time.time)
        {
            _canCastAfterTime = _attribute.Cooldown + Time.time;
        }
    }
}
