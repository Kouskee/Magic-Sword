using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class IceGroundSettings : MonoBehaviour, ISettingsAbility
{
    private VisualEffect _visualEffect;
    
    private void Awake()
    {
        _visualEffect = GetComponentInChildren<VisualEffect>();
    }

    private void OnEnable() => StartCoroutine(CheckNullParticle());

    public void Settings(Transform transformPlayer, Transform transformEnemy, int range = 1)
    {
        var direction = transformEnemy.position - transformPlayer.position;
        
        transform.position = transformPlayer.position + transformPlayer.forward * 1f;
        transform.forward = direction;
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
    
}
