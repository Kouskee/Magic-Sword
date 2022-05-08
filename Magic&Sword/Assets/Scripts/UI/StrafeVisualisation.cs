using DG.Tweening;
using Manager;
using UnityEngine.UI;

namespace UI
{
    public class StrafeVisualisation
    {
        private readonly Image _strafeImageBg;
    
        public StrafeVisualisation(Image strafeImageBg)
        {
            _strafeImageBg = strafeImageBg;

            GlobalEventManager.OnStrafe.AddListener(OnStrafe);
        }

        private void OnStrafe(float duration)
        {
            _strafeImageBg.fillAmount = 1;
            _strafeImageBg.DOFillAmount(0, duration).SetEase(Ease.Linear);
        }
    }
}
