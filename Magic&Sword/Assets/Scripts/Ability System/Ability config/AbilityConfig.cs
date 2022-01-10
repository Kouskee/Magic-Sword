using UnityEngine;


[CreateAssetMenu(fileName = "AbilityAttribute",menuName = "Ability/AbilityAttribute")]
public class AbilityConfig : ScriptableObject
{
    [Header("Description")]
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;

    [Header("Attribute")]
    [SerializeField] private float _cost;
    [SerializeField] private float _castRate;
    
    public string Name => _name;
    public Sprite Icon => _icon;
    public float Cost => _cost;
    public float Cooldown => 1/_castRate;

}
