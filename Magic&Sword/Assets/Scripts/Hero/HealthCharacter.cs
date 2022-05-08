using UnityEngine;

public class HealthCharacter : MonoBehaviour
{
    private GameController _gameController;
    private InputController _inputController;
    private PlayerController _playerController;
    private Animator _animator;

    private float _health = 100;
    
    private static readonly int AnimDeath = Animator.StringToHash("Death");

    public void Initialize(GameController gameController) => _gameController = gameController;

    private void Awake() => TryGetComponent(out _animator);

    public void TakeDamage(float damage)
    {
        if (_health < 0)
            _health -= damage;
        else
        {
            _gameController.Death();
            _animator.SetBool(AnimDeath, true);
        }
    }
}