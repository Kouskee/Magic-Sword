using System.Collections.Generic;
using Cinemachine;
using Enemy;
using UnityEngine;

public class ChoiceEnemyInstaller : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup _targetGroup;
    
    private List<Transform> _unitsCameraRoots;
    private List<Unit> _units;
    
    private Transform _playerController;
    private SwitchTargetUnit _switchTargetUnit;
    private GameController _gameController;

    public void Install(Transform playerController, List<Transform> unitsCameraRoots, List<Unit> units, GameController gameController)
    {
        _unitsCameraRoots = unitsCameraRoots;
        _playerController = playerController;
        _units = units;
        _gameController = gameController;

        for (var i = 0; i < _unitsCameraRoots.Count; i++)
        {
            _units[i].Initialize(_playerController);
        }
        
        _switchTargetUnit = new SwitchTargetUnit(_unitsCameraRoots, _units, _targetGroup, _playerController, _gameController);
        
        _switchTargetUnit.CloseUnit();
    }
}
