using System.Collections.Generic;
using Ability_System.AConfigs;

public interface IAbility
{
    float Cost { get; }
    float CoolDown { get; }
    TypeDamage TypeDamage { get; }
    
    List<IDebuff> Debuffs();
    bool CanUse();
}
