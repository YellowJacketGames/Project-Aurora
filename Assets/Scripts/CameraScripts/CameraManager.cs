using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

//This script will handle the camera methods as well as hold references
//To all the cinemachina camera reference in the current scene for
//Transitions and different elements
public class CameraManager : MonoBehaviour
{
    //Variables of the different cameras in scene
    [Header("Current Cameras")]
    [SerializeField] CinemachineVirtualCamera levelCamera;
    [SerializeField] CinemachineVirtualCamera dialogueCamera;

    private CinemachineVirtualCamera currentCamera;
    [Space]
    [Header("Camera Variables")]
    [SerializeField] private float zoomInValue;
    [SerializeField] private float zoomOutValue;
    
    private bool zoomActivate;
    private bool zoomed = false;

    private float zoomTime;
    [SerializeField] private float zoomDuration;
    private void Start()
    {
        #region Give Game Manager Reference

        GameManager.instance.currentCameraManager = this;

        #endregion
        //Every level will begin as a level so we set the priority to the level camera on Start
        ChangeToLevelCamera();
    }


    public void Update()
    {
        if(GameManager.instance.currentController.CurrentPlayerState != PlayerState.Conversation || GameManager.instance.currentController.CurrentPlayerState != PlayerState.Transition)
        {
            if (GameManager.instance.currentController.playerInputHandlerComponent.GetToggleZoomInput())
            {
                if (zoomActivate)
                {
                    zoomed = !zoomed;
                }
                else
                {
                    zoomActivate = true;
                }
            }
        }

        if (zoomActivate)
        {
            if (!zoomed)
            {
                zoomTime += Time.deltaTime;

            }
        }
    }
    #region Camera Methods

    public void ChangeToLevelCamera()
    {
        currentCamera = levelCamera;
        levelCamera.Priority = 1;
        dialogueCamera.Priority = 0;
    }

    public void ChangeToDialogueCamera()
    {
        currentCamera = dialogueCamera;
        dialogueCamera.Priority = 1;
        levelCamera.Priority = 0;
    }

    #endregion
}
