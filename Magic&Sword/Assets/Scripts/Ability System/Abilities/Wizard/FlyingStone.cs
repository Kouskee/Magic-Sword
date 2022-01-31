using UnityEngine;

public class FlyingStone : IAbility
{
    private readonly float _damage;
    private readonly float _cooldown;
    private float _canCastAfterTime = float.MinValue;

    public FlyingStone(float damage, float cooldown, float cost)
    {
        _damage = damage;
        _cooldown = cooldown;
        Cost = cost;
    }

    public bool CanUse()
    {
        if (_canCastAfterTime <= Time.time)
        {
            _canCastAfterTime = _cooldown + Time.time;
            return true;
        }

        return false;
    }

    public float Cost { get; }
    public float CoolDown => _cooldown;
    public GameObject Prefab { get; set; }

    public void Use()
    {
        Debug.Log(_damage);
    }
}