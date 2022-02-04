using UnityEngine;

public class DimpleSpeelAbility : IAbility
{
    private readonly float _damage;
    private readonly float _cooldown;
    private float _canCastAfterTime = float.MinValue;

    public DimpleSpeelAbility(float damage, float cooldown, float cost)
    {
        _damage = damage;
        _cooldown = cooldown;
        Cost = cost;
    }

    public bool CanUse()
    {
        if (!(_canCastAfterTime <= Time.time)) 
            return false;
        
        _canCastAfterTime = _cooldown + Time.time;
        return true;

    }

    public float Cost { get; }
    public float CoolDown => _cooldown;
    public GameObject Prefab { get; set; }

    public void Use()
    {
        
    }
}