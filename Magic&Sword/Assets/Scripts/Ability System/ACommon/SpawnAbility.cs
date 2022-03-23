using System;
using UnityEngine;

public class SpawnAbility : MonoBehaviour
{
    private PlayerController _player;
    private RandomMoveBot _enemy;

    public void Initialize(PlayerController player, RandomMoveBot enemy)
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
        animationAbility.Settings(_player.transform, _enemy.transform);
    }
}