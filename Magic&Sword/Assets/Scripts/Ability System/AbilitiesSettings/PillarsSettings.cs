using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(BoxCollider))]
public class PillarsSettings : MonoBehaviour, ISettingsAbility
{
    private VisualEffect _visualEffect;
    private BoxCollider _collider;
    private Transform _transformPlayer, _transformEnemy;
    private float _scaleY;

    private const float SIZE_PILLAR = 3f;

    private void Awake()
    {
        TryGetComponent(out _collider);
        _visualEffect = GetComponentInChildren<VisualEffect>();
    }

    private void OnEnable() => StartCoroutine(CheckNullParticle());

    public void Settings(Transform transformPlayer, Transform transformEnemy, int range)
    {
        _scaleY = range * 0.5f;
        _transformPlayer = transformPlayer;
        _transformEnemy = transformEnemy;

        _collider.center = new Vector3(0, _scaleY, 0);
        _collider.size = new Vector3(2, _scaleY * 2, 2);

        var direction = _transformEnemy.position - _transformPlayer.position;
        direction = new Vector3(direction.x, 0, direction.z);
        transform.forward = direction;
        transform.position = transformPlayer.position + transform.forward * (range * SIZE_PILLAR);

        _visualEffect.SetFloat("ScaleY", _scaleY);
    }

    IEnumerator CheckNullParticle()
    {
        while (true)
        {
            yield return new WaitForSeconds(.25f);
            if (_visualEffect.aliveParticleCount > 0) continue;
            
            PoolObject.OnAbilityDestroy.Invoke(gameObject);
            yield return null;
        }
    }

    public int Count()
    {
        var count = Mathf.Clamp(Vector3.Distance(_transformPlayer.position, _transformEnemy.position) / SIZE_PILLAR,
            0, 3);
        return (int) count;
    }
}