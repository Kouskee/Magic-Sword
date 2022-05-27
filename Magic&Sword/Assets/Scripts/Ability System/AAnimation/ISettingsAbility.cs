using UnityEngine;

public interface ISettingsAbility
{
    void Settings(Transform transformPlayer, Transform transformEnemy, int range = 0);

    int Count();
}
