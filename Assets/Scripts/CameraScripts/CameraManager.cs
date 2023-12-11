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

    private void Start()
    {
        #region Give Game Manager Reference

        GameManager.instance.currentCameraManager = this;

        #endregion
        //Every level will begin as a level so we set the priority to the level camera on Start
        ChangeToLevelCamera();
    }
    
    #region Camera Methods

    public void ChangeToLevelCamera()
    {
        levelCamera.Priority = 1;
        dialogueCamera.Priority = 0;
    }

    public void ChangeToDialogueCamera()
    {
        dialogueCamera.Priority = 1;
        levelCamera.Priority = 0;
    }

    #endregion
}
