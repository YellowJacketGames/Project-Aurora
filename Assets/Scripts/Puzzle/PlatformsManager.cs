using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsManager : MonoBehaviour
{
    [SerializeField] private List<Platform> _platforms;
    [SerializeField] private bool _debugging;

    private int ref_id;
    private PlatformLinker ref_linker;

    private void Awake()
    {
        foreach (var platform in transform.GetComponentsInChildren<Platform>())
        {
            platform.InjectPlatformsManager(this);
            _platforms.Add(platform);
        }
    }

    private void OnEnable()
    {
        GameManager.instance.currentTransitionManager.onTransitionFinished.AddListener(LoadPlatformAfterTransition);
    }

    private void OnDisable()
    {
        GameManager.instance.currentTransitionManager.onTransitionFinished.RemoveListener(LoadPlatformAfterTransition);
    }

    private void LoadPlatformAfterTransition()
    {
        LoadPlatform(_platforms.First(platform => platform.id == ref_id));
        GameManager.instance.currentController.transform.position = ref_linker.spawningPosition.position;
        Invoke("TransitionDelay", 0.2f);
    }

    public void LoadPlatformWithId(int id, PlatformLinker linker)
    {
        //camera fading here 
        GameManager.instance.currentTransitionManager.SetFadeIn();
        GameManager.instance.currentController.playerMovementComponent.FreezePlayer();
        ref_id = id;
        ref_linker = linker;
    }


    private void LoadPlatform(Platform platform)
    {
        foreach (var p in _platforms)
            p.gameObject.SetActive(false);

        if (!_debugging)
            platform.transform.position = Vector3.zero;
        platform.gameObject.SetActive(true);
    }


    private void TransitionDelay()
    {
        GameManager.instance.currentTransitionManager.SetFadeOut();
    }
}