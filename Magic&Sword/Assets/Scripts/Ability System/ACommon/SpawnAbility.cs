using Manager;
using UnityEngine;

public class SpawnAbility : MonoBehaviour
{
    private Transform _player;
    private Transform _enemy;

    private void Awake() => 
        GlobalEventManager.OnSwapTargetEnemy.AddListener(unit => _enemy = unit);
    
    public void Initialize(Transform player) => _player = player;

    public void SpawnAbilityPrefab(GameObject prefabAility, IAbility ability)
    {
        var prefab = Instantiate(prefabAility);
        prefab.TryGetComponent<IAnimationAbility>(out var animationAbility);
        prefab.TryGetComponent<TriggerZone>(out var triggerZone);
        triggerZone.Initialize(ability);
        animationAbility.Settings(_player.transform, _enemy);
    }
}