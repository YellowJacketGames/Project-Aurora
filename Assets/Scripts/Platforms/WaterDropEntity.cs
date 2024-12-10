using System;
using System.Collections;
using UnityEngine;

namespace Platforms
{
    [RequireComponent(typeof(Collider))]
    public class WaterDropEntity : MonoBehaviour
    {
        [SerializeField] private string tag;

        [SerializeField] private float initialSpeed = 0.5f;
        [SerializeField] private float acceleration = 9.8f;
        private ObjectPooling _objectPooling;
        private bool isFalling;
        private float currentSpeed;

        private Transform newPosition;

        public void SetRef(ObjectPooling op, string poolTag)
        {
            _objectPooling = op;
            tag = poolTag;
        }

        public void InjectResetPosRef(Transform position)
        {
            newPosition = position;
        }

        private void OnEnable()
        {
            isFalling = true;
            currentSpeed = initialSpeed;
            StartCoroutine(Fall());
        }

        private void OnDisable()
        {
            isFalling = false;
            StopCoroutine(Fall());
        }

        private IEnumerator Fall()
        {
            while (isFalling)
            {
                currentSpeed += acceleration * Time.deltaTime;
                transform.position += Vector3.down * (currentSpeed * Time.deltaTime);
                yield return null;
            }

            yield return null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name.Equals("PlayerInteractor")) return;

            if (other.gameObject.CompareTag("Player"))
            {
                // EventsManager.OnWaterDropHitPlayer?.Invoke();
                SendPlayerToCheckpoint();
                _objectPooling.ReturnToPool(gameObject);
                Debug.Log("Player water hit!");
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
                _objectPooling.ReturnToPool(gameObject);
        }

        private void SendPlayerToCheckpoint()
        {
            GameManager.instance.currentTransitionManager.CompleteTransition();
            GameManager.instance.currentCameraManager.LoseReferences();
            GameManager.instance.currentController.playerMovementComponent.DisableAllInput();
            Invoke("ChangePlayerPosition", 2f);
        }

        private void ChangePlayerPosition()
        {
            GameManager.instance.currentController.playerRigid.isKinematic = true;
            GameManager.instance.currentController.transform.position = newPosition.position;
            GameManager.instance.currentCameraManager.ReturnReferences();
            GameManager.instance.currentController.playerMovementComponent.EnableAllInput();
            GameManager.instance.currentController.ChangeState(PlayerState.Idle);
            GameManager.instance.currentController.playerRigid.isKinematic = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, 0.15f);
        }
    }
}