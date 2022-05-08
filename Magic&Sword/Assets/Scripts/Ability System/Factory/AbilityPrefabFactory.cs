using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Patterns.Factory
{
    public class AbilityPrefabFactory
    {
        private readonly Dictionary<int, GameObject> _prefabs = new Dictionary<int, GameObject>();
    
        public AbilityPrefabFactory()
        {
            for (var i = 0; i < 4; i++)
            {
                _prefabs.Add(i, DataActivePrefabs.Prefabs[i]);
            }
        }

        public GameObject GetPrefab(int id)
        {
            return _prefabs.ContainsKey(id) ? _prefabs[id] : null;
        }
    }
}
