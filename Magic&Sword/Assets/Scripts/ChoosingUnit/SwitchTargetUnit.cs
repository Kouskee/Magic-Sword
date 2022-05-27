using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Enemy;
using Manager;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchTargetUnit
{
    private Transform _currentUnit;
    
    private readonly List<Transform> _unitsCamera = new List<Transform>();
    private readonly List<Unit> _units;
    private readonly Transform _player;
    private readonly CinemachineTargetGroup _targetGroup;
    private readonly GameController _gameController;

    private const float MIN_DISTANCE = float.MaxValue;

    public SwitchTargetUnit(IEnumerable<Transform> unitsCameraRoots, List<Unit> units, CinemachineTargetGroup targetGroup, 
        Transform player, GameController gameController)
    {
        _unitsCamera.AddRange(unitsCameraRoots);
        _units = units;
        _targetGroup = targetGroup;
        _player = player;
        _gameController = gameController;
        _currentUnit = targetGroup.m_Targets[1].target;

        var inputPlayer = new InputPlayer();
        inputPlayer.CharacterControls.Enable();
        inputPlayer.CharacterControls.SwapEnemy.performed += OnSwapUnit;
    }

    private void OnSwapUnit(InputAction.CallbackContext obj)
    {
        var direction = obj.ReadValue<float>();
        SideMeasurement(direction);
    }
    
    public void CloseUnit()
    {
        var minDistance = MIN_DISTANCE;
        var count = 0;
        
        for (var i = 0; i < _units.Count; i++)
        {
            var distance = Vector3.Distance(_unitsCamera[i].position, _player.position);

            if (!(distance < minDistance)) continue;
            
            minDistance = distance;
            count = i;
        }

        CurrentUnit(_unitsCamera[count], count);
    }

    private void SideMeasurement(float direction)
    {
        var minDistance = MIN_DISTANCE;
        var unitsCamera = _unitsCamera.ToList();
        var count = int.MaxValue;
        
        var currentUnit = _currentUnit.position;
        var playerPosition = _player.position;
        
        unitsCamera.Remove(_currentUnit);
        
        for (var i = 0; i < unitsCamera.Count; i++)
        {
            var anotherUnit = unitsCamera[i].position;
            
            var side = (anotherUnit.x - playerPosition.x)
                       * (currentUnit.z - playerPosition.z)
                       - (anotherUnit.z - playerPosition.z)
                       * (currentUnit.x - playerPosition.x);

            var sideClamp = (int) Mathf.Clamp(side, -1, 1);

            if (sideClamp != direction) continue;

            var distance = Vector3.Distance(unitsCamera[i].position, _currentUnit.position);

            if (!(distance < minDistance)) continue;

            minDistance = distance;
            count = i;
        }

        if(unitsCamera.Count <= count) return;

        var cameraRoot = unitsCamera[count];
        CurrentUnit(cameraRoot, _unitsCamera.IndexOf(cameraRoot));
    }

    private void CurrentUnit(Transform currentUnit, int index)
    {
        _currentUnit = _targetGroup.m_Targets[1].target = currentUnit;
        for (var i = 0; i < _units.Count; i++)
        {
            _units[i].CurrentUnit = i == index;
        }
        GlobalEventManager.OnSwapTargetEnemy.Invoke(_currentUnit);
    }
    
    public void OnDestroyTargetEnemy(Unit unit)
    {
        var index= _units.IndexOf(unit);
        _units.Remove(unit);
        _unitsCamera.Remove(_unitsCamera[index]);
        if (_unitsCamera.Count <= 0)
            _gameController.WinGame();
        else
            CloseUnit();
    }
}