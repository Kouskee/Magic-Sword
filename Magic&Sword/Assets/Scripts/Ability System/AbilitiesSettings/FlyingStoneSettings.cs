using DG.Tweening;
using UnityEngine;

public class FlyingStoneSettings : MonoBehaviour, ISettingsAbility
{
    [SerializeField] private Vector3 _changeScale;
    [SerializeField] private Ease _moveEase;
    
    private Transform _prefab;

    private void Awake()=>
        _prefab = GetComponent<Transform>();
    

    private void OnEnable() =>
        DOTween.Init();
    

    public void Settings(Transform player, Transform enemy, int range)
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

    private void Destroy()=>
        Destroy(gameObject); 
        //PoolObject.OnAbilityDestroy.Invoke(gameObject);
}