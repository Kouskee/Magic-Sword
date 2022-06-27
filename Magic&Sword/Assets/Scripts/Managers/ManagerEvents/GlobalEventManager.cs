using Enemy;
using UnityEngine;
using UnityEngine.Events;

namespace Manager
{
    public class GlobalEventManager : MonoBehaviour
    {
        public static readonly UnityEvent OnEnemyKilled = new UnityEvent();
        public static readonly UnityEvent<Unit> OnDestroyTargetEnemy = new UnityEvent<Unit>();
        public static readonly UnityEvent<int> OnUseAbility = new UnityEvent<int>();
        public static readonly UnityEvent<float> OnStrafe = new UnityEvent<float>();
        public static readonly UnityEvent<Transform> OnSwapTargetEnemy = new UnityEvent<Transform>();
    }
}