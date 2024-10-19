using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public int id;
    [SerializeField] private List<PlatformLinker> _linkers;
    private PlatformsManager _platformsManager;


    public void InjectPlatformsManager(PlatformsManager pm)
    {
        _linkers = GetComponentsInChildren<PlatformLinker>().ToList();

        _platformsManager = pm;
        foreach (var link in _linkers)
            link.InjectRefs(pm, id);
    }
    private void OnEnable()
    {
//
    }

    private void OnDisable()
    {
        //
    }
}