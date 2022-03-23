using UnityEngine;
using UnityEngine.Events;

namespace Manager
{
    public class GlobalEventManager : MonoBehaviour
    {
        public static readonly UnityEvent<int> OnEnemySpawned = new UnityEvent<int>();
        public static readonly UnityEvent<int> OnEnemyKilled = new UnityEvent<int>();
        public static readonly UnityEvent<int> OnEnemyHit = new UnityEvent<int>();

        public static void SendEnemyHit(int damage)
        {
            OnEnemyHit.Invoke(damage);
        }
    }
}
