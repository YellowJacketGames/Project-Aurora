using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

namespace PlayerScripts
{
    public class CameraManagerRelativeToMovement : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        private CinemachineOrbitalTransposer _orbitalTransposer;
        [SerializeField] private float lerpDuration = 1.0f;
        private bool _lerping;

        private Coroutine lerpCoroutine;

        private void Awake()
        {
            _camera = GetComponent<CinemachineVirtualCamera>();
            _orbitalTransposer = _camera.GetCinemachineComponent<CinemachineOrbitalTransposer>();
            GameManager.instance.currentController.playerMovementComponent.relativeToMovementSimpleCamera = this;
        }

        public void SetCameraInBack()
        {
            StartLerping(0);
        }

        public void SetCameraInFront()
        {
            StartLerping(180);
        }

        private void StartLerping(float targetBias)
        {
            if (lerpCoroutine != null)
                StopCoroutine(lerpCoroutine);
            lerpCoroutine = StartCoroutine(LerpBias(targetBias));
        }

        private IEnumerator LerpBias(float targetBias)
        {
            _lerping = true;
            var startBias = _orbitalTransposer.m_Heading.m_Bias;
            var elapsedTime = 0f;

            while (elapsedTime < lerpDuration)
            {
                elapsedTime += Time.deltaTime;
                var t = Mathf.Clamp01(elapsedTime / lerpDuration);
                _orbitalTransposer.m_Heading.m_Bias = Mathf.Lerp(startBias, targetBias, t);
                yield return null;
            }

            _lerping = false;
            _orbitalTransposer.m_Heading.m_Bias = targetBias;
        }
    }
}