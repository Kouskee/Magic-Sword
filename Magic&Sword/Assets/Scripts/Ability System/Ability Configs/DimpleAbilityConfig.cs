using UnityEngine;


[CreateAssetMenu(fileName = "DimpleAbilityConfig",menuName = "Ability/DimpleAbilityConfig")]
public class DimpleAbilityConfig : ScriptableObject
{
    [Header("Description")]
    [SerializeField] private int _id;
    [SerializeField] private float _damage;
    [SerializeField] private Sprite _icon;

    [Header("Attribute")]
    [SerializeField] private float _cost;
    [SerializeField] private float _castRate;
    
    public int Id => _id;
    public float Damage => _damage;
    public Sprite Icon => _icon;
    public float Cost => _cost;
    public float Cooldown => 1/_castRate;

}
