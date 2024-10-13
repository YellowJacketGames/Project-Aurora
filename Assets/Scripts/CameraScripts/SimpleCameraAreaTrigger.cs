using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


public class SimpleCameraAreaTrigger : MonoBehaviour
{
    [SerializeField] private SimpleCamAreaTType type;
    [SerializeField] private bool enableBothDir;
    [SerializeField] private bool shouldRestorePreviousMovementType;
    [SerializeField] protected CinemachineVirtualCamera cameraArea;
    [SerializeField] private PlayerMovement.MovementType newMovementType;
    [SerializeField] private PlayerMovement.MovementDirection newMovementDirection;
    private PlayerMovement.MovementType _initialMovementType;
    private PlayerMovement.MovementDirection _initialMovementDirection;


    [SerializeField] private List<GameObject> enableOnEnter;
    [SerializeField] private List<GameObject> disableOnEnter;


    [SerializeField] private List<GameObject> enableOnExit;
    [SerializeField] private List<GameObject> disableOnExit;

    private void Awake()
    {
        if (!GameManager.instance.currentCameraManager) return;
        if (!cameraArea) return;
        if (!GameManager.instance.currentCameraManager.otherCameras.Contains(cameraArea))
            GameManager.instance.currentCameraManager.otherCameras.Add(cameraArea);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (type.Equals(SimpleCamAreaTType.ENTER))
            ManageEnterType();
        else
            ManageExitType();
    }

    private void ManageEnterType()
    {
        TrySetNewCamera();
        TryUpdatePlayerMovement();
        foreach (var obj in enableOnEnter)
            obj.SetActive(true);

        foreach (var obj in disableOnEnter)
            obj.SetActive(false);
    }

    private void ManageExitType()
    {
        ReturnOldCamera();
        TryRestoreMovement();
        foreach (var obj in enableOnExit)
            obj.SetActive(true);

        foreach (var obj in disableOnExit)
            obj.SetActive(false);
    }

    private void TryRestoreMovement()
    {
        GameManager.instance.currentController.playerMovementComponent.horizontalAndVerticalMovement = enableBothDir;
        if (!shouldRestorePreviousMovementType) return;
        GameManager.instance.currentController.playerMovementComponent.ChangeMovementDirection(_initialMovementType,
            _initialMovementDirection);
    }

    private void TrySetNewCamera()
    {
        if (!cameraArea) return;
        GameManager.instance.currentCameraManager.ChangeToCameraArea();
        cameraArea.Priority = 1;
    }

    private void ReturnOldCamera()
    {
        if(!cameraArea)
        {
            cameraArea.Priority = 0;
            GameManager.instance.currentCameraManager.ReturnFromCameraArea();
        }
        else
            TrySetNewCamera();
        
    }

    private void TryUpdatePlayerMovement()
    {
        GameManager.instance.currentController.playerMovementComponent.horizontalAndVerticalMovement = enableBothDir;
        (_initialMovementType, _initialMovementDirection) =
            GameManager.instance.currentController.playerMovementComponent.GetMovementDirection();
        GameManager.instance.currentController.playerMovementComponent.ChangeMovementDirection(newMovementType,
            newMovementDirection);
    }
}

public enum SimpleCamAreaTType
{
    ENTER,
    EXIT,
}