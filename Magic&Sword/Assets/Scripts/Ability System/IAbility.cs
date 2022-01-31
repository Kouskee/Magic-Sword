using UnityEngine;

public interface IAbility
{
    float Cost { get; }
    float CoolDown { get; }
    GameObject Prefab { get; set; }
    void Use();
    bool CanUse();
}
