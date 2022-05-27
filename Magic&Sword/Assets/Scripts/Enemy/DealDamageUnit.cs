using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class DealDamageUnit : MonoBehaviour
    {
        private Transform _player;
        private HealthCharacter _health;
        
        private const float DAMAGE = 25;

        public void Init(Transform player)
        { 
            _player = player;
            player.TryGetComponent(out _health);
        }

        public void TurnOnDamage() => StartCoroutine(DealDamage());
        public void TurnOffDamage() => StopCoroutine(DealDamage());

        private IEnumerator DealDamage()
        {
            while (true)
            {
                if(Vector2.Distance(_player.position, transform.position) >= 1.5f)
                    yield return new WaitForSeconds(.1f);
                else
                {
                    _health.TakeDamage(DAMAGE);
                    yield break;
                }
                
            }
        }
    }
}