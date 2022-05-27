using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class EnemySpawn
{
    private List<Unit> _units;
    private List<Transform> _rootsCamera;

    private readonly Unit _unit;
    private readonly Transform[] _spawners;
    
    public EnemySpawn(Unit unit, Transform[] spawners)
    {
        _unit = unit;
        _spawners = spawners;
        
        _units = new List<Unit>(_spawners.Length);
        _rootsCamera= new List<Transform>(_spawners.Length);
    }

    public void Spawn(out List<Unit> units, out List<Transform> rootsCamera)
    {
        foreach (var spawner in _spawners)
        {
            var unit = Object.Instantiate(_unit, spawner);
            unit.transform.position = spawner.position;
            
            _units.Add(unit);
            _rootsCamera.Add(unit.GetComponentInChildren<RootCamera>().transform);
        }

        units = _units;
        rootsCamera = _rootsCamera;
    }
}