using UnityEngine;

public class DimpleAbility : IAbility
{
    private float _canCastAfterTime = float.MinValue;

    public DimpleAbility(int damage, float cooldown, float cost)
    {
        Damage = damage;
        CoolDown = cooldown;
        Cost = cost;
    }

    public bool CanUse()
    {
        if (!(_canCastAfterTime <= Time.time)) return false;
        
        _canCastAfterTime = CoolDown + Time.time;
        return true;

    }

    public float Cost { get; }
    public float CoolDown { get; }
    public int Damage { get; }
    public GameObject Prefab { get; set; }

    public void Use()
    {
        
    }

    public void Accept(IAbilityVisitor visitor)
    {
        visitor.Visit(this);
    }
}