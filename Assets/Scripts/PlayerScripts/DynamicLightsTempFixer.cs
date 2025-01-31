using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DynamicLightsTempFixer : MonoBehaviour
{
    [SerializeField] private List<Light> levelLights;

    // [SerializeField] private float lightDetectionRange = 20f;
    [SerializeField] private int maxLightsActive = 10;
     [SerializeField] private int currentLightsActive;

    private HashSet<Light> activeLights = new HashSet<Light>();
    private Camera camRef;
    private bool _iscamRefNull;

    private void Start()
    {
        _iscamRefNull = camRef == null;
    }

    private void Awake()
    {
        levelLights = FindObjectsOfType<Light>().ToList();
        // currentLightsActive = 0;
        foreach (var light in levelLights)
        {
            light.gameObject.SetActive(false);
        }

        camRef = Camera.main;
    }

    private void Update()
    {
        CheckVisibleLights();
        // CheckDistances();
    }


    private void CheckVisibleLights()
    {
        if (_iscamRefNull) return;

        Plane[] cameraFrustum = GeometryUtility.CalculateFrustumPlanes(camRef);
        currentLightsActive = activeLights.Count;

        foreach (var light in levelLights)
        {
            if (light is not { type: not LightType.Directional }) continue;

            Bounds lightBounds = GetLightBounds(light);

            if (GeometryUtility.TestPlanesAABB(cameraFrustum, lightBounds))
            {
                if (!activeLights.Contains(light) && currentLightsActive < maxLightsActive)
                {
                    ViewLight(light);
                    currentLightsActive++;
                }
            }
            else if (activeLights.Contains(light))
            {
                HideLight(light);
                currentLightsActive--;
            }
        }
    }

    private Bounds GetLightBounds(Light light)
    {
        switch (light.type)
        {
            case LightType.Point:
                return new Bounds(light.transform.position, Vector3.one * (light.range * 2));
            case LightType.Spot:
                float spotRadius = Mathf.Tan(light.spotAngle * 0.5f * Mathf.Deg2Rad) * light.range;
                Vector3 size = new Vector3(spotRadius * 2, spotRadius * 2, light.range * 2);
                return new Bounds(light.transform.position + light.transform.forward * (light.range / 2), size);
            default:
                return new Bounds(light.transform.position, Vector3.one);
        }
    }

    private void ViewLight(Light light)
    {
        light.gameObject.SetActive(true);
        activeLights.Add(light);
    }

    private void HideLight(Light light)
    {
        light.gameObject.SetActive(false);
        activeLights.Remove(light);
    }

    private void OnDrawGizmos()
    {
        if (Camera.main == null) return;

        Plane[] cameraFrustum = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        foreach (var light in levelLights)
        {
            if (light == null || light.type == LightType.Directional) continue;

            Bounds lightBounds = GetLightBounds(light);
            bool isInFrustum = GeometryUtility.TestPlanesAABB(cameraFrustum, lightBounds);
            bool isActive = activeLights.Contains(light);

            Gizmos.color = isActive ? Color.blue : (isInFrustum ? Color.green : Color.red);
            Gizmos.DrawWireCube(lightBounds.center, lightBounds.size);
        }
    }

    // private void CheckDistances()
    // {
    //     var activeLights = new List<Light>();
    //     foreach (var light in levelLights)
    //     {
    //         float distance = Vector3.Distance(transform.position, light.transform.position);
    //         if (distance <= lightDetectionRange)
    //         {
    //             if (!light.gameObject.activeSelf && currentLightsActive < maxLightsActive)
    //                 ViewLight(light);
    //             if (light.gameObject.activeSelf)
    //                 activeLights.Add(light);
    //         }
    //         else if (light.gameObject.activeSelf)
    //             HideLight(light);
    //     }
    //
    //     currentLightsActive = activeLights.Count;
    // }

    // private void ViewLight(Light light)
    // {
    //     if (light.gameObject.activeSelf) return;
    //     light.gameObject.SetActive(true);
    //     currentLightsActive++;
    // }
    //
    // private void HideLight(Light light)
    // {
    //     if (!light.gameObject.activeSelf) return;
    //     light.gameObject.SetActive(false);
    //     currentLightsActive--;
    // }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.magenta;
    //     Gizmos.DrawWireSphere(transform.position, lightDetectionRange);
    // }
}