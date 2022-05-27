using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class PillarsSettings : MonoBehaviour, ISettingsAbility
{
   private VisualEffect _visualEffect;
   
   private float _scaleY = 2f;
   Transform _transformPlayer, _transformEnemy;
   
   public void Install(float scaleY)
   {
      _scaleY = scaleY * 0.5f;
   }
   
   public void Settings(Transform transformPlayer, Transform transformEnemy, int range = 0)
   {
      _transformPlayer = transformPlayer;
      _transformEnemy = transformEnemy;
      var direction = _transformEnemy.position - _transformPlayer.position;
      transform.position = transformPlayer.position + direction * range;
      _visualEffect = GetComponentInChildren<VisualEffect>();
      _visualEffect.SetFloat("ScaleY", _scaleY);
   }

   private void Start() => StartCoroutine(CheckNullParticle());
   
   IEnumerator CheckNullParticle()
   {
      while (true)
      {
         yield return new WaitForSeconds(.25f);
         if(_visualEffect.aliveParticleCount <= 0)
            PoolObject.OnAbilityDestroy.Invoke(gameObject);
      }
   }

   public int Count()
   {
      return (int)Vector3.Distance(_transformPlayer.position, _transformEnemy.position);
   }
}
