using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(BoxCollider))]
public class PlatformLinker : MonoBehaviour
{
    public int platformId;
    public Transform spawningPosition;
    [SerializeField] private Color gizmosColor;
    public BoxCollider collider;
    public PlatformLinker platformLinkerTarget;

    private float _waitForTriggersTime = 1.0f;
    private float _elapsedTimeWaitForTriggers;
    private PlatformsManager _platformsManager;
    private bool triggersCanDetect = false;
    private Coroutine _waitCorr;

    private void Awake()
    {
        collider = GetComponent<BoxCollider>();
        if (!spawningPosition)
            spawningPosition = transform;
    }

    private void OnEnable()
    {
        if (_waitCorr != null)
            StopCoroutine(_waitCorr);
        _waitCorr = StartCoroutine(WaitToActivate());
    }

    private void OnDisable()
    {
        triggersCanDetect = false;
    }

    private IEnumerator WaitToActivate()
    {
        triggersCanDetect = false;
        while (_elapsedTimeWaitForTriggers < _waitForTriggersTime)
        {
            _elapsedTimeWaitForTriggers += Time.deltaTime;
            yield return null;
        }

        _elapsedTimeWaitForTriggers = 0f;
        triggersCanDetect = true;
        yield break;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!triggersCanDetect) return;
        if (!other.CompareTag("Player")) return;
        _platformsManager.LoadPlatformWithId(platformLinkerTarget.platformId, platformLinkerTarget);
    }

    public void InjectRefs(PlatformsManager pm, int pid)
    {
        _platformsManager = pm;
        platformId = pid;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor; //Color.green;

        if (collider != null)
        {
            var colliderCenter = collider.bounds.center;
            var colliderSize = collider.size;
            Gizmos.DrawWireCube(colliderCenter, colliderSize);
            if (platformLinkerTarget != null)
                if (platformLinkerTarget.collider != null)
                    Gizmos.DrawLine(colliderCenter, platformLinkerTarget.collider.bounds.center);
        }

        Gizmos.color = Color.red;
        if (spawningPosition != null)
            Gizmos.DrawWireSphere(spawningPosition.position, 0.25f);
    }
}