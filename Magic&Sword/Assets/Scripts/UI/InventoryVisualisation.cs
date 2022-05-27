using DG.Tweening;
using UnityEngine.UI;

namespace UI
{
    public class InventoryVisualisation
    {
        private readonly Inventory _inventory;
    
        private readonly Image[] _imagesShadow;
        private readonly Image[] _icons;

        public InventoryVisualisation(Inventory inventory, Image[] icons, Image[] imagesShadow)
        {
            _inventory = inventory;
            _icons = icons;
            _imagesShadow = imagesShadow;
        }

        public void Start()
        {
            for (var i = 0; i < _icons.Length; i++)
            {
                var icon = _inventory.GetIcon(i);
                _icons[i].sprite = icon;
            }
        }

        public void OnUseAbility(int id)
        {
            var ability = _inventory.GetItem(id);
            var image = _imagesShadow[id];

            image.fillAmount = 1;
            image.DOFillAmount(0, ability.CoolDown).SetEase(Ease.Linear);
        }
    }
}