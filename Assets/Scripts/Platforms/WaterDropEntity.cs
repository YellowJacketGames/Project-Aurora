using System;
using System.Collections;
using UnityEngine;

namespace Platforms
{
    [RequireComponent(typeof(Collider))]
    public class WaterDropEntity : MonoBehaviour
    {
        [SerializeField] private float initialSpeed = 0.5f;
        [SerializeField] private float acceleration = 9.8f;
        private ObjectPooling _objectPooling;
        private bool isFalling;
        private float currentSpeed;

        public void SetRef(ObjectPooling op)
        {
            _objectPooling = op;
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
                EventsManager.OnWaterDropHitPlayer?.Invoke();
                _objectPooling.ReturnToPool(gameObject);
                Debug.Log("Player water hit!");
            }
            else
                _objectPooling.ReturnToPool(gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, 0.15f);
        }
    }
}