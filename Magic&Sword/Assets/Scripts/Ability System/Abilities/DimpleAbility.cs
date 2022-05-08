using System.Collections.Generic;
using Debufs;
using UnityEngine;

public class DimpleAbility : IAbility
{
    private float _canCastAfterTime = float.MinValue;

    public DimpleAbility(TypeDamage type, float cooldown, float cost)
    {
        TypeDamage = type;
        CoolDown = cooldown;
        Cost = cost;
    }

    public List<IDebuff> Debuffs()
    {
        var container = new DebuffsContainer(TypeDamage);
        var debuffs = container.GetDebuffs();
        return debuffs;
    }

    public bool CanUse()
    {
        if (!(_canCastAfterTime <= Time.time)) return false;
        
        _canCastAfterTime = CoolDown + Time.time;
        return true;
    }

    public float Cost { get; }
    public float CoolDown { get; }
    public TypeDamage TypeDamage { get; }
}