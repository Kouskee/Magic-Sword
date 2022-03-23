using UnityEngine;

public interface IAbility
{
    float Cost { get; }
    float CoolDown { get; }
    int Damage { get; }
    GameObject Prefab { get; set; }
    
    void Use();
    bool CanUse();
    void Accept(IAbilityVisitor visitor);
}
