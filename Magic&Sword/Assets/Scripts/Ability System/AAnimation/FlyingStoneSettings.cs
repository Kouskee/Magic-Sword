using DG.Tweening;
using UnityEngine;

public class FlyingStoneSettings : MonoBehaviour, ISettingsAbility
{
    private Transform _prefab;
    [SerializeField] private Vector3 _changeScale;
    [SerializeField] private Ease _moveEase;

    private void Awake()
    {
        _prefab = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        DOTween.Init();
    }

    public void Settings(Transform player, Transform enemy, int range = 0)
    {
        _prefab.position = enemy.position + Vector3.up * 1.5f;
        _prefab.Translate(player.forward);

        DoMove();
    }
    
    private void DoMove()
    {
        var sequence = DOTween.Sequence();

        sequence.Append(_prefab.DOMoveY(_prefab.position.y - 2, 1).SetEase(_moveEase));

        sequence.Append(_prefab.DOScale(_changeScale, 1)).OnComplete(Destroy);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
    
    public int Count() => 0;
}