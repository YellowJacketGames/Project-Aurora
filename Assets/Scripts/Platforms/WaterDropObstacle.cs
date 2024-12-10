using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platforms
{
    public class WaterDropObstacle : MonoBehaviour
    {
        [SerializeField] private Transform newPosition;

        [SerializeField] private GameObject waterdropPrefab;
        [SerializeField] private float initialDelay = 0f;
        [SerializeField] private float dropWaitTime = 2f;
        [SerializeField] private int poolSize = 4;
        private float dropElapsedTime = 0f;
        [SerializeField] private Transform dropperStartPos;
        [SerializeField] private ObjectPooling _poolingManager;

        private List<GameObject> waterDrops;

        private void Start()
        {
            _poolingManager.CreateNewPool(new Pool("waterDrops_" + gameObject.name, waterdropPrefab, poolSize));
            waterDrops = _poolingManager.GetPool("waterDrops_" + gameObject.name).ToList();
            foreach (var drop in waterDrops)
                drop.GetComponent<WaterDropEntity>().InjectResetPosRef(newPosition);
            StartCoroutine(InitialDelay());
        }

        private IEnumerator InitialDelay()
        {
            yield return new WaitForSeconds(initialDelay);
            NewDrop();
        }

        private IEnumerator WaitToWaterDrop()
        {
            dropElapsedTime += Time.deltaTime;
            if (dropElapsedTime >= dropWaitTime)
            {
                _poolingManager.SpawnFromPool("waterDrops_" + gameObject.name, dropperStartPos.position,
                    Quaternion.identity);
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
            if (!dropperStartPos) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(dropperStartPos.position, 0.2f);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 0.6f);


            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(newPosition.position, 0.2f);
        }
    }
}