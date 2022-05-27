using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    private Image _energyUi;
    private float _energy;

    private readonly float _minEnergy = 0, _maxEnergy = 100f;

    public void Init(Image energyUi)
    {
        _energyUi = energyUi;
    }

    private void Start()
    {
        _energy = _maxEnergy;
    }

    private void Update()
    {
        if (_energy >= _maxEnergy) return;

        _energy += Mathf.Clamp(Time.deltaTime * 2, _minEnergy, _maxEnergy);
        _energyUi.fillAmount = _energy / 100;
    }

    public void StealEnergy(float costAbility)
    {
        _energy = Mathf.Clamp(_energy - costAbility, _minEnergy, _maxEnergy);
        _energyUi.fillAmount = _energy / 100;
    }

    public bool CanCast(float costSpell)
    {
        return _energy > costSpell;
    }
}