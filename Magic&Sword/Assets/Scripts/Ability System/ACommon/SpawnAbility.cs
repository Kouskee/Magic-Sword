using System;
using UnityEngine;

public class SpawnAbility : MonoBehaviour
{
    private ThirdPersonMovement _player;

    public void Initialize(ThirdPersonMovement player)
    {
        _player = player;
    }

    public void SpawnAbilityPrefab(GameObject prefabAility)
    {
        var prefab = Instantiate(prefabAility);
        prefab.TryGetComponent<IAnimationAbility>(out var animationAbilityn);
        animationAbilityn.Settings(_player.transform);
    }
}