using UnityEngine;

public class DimpleSpeelAbility : IAbility
{
    private readonly float _damage;
    private readonly float _cooldown;
    private float _canCastAfterTime = float.MinValue;

    public DimpleSpeelAbility(float damage, float cooldown)
    {
        _damage = damage;
        _cooldown = cooldown;
    }
    
    public bool CanUse()
    {
        if (_canCastAfterTime <= Time.time)
        {
            _canCastAfterTime = _cooldown + Time.time;
            return true;
        }
        return false;
    }

    public void Use() 
    {
        Debug.Log(_damage);
    }
}
