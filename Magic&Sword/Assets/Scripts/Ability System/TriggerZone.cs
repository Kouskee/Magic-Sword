using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    private IAbility _ability;

    public void Initialize(IAbility ability)
    {
        _ability = ability;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent(out IAbilityVisitor abilityVisitor);
        Debug.Log(abilityVisitor);
        _ability.Accept(abilityVisitor);
    }
    
}