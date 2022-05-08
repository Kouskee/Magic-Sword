using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private List<Unit> _units;
    private List<Transform> _rootsCamera;

    private Unit _unit;
    private Transform[] _spawners;
    
    public void Install(Unit unit, Transform[] spawners)
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
            var unit = Instantiate(_unit, spawner);
            unit.transform.position = spawner.position;
            
            _units.Add(unit);
            _rootsCamera.Add(unit.GetComponentInChildren<RootCamera>().transform);
        }

        units = _units;
        rootsCamera = _rootsCamera;
    }
}