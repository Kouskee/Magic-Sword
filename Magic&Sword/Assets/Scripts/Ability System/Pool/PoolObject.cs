using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoolObject
{
    private GameObject _poolObject;
    
    public static readonly UnityEvent<GameObject> OnAbilityDestroy = new UnityEvent<GameObject>();

    private readonly Dictionary<string, GameObject> _prefabs;
    private readonly Dictionary<string, Queue<GameObject>> _pools = new Dictionary<string, Queue<GameObject>>();

    public PoolObject(Dictionary<string, GameObject> prefabs)
    {
        _prefabs = prefabs;
        Start();
    }

    private void Start()
    {
        _poolObject = new GameObject("PoolObjects");
        
        OnAbilityDestroy.AddListener(ReturnObject);
        foreach (var prefab in _prefabs)
        {
            _pools.Add(prefab.Key, new Queue<GameObject>());
        }
    }

    public GameObject GetObject(string poolName)
    {
        if (_pools[poolName].Count <= 0) return Object.Instantiate(_prefabs[poolName], _poolObject.transform);

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