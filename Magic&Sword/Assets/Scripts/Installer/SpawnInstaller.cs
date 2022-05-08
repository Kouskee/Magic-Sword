using System.Collections.Generic;
using Enemy;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Installer
{
    public class SpawnInstaller : MonoBehaviour
    {
        [SerializeField] private GameObject _spawner;

        [FoldoutGroup("Enemies")][SerializeField] private Unit _unit;
        [FoldoutGroup("Enemies")][SerializeField] private Transform[] _spawners;
        
        private EnemySpawn _enemySpawn;
        
        private List<Unit> _units = new List<Unit>();
        private List<Transform> _rootsCamera = new List<Transform>();
        
        private void Awake()
        {
            var spawner = Instantiate(_spawner);
            
            _enemySpawn = spawner.GetComponent<EnemySpawn>();
            
            _enemySpawn.Install(_unit, _spawners);
        }

        public void Install()
        {
            _enemySpawn.Spawn(out _units, out _rootsCamera);
        }

        public List<Unit> GetUnits() => _units;
        public List<Transform> GetRootsCamera() => _rootsCamera;
    }
}
