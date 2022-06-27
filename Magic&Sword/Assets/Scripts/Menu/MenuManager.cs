using Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu
{
    public class MenuManager : MonoBehaviour
    {
        private GameObject _menuCanvas;
        private GameObject _abilitiesCanvas;

        private AbilityConfig[] _configs;
        private Image[] _inventorySlots;

        public void Init(AbilityConfig[] configs, Image[] inventorySlots, GameObject menuCanvas, GameObject abilitiesCanvas)
        {
            _configs = configs;
            _inventorySlots = inventorySlots;
            _menuCanvas = menuCanvas;
            _abilitiesCanvas = abilitiesCanvas;
        }

        #region Menu

        public void StartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        public void ChangeAbilities()
        {
            EnableMenu(false);
        }

        public void ExitGame() => Application.Quit();

        #endregion

        #region Abilities

        public void OnPressed(GameObject go)
        {
            var image = go.GetComponent<Image>();
            foreach (var config in _configs)
            {
                if (config.Icon == image.sprite)
                    SelectAbility.OnSelectAbility.Invoke(config);
            }
        }

        public void SaveAbilities()
        {
            foreach (var config in _configs)
            {
                for (var i = 0; i < _inventorySlots.Length; i++)
                {
                    if (config.Icon == _inventorySlots[i].sprite)
                        DataActiveAbilities.Abilities[i] = config;
                }
            }
        }

        public void BackToMenu() => EnableMenu(true);

        #endregion

        private void EnableMenu(bool enable)
        {
            _menuCanvas.SetActive(enable);
            _abilitiesCanvas.SetActive(!enable);
        }
    }
}