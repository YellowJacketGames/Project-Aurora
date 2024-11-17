using System;
using System.Collections;
using UnityEngine;

namespace Platforms
{
    public class WaterDropObstacle : MonoBehaviour
    {
        [SerializeField] private GameObject waterdropPrefab;
        [SerializeField] private float dropWaitTime = 2f;
        private float dropElapsedTime=0f;
        [SerializeField] private Transform dropperStartPos;
        [SerializeField] private ObjectPooling _poolingManager;

        private void Start()
        {
            _poolingManager.CreateNewPool(new Pool("waterDrops", waterdropPrefab, 10));
            NewDrop();
        }

        private IEnumerator WaitToWaterDrop()
        {
            dropElapsedTime += Time.deltaTime;
            if (dropElapsedTime >= dropWaitTime)
            {
                _poolingManager.SpawnFromPool("waterDrops", dropperStartPos.position, Quaternion.identity);
                dropElapsedTime = 0f;
                yield return null;
            }

            yield return new WaitForEndOfFrame();
            NewDrop();
        }

        private void NewDrop()
        {
            StartCoroutine(WaitToWaterDrop());

        }

        private void OnDrawGizmos()
        {
            if(!dropperStartPos) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(dropperStartPos.position, 0.2f);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 0.6f);
                
        }
    }
}