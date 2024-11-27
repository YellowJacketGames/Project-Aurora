using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class MainMenuElement3D : MonoBehaviour
    {
        [SerializeField] private TMP_Text _target;
        [SerializeField] private ColorBlock _colors;
        [SerializeField] private UnityEvent onClick;
        [SerializeField] private UnityEvent onClickEndCam;

        private bool _isHovered;

        public void PerformHoverAction()
        {
            _isHovered = true;
            _target.color = _colors.highlightedColor;
        }

        public void PerformResetHoverAction()
        {
            _target.color = _colors.normalColor;
            _isHovered = false;
        }

        public void PerformClick()
        {
            StartCoroutine(SimulateClickEffect());
        }

        private System.Collections.IEnumerator SimulateClickEffect()
        {
            if (_target == null) yield break;
            _target.color = _colors.pressedColor;
            yield return new WaitForSeconds(0.1f);
            _target.color = _isHovered ? _colors.pressedColor : _colors.normalColor;
            onClick?.Invoke();
            yield return new WaitForSeconds(2f);
            onClickEndCam?.Invoke();
        }
    }
}