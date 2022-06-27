using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class FireSlashSettings : MonoBehaviour, ISettingsAbility
{
    [SerializeField] private VisualEffect _slashEffect;
    [SerializeField] private float _speed = 5f;

    private void OnEnable() => StartCoroutine(CheckNullParticle());
    
    public void Settings(Transform transformPlayer, Transform transformEnemy, int range = 1)
    {
        var direction = transformEnemy.position - transformPlayer.position;
        
        transform.position = transformPlayer.position + transformPlayer.forward * 1.5f;
        transform.forward = direction;
    }
    
    private void Update()
    {
        transform.position += transform.forward * (_speed * Time.deltaTime);
    }
    
    IEnumerator CheckNullParticle()
    {
        while (true)
        {
            yield return new WaitForSeconds(.25f);
            if (_slashEffect.aliveParticleCount > 0) continue;
            
            PoolObject.OnAbilityDestroy.Invoke(gameObject);
            yield return null;
        }
    }
}
