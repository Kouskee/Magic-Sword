using System;
using UnityEngine;

public class SpawnAbility : MonoBehaviour
{
    private Transform _player;
    private Transform _enemy;

    public void Initialize(Transform player, Transform enemy)
    {
        _player = player;
        _enemy = enemy;
    }

    public void SpawnAbilityPrefab(GameObject prefabAility, IAbility ability)
    {
        var prefab = Instantiate(prefabAility);
        prefab.TryGetComponent<IAnimationAbility>(out var animationAbility);
        prefab.TryGetComponent<TriggerZone>(out var triggerZone);
        triggerZone.Initialize(ability);
        animationAbility.Settings(_player.transform, _enemy);
    }
}