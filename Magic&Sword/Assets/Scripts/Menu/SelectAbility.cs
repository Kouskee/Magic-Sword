using Ability_System.AConfigs;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Menu
{
    public class SelectAbility : MonoBehaviour
    {
        public static readonly UnityEvent<AbilityConfig> OnSelectAbility = new UnityEvent<AbilityConfig>();

        private Image[] _inventorySlots;
    
        private AbilityConfig _abilityConfig;

        public void Init(Image[] inventorySlots) => 
            _inventorySlots = inventorySlots;
        
        private void Awake() => 
            OnSelectAbility.AddListener(OnSelect);
        
        private void OnSelect(AbilityConfig abilityConfig) => 
            _abilityConfig = abilityConfig;
        
        public void OnCastAbility(InputValue value)
        {
            if(_abilityConfig == null) return;
            var index = (int)value.Get<float>();
            _inventorySlots[index].sprite = _abilityConfig.Icon;
            _abilityConfig = null;
        }
    }
}