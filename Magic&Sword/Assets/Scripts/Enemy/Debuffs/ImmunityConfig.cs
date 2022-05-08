using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Debuffs/Immunity")]
public class ImmunityConfig : ScriptableObject
{
    [SerializeField, Range(0, 100)] private float _health;
    [SerializeField] private DictionaryDebuffResist[] _debuffResist;

    public float Health => _health;
    public Dictionary<TypeDamage, float> DebuffResist
    {
        get
        {
            var debuffResist = new Dictionary<TypeDamage, float>();
            foreach (var dictionary in _debuffResist)
            {
                debuffResist.Add(dictionary.TypeDamage, (float)dictionary.Resist / 100);
            }
            return debuffResist;
        }
    }
}

[Serializable]
public class DictionaryDebuffResist
{
    public TypeDamage TypeDamage;
    [Range(0, 100)] public int Resist;
}