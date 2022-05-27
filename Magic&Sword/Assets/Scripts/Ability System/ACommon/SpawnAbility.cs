using Manager;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnAbility : MonoBehaviour
{
    private PoolObject _poolObject;
    private Transform _player;
    private Transform _unit;

    private void Awake() =>
        GlobalEventManager.OnSwapTargetEnemy.AddListener(unit => _unit = unit);

    public void Init(Transform player, PoolObject poolObject)
    {
        _poolObject = poolObject;
        _player = player;
    }
    
    public void SpawnAbilityPrefab(GameObject prefabAbility, IAbility ability)
    {
        var prefab = _poolObject.GetObject(prefabAbility.name);

        if (prefab.TryGetComponent<ISettingsAbility>(out var settingsAbility))
            settingsAbility.Settings(_player.transform, _unit);
        var count = settingsAbility.Count();
        if (count != 0)
        {
            for (var i = 0; i < count-1; i++)
            {
                var prefabs = _poolObject.GetObject(prefabAbility.name);
                if (prefabs.TryGetComponent<ISettingsAbility>(out var settingsAbilitys))
                    settingsAbilitys.Settings(_player.transform, _unit, i);
            }
        }
        if (prefab.TryGetComponent<TriggerZone>(out var triggerZone))
            triggerZone.Initialize(ability);
    }
}