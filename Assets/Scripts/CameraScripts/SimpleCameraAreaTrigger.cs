using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SimpleCameraAreaTrigger : MonoBehaviour
{
    [SerializeField] private SimpleCamAreaTType type;
    [SerializeField] private bool enableBothDir;
    [SerializeField] private bool shouldRestorePreviousMovementType;
    [SerializeField] protected CinemachineVirtualCamera cameraArea;
    [SerializeField] protected CinemachineVirtualCamera dialogueCamera;
    [SerializeField] private FreezePlayerXTime _freezePlayerXTime;
    [SerializeField] private PlayerMovement.MovementType newMovementType;
    [SerializeField] private PlayerMovement.MovementDirection newMovementDirection;
    [SerializeField] private PlayerMovement.MovementType restorableType;
    [SerializeField] private PlayerMovement.MovementDirection restorableDirection;


    [SerializeField] private List<GameObject> enableOnEnter;
    [SerializeField] private List<GameObject> disableOnEnter;


    [SerializeField] private List<GameObject> enableOnExit;
    [SerializeField] private List<GameObject> disableOnExit;
    [SerializeField] private bool freezePlayerOnEnter;


    private void Awake()
    {
        _freezePlayerXTime = GetComponentInParent<FreezePlayerXTime>();

        if (!GameManager.instance.currentCameraManager) return;
        if (!cameraArea) return;
        if (!GameManager.instance.currentCameraManager.otherCameras.Contains(cameraArea))
            GameManager.instance.currentCameraManager.otherCameras.Add(cameraArea);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (dialogueCamera)
            GameManager.instance.currentCameraManager.AssignDialogueCamera(dialogueCamera, cameraArea);
        else GameManager.instance.currentCameraManager.AssignDialogueCamera(cameraArea);

        if (type.Equals(SimpleCamAreaTType.ENTER))
            ManageEnterType();
        else
            ManageExitType();
        if (!freezePlayerOnEnter) return;
        _freezePlayerXTime.TryFreeze();
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
        GameManager.instance.currentController.playerMovementComponent.ChangeMovementDirection(restorableType,
            restorableDirection);
    }

    private void TrySetNewCamera()
    {
        if (!cameraArea) return;
        GameManager.instance.currentCameraManager.ChangeToCameraArea();
        cameraArea.Priority = 1;
    }

    private void ReturnOldCamera()
    {
        if (!cameraArea)
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
        GameManager.instance.currentController.playerMovementComponent.ChangeMovementDirection(newMovementType,
            newMovementDirection);
    }
}

public enum SimpleCamAreaTType
{
    ENTER,
    EXIT,
}