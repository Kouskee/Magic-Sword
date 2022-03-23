﻿using UnityEngine;

[CreateAssetMenu(fileName = "FlyingStoneConfig", menuName = "Ability/FlyingStoneConfig")]
public class FlyingStoneConfig : ScriptableObject
{
    [Header("Description")] 
    [SerializeField] private string _id;
    [SerializeField] private int _damage;
    [SerializeField] private Sprite _icon;

    [Header("Attribute")] 
    [SerializeField] private float _cost;
    [SerializeField] private float _castRate;
    
    public string Id => _id;
    public int Damage => _damage;
    public Sprite Icon => _icon;
    public float Cost => _cost;
    public float Cooldown => 1 / _castRate;
}