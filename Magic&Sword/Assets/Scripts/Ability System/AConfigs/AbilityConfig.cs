using UnityEngine;

[CreateAssetMenu(fileName = "AbilityConfig", menuName = "Ability/AbilityConfig")]
public class AbilityConfig : ScriptableObject
{
    [Header("Description")] 
    [SerializeField] private string _id;
    [SerializeField] private TypeDamage _typeDamage;
    [SerializeField] private Sprite _icon;
    [SerializeField] private GameObject _prefab;

    [Header("Attribute")] 
    [SerializeField] private float _cost;
    [SerializeField] private float _castRate;
    
    public string Id => _id;
    public TypeDamage TypeDamage => _typeDamage;
    public Sprite Icon => _icon;
    public GameObject Prefab => _prefab;
    public float Cost => _cost;
    public float Cooldown => 1 / _castRate;
}

public enum TypeDamage
{
    Fire,
    Water,
    Earth,
    Air
}