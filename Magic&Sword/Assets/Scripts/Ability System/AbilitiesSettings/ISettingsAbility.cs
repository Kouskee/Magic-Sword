using UnityEngine;

public interface ISettingsAbility
{
    void Settings(Transform transformPlayer, Transform transformEnemy, int range = 1);

    int Count() => 0;
}
