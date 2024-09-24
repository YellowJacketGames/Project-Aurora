using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
//This script handles a camera in level that is set to maximum priority
//When the player enters the area designated by a collider.
//When it leaves the collider, it resets to a previous camera.

public class CameraArea : MonoBehaviour
{
    [Header("Camera area variables")]
    [SerializeField] private CinemachineVirtualCamera areaCamera;

    bool inArea;
    protected virtual  void ChangeToArea()
    {
        GameManager.instance.currentCameraManager.ChangeToCameraArea();
        areaCamera.Priority = 1;
        inArea = true;
    }

    protected virtual void ExitArea()
    {
        areaCamera.Priority = 0;
        GameManager.instance.currentCameraManager.ReturnFromCameraArea();
        inArea = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (inArea)
            return;

        if (other.CompareTag("Player"))
        {
            ChangeToArea();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (!inArea)
            return;

        if (other.CompareTag("Player"))
        {
            ExitArea();
        }
    }
}
