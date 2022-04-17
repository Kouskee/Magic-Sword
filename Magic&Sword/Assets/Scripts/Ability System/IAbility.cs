using UnityEngine;

public interface IAbility
{
    float Cost { get; }
    float CoolDown { get; }
    int Damage { get; }
    
    void Use();
    bool CanUse();
}
