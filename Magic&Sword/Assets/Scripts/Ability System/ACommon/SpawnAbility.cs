using System.Collections;
using Manager;
using UnityEngine;

public class SpawnAbility : MonoBehaviour
{
    private PoolObject _poolObject;
    private Transform _player;
    private Transform _unit;
    private IAbility _ability;

    private void Awake() =>
        GlobalEventManager.OnSwapTargetEnemy.AddListener(unit => _unit = unit);

    public void Init(Transform player, PoolObject poolObject)
    {
        _poolObject = poolObject;
        _player = player;
    }

    public void SpawnAbilityPrefab(GameObject prefabAbility, IAbility ability)
    {
        _ability = ability;
        var prefab = _poolObject.GetObject(prefabAbility.name);

        if (!GetSettingsAbility(prefab, out var settingsAbility)) return;
        InitTrigger(prefab);

        settingsAbility.Settings(_player.transform, _unit);
        var count = settingsAbility.Count();
        if (count != 0)
            StartCoroutine(SpawnAbilityEnumerator(prefabAbility, count));
    }

    private IEnumerator SpawnAbilityEnumerator(Object prefabAbility, int _count)
    {
        var count = 1;
        while (count < _count)
        {
            var prefab = _poolObject.GetObject(prefabAbility.name);
            if (GetSettingsAbility(prefab, out var pillars))
                pillars.Settings(_player.transform, _unit, count + 1);
            InitTrigger(prefab);

            yield return new WaitForSeconds(.2f);
            count++;
        }
    }

    private bool GetSettingsAbility(GameObject go, out ISettingsAbility settings)
    {
        return go.TryGetComponent(out settings);
    }

    private void InitTrigger(GameObject go)
    {
        if (go.TryGetComponent<AbilityAttackTrigger>(out var triggerZone))
            triggerZone.Initialize(_ability);
    }
}