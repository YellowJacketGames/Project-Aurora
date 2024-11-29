using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

//This script handles a camera in level that is set to maximum priority
//When the player enters the area designated by a collider.
//When it leaves the collider, it resets to a previous camera.

public class CameraArea : MonoBehaviour
{
    [Header("Camera area variables")] [SerializeField]
    protected CinemachineVirtualCamera areaCamera;

    [SerializeField] bool inArea;
    public bool disableInputOnEnter;
    private void Awake()
    {
        gameObject.tag = "CameraArea";

    }

    protected virtual void ChangeToArea()
    {
        if (!areaCamera) return;

        GameManager.instance.currentCameraManager.ChangeToCameraArea();
       if(disableInputOnEnter) GameManager.instance.currentController.playerMovementComponent.DisableAllInput();
        areaCamera.Priority = 1;
        inArea = true;
    }

    protected virtual void ExitArea()
    {
        if (!areaCamera) return;

        Debug.LogError("Area exited");
        areaCamera.Priority = 0;
        GameManager.instance.currentCameraManager.ReturnFromCameraArea();
     if(disableInputOnEnter)   GameManager.instance.currentController.playerMovementComponent.EnableAllInput();
        inArea = false;
    }


    protected virtual void OnTriggerEnter(Collider other)
    {
        if (inArea)
            return;

        if (other.CompareTag("Player"))
        {
            ChangeToArea();
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (!inArea)
            return;

        if (other.CompareTag("Player"))
        {
            ExitArea();
        }
    }
}