using System.Collections.Generic;

public interface IAbility
{
    float Cost { get; }
    float CoolDown { get; }
    TypeDamage TypeDamage { get; }
    
    List<IDebuff> Debuffs();
    bool CanUse();
}
