using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoolObject : MonoBehaviour
{
    public static readonly UnityEvent<GameObject> OnAbilityDestroy = new UnityEvent<GameObject>();

    [SerializeField] private PrefabData[] _prefabData;

    private readonly Dictionary<string, GameObject> _prefabs = new Dictionary<string, GameObject>();
    private readonly Dictionary<string, Queue<GameObject>> _pools = new Dictionary<string, Queue<GameObject>>();

    private void Start()
    {
        OnAbilityDestroy.AddListener(ReturnObject);
    }

    private void Awake()
    {
        foreach (var prefabData in _prefabData)
        {
            _prefabs.Add(prefabData.Name.ToString(), prefabData.Prefab);
            _pools.Add(prefabData.Name.ToString(), new Queue<GameObject>());
        }
    }

    public GameObject GetObject(string poolName)
    {
        if (_pools[poolName].Count <= 0) return Instantiate(_prefabs[poolName]);

        var prefab = _pools[poolName].Dequeue();
        prefab.SetActive(true);
        return prefab;
    }

    private void ReturnObject(GameObject poolObject)
    {
        poolObject.name = poolObject.name.Replace("(Clone)", "");
        poolObject.SetActive(false);
        _pools[poolObject.name].Enqueue(poolObject);
    }
}

[Serializable]
public class PrefabData
{
    public EnumAbilities Name;
    public GameObject Prefab;
}