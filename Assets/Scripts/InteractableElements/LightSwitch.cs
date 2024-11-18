using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class LightSwitch : InteractableElement
{
    [SerializeField] private List<Light> connectedLights;
    [SerializeField] private float lightsOffTimeout = 2.5f;

    [SerializeField] private bool switchesToCamera;
    [SerializeField] private CinemachineVirtualCamera lightsPlaneCamera;
    [SerializeField] private CinemachineVirtualCamera levelCamera;
    private Camera camera;


    private float elapsedTime = 0f;
    private bool isWaiting;

    protected override void Awake()
    {
        base.Awake();
        camera = Camera.main;
    }

    private void Start()
    {
        foreach (var lightObject in connectedLights)
            lightObject.gameObject.SetActive(false);
    }

    public override void OnInteract()
    {
        if (isWaiting) return;
        HideInteractPrompt();
        ignorePopup = true;

        if (switchesToCamera)
        {
            SetLightsCamera();
        }

        StartCoroutine(WaitToShutLights());
    }

    private void SetLightsCamera()
    {
        if (!lightsPlaneCamera || !levelCamera) return;
        lightsPlaneCamera.Priority = 5;
        levelCamera.Priority = 0;
    }


    private void ResetMainCamera()
    {
        if (!lightsPlaneCamera || !levelCamera) return;
        lightsPlaneCamera.Priority = 0;
        levelCamera.Priority = 5;
    }

    private IEnumerator WaitToShutLights()
    {
        isWaiting = true;
        if (switchesToCamera)
            yield return new WaitForSeconds(2f); //that's the ease out setted in cinemachine brain mainCamera

        foreach (var lightObject in connectedLights)
            lightObject.gameObject.SetActive(true);
        while (elapsedTime <= lightsOffTimeout)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        foreach (var lightObject in connectedLights)
            lightObject.gameObject.SetActive(false);
        isWaiting = false;
        ignorePopup = false;
        ResetMainCamera();
        yield return null;
    }
}