using UnityEngine;

public class Energy : MonoBehaviour
{
    private readonly float _minEnergy = 0, _maxEnergy = 100f;
    private float _energy;
    
    private void Start()
    {
        _energy = _maxEnergy;
    }

    private void Update()
    {
        if (_energy < _maxEnergy)
            _energy = Mathf.Clamp(_energy + Time.deltaTime * 2, _minEnergy, _maxEnergy);
    }

    public void StealEnergy(float costAbility)
    {
        _energy = Mathf.Clamp(_energy - costAbility, _minEnergy, _maxEnergy);
    }

    public bool CanCast(float costSpell)
    {
        if (_energy > costSpell)
            return true;
        return false;
    }
}