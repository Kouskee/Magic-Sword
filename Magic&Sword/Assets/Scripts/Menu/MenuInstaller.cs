using Ability_System.AConfigs;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Menu
{
    public class MenuInstaller : MonoBehaviour
    {
        [SerializeField] private SelectAbility _selectAbility;
        [SerializeField] private MenuManager _manager;
        [SerializeField] private GameObject _prefabItem;
    
        [FoldoutGroup("Canvases")] [SerializeField]
        private GameObject _menuCanvas;
        [FoldoutGroup("Canvases")] [SerializeField]
        private GameObject _abilitiesCanvas;

        [FoldoutGroup("Abilities")] [SerializeField]
        private RectTransform _itemsParent;
        [FoldoutGroup("Abilities")] [SerializeField]
        private Image[] _inventorySlots;

        private AbilityConfig[] _configs;

        private void Awake()
        {
            _configs = Resources.LoadAll<AbilityConfig>("Abilities");
            foreach (var config in _configs)
            {
                var prefab = Instantiate(_prefabItem, _itemsParent);
                var image = prefab.GetComponent<Image>();
                prefab.name = config.Id;
                image.sprite = config.Icon;
                var eventTrigger = prefab.AddComponent<EventTrigger>();
                var eventEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerEnter};
                eventEntry.callback.AddListener((data) => {_manager.OnPressed(prefab.gameObject); });
                eventTrigger.triggers.Add(eventEntry);
            }

            Install();
            Destroy(gameObject);
        }

        private void Install()
        {
            _selectAbility.Init(_inventorySlots);
            _manager.Init(_configs, _inventorySlots, _menuCanvas, _abilitiesCanvas);
        }
    }
}