using UnityEngine;

public abstract class AbstractSpell : MonoBehaviour
{
    public abstract AbilityConfig AbilityConfig { get; }
    public abstract void Use();
    public abstract void CanUse();
}
