using System;
using UnityEngine;

namespace Enemy
{
    public class DealDamageUnit : MonoBehaviour
    {
        private BoxCollider _boxCollider;

        private const float DAMAGE = 25;

        private void Awake() => _boxCollider = GetComponentInChildren<BoxCollider>();

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out HealthCharacter health)) return;
            
            health.TakeDamage(DAMAGE);
        }
        

        public void TurnOnDamage() => _boxCollider.enabled = true;
        public void TurnOffDamage() => _boxCollider.enabled = false;
    }
}