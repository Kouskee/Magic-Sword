using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    private List<IDebuff> _debuffs;
    private IAbility _ability;

    public void Initialize(IAbility ability) => _ability = ability;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out IUnitDebuff enemy)) return;
        
        _debuffs = _ability.Debuffs();
        if(_debuffs == null) return;
        foreach (var debuff in _debuffs)
        {
            enemy.AddDebuff(debuff);
        }
    }
}