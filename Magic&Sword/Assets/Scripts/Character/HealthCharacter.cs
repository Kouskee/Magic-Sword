using UnityEngine;
using UnityEngine.UI;

public class HealthCharacter : MonoBehaviour
{
    private Image _healthUi;
    private GameController _gameController;
    private Animator _animator;

    private float _health = 100;

    private static readonly int AnimDeath = Animator.StringToHash("Death");

    public void Init(GameController gameController, Image healthUI)
    {
        _gameController = gameController;
        _healthUi = healthUI;
    }

    private void Awake() => TryGetComponent(out _animator);

    public void TakeDamage(float damage)
    {
        if (_health > 0)
        {
            _health -= damage;
            _healthUi.fillAmount = _health/100;
        }
        else
        {
            _gameController.Death();
            _animator.SetBool(AnimDeath, true);
        }
    }
}