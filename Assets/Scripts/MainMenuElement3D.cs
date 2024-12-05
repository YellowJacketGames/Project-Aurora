using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class MainMenuElement3D : MonoBehaviour
    {
        public TMP_Text target;
        [SerializeField] private ColorBlock _colors;
        [SerializeField] private UnityEvent onClick;
        [SerializeField] private UnityEvent onClickEndCam;

        private bool _isHovered;

        public void PerformHoverAction()
        {
            _isHovered = true;
            target.color = _colors.highlightedColor;
        }

        public void PerformResetHoverAction()
        {
            target.color = _colors.normalColor;
            _isHovered = false;
        }

        public void PerformClick()
        {
            StartCoroutine(SimulateClickEffect());
        }


        private System.Collections.IEnumerator SimulateClickEffect()
        {
            if (target == null) yield break;
            target.color = _colors.pressedColor;
            yield return new WaitForSeconds(0.1f);
            target.color = _isHovered ? _colors.pressedColor : _colors.normalColor;
            onClick?.Invoke();
            yield return new WaitForSeconds(2f);
            onClickEndCam?.Invoke();
        }
    }
}