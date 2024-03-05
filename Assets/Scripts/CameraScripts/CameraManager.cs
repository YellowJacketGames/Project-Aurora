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

    //The camera that is set when the player is moving to the right
    [SerializeField] CinemachineVirtualCamera levelCameraRight;
    private CinemachineFramingTransposer transposerRight;

    //The camera that is set when the player is moving to the left
    [SerializeField] CinemachineVirtualCamera levelCameraLeft;
    private CinemachineFramingTransposer transposerLeft;

    //Camera for dialogues
    [SerializeField] CinemachineVirtualCamera dialogueCamera;

    //Variable to store the current camera being used
    private CinemachineVirtualCamera currentCamera;

    //Variable to store the current camera state when we change to a different area.
    private CameraStates previousCameraState;

    [Space]
    [Header("Camera Variables")]
    [Space]
    [Header("Zoom")]
    [SerializeField] private float zoomInValue;
    [SerializeField] private float zoomOutValue;

    private CameraStates currentCameraState;
    private bool zoomActivate;
    private bool zoomed = false;

    [SerializeField] private float zoomTime;
    [SerializeField] private float zoomDuration;

    [Space]
    [Header("Changing Left/Right")]
    [SerializeField] private float changeTimer;
    [SerializeField] private float _changeTimer;

    bool changeToRight;
    bool changeToLeft;

    bool right;
    bool left;

    bool shouldToggle => GameManager.instance.currentController.CurrentPlayerState != PlayerState.Conversation && GameManager.instance.currentController.CurrentPlayerState != PlayerState.Transition
        && GameManager.instance.CanPlay();
    private void Start()
    {
        #region Give Game Manager Reference

        GameManager.instance.currentCameraManager = this;

        #endregion

        //We set the first camera state to player follow so that it sets the camera to follow the player
        currentCameraState = CameraStates.PlayerFollow;

        //Every level will begin as a level so we set the priority to the level camera on Start and to the right
        GetFollowCamerasTransposers();
        SetFollowCameraRight();


        //We set this variables for testing purposes, it will depend later on each level
        zoomActivate = false;
        zoomed = true;
    }


    public void Update()
    {
        //If the camera is following the player, we can toggle zoom and look to the left and right.
        if(currentCameraState == CameraStates.PlayerFollow)
        {
            //This area handles the toggle bewteen zoom in and out.
            #region Zoom in and out

            //if the player isn't in both states that set a different camera, we can perform the toggle.
            if (shouldToggle)
            {
                //if the player does the toggle input
                if (GameManager.instance.currentController.playerInputHandlerComponent.GetToggleZoomInput())
                {
                    //If it's already activated, we just switch the value to do the opposite thing.
                    //We also reset the timer
                    if (zoomActivate)
                    {
                        zoomed = !zoomed;
                        zoomTime = 0;
                    }

                    //If not, we activate the zoom
                    else
                    {
                        zoomActivate = true;
                    }
                }
            }
            

            //If zoom is active we do the appropiate lerp.
            if (zoomActivate)
            {

                //if camera is zoomed out, we zoom it in.
                if (!zoomed)
                {
                    //We do a simple lerp between the camera distance and the value we want.
                    zoomTime += Time.deltaTime;
                    float currentZoomValue = Mathf.Lerp(transposerLeft.m_CameraDistance, zoomInValue, zoomTime / zoomDuration); //We get the value from the left camera but it could be either one.

                    //We change the value of both cameras since it could be any of the active, or change during the zoom in and zoom out
                    transposerLeft.m_CameraDistance = currentZoomValue;
                    transposerRight.m_CameraDistance = currentZoomValue;


                    //If the lerp is done, we reset the activate, set the proper camera state and reset the timer.
                    if (zoomTime >= zoomDuration)
                    {
                        zoomTime = 0;
                        zoomActivate = false;
                        zoomed = true;
                    }
                }

                //If camera is zoomed in, we zoom it out.
                else
                {
                    //We do a simple lerp between the camera distance and the value we want.
                    zoomTime += Time.deltaTime;
                    float currentZoomValue = Mathf.Lerp(transposerLeft.m_CameraDistance, zoomOutValue, zoomTime / zoomDuration); //We get the value from the left camera but it could be either one.

                    //We change the value of both cameras since it could be any of the active, or change during the zoom in and zoom out
                    transposerLeft.m_CameraDistance = currentZoomValue;
                    transposerRight.m_CameraDistance = currentZoomValue;

                    //If the lerp is done, we reset the activate, set the proper camera state and reset the timer.
                    if (zoomTime >= zoomDuration)
                    {
                        zoomTime = 0;
                        zoomActivate = false;
                        zoomed = false;
                    }
                }
            }

            #endregion

            //This area handles the change in the camera priorities
            #region Change Between Right and Left

            //If we wanted to change to the right
            if (changeToRight)
            {
                //We begin a timer to check if the player has moved enough to change the camera
                _changeTimer -= Time.deltaTime;
                
                if(_changeTimer <= 0) //if it's done, we deactivate the check, reset the timer and set the camera priority to the right.
                {
                    changeToRight = false;
                    _changeTimer = changeTimer;
                    SetFollowCameraRight();
                }
            }

            //If we wanted to change to the left
            if (changeToLeft)
            {
                //We begin a timer to check if the player has moved enough to change the camera
                _changeTimer -= Time.deltaTime;

                if (_changeTimer <= 0) //if it's done, we deactivate the check, reset the timer and set the camera priority to the left.
                {
                    changeToRight = false;
                    _changeTimer = changeTimer;
                    SetFollowCameraLeft();
                }
            }
            #endregion
        }

    }


    #region Camera Methods

    public void GetFollowCamerasTransposers() //This method gets the camera framing transposers to change the camera distance for the zooms.
    {
        transposerLeft = levelCameraLeft.GetCinemachineComponent<CinemachineFramingTransposer>();
        transposerRight = levelCameraRight.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    public void ChangeCurrentCameraState(CameraStates currentState) //This just sets the current state to a new one through the parameter.
    {
        currentCameraState = currentState;
    }

    public void LoseReferences()
    {
        levelCameraLeft.Follow = null;
        levelCameraRight.Follow = null;
    }

    public void ReturnReferences()
    {
        levelCameraLeft.Follow = GameManager.instance.currentController.transform;
        levelCameraRight.Follow = GameManager.instance.currentController.transform;
    }
    //This region holds all the methods related to camera timers
    #region Camera Timers
    public void SetCameraRightTimer() //We set the camera timer to the right.
    {
        if (right) //If this method's been called, and the player is already moving to the right, it means the player has cancelled his movement to the other direction, so we reset the direction.
        {
            changeToLeft = false; //we deactivate the check.
            _changeTimer = changeTimer; //We reset the timer.
        }
        else //if it's false, it means the player is moving to the opposite direction, so we activate the timer.
        {
            changeToRight = true;  //We activate the check.
        }

    }
    public void SetCameraLeftTimer()//We set the camera timer to the left.
    {
        if (left) //If this method's been called, and the player is already moving to the left, it means the player has cancelled his movement to the other direction, so we reset the direction.
        {
            changeToRight = false; //we deactivate the check.
            _changeTimer = changeTimer; //We reset the timer.
        }
        else //if it's false, it means the player is moving to the opposite direction, so we activate the timer.
        {
            changeToLeft = true; //We activate the check.
        }
    }

    //This is a method to call from the enter idle state in the player controller.
    //If the player stops moving, it isn't moving in any direction.
    //So we need to cancel any change direction timer.
    public void StopCameraTimer() 
    {
        changeToRight = false;
        changeToLeft = false;

        _changeTimer = changeTimer;
    }

    #endregion

    //This region holds all the methods related to setting the priorities of the cameras stored in the manager.
    #region Set Different Cameras
    public void SetFollowCameraRight() //We change the active camera to the dialogue camera.
    {
        ResetAllPriorities(); //We reset all priorities to clean the slate.
        ChangeCurrentCameraState(CameraStates.PlayerFollow); //We set the current state to Follow

        currentCamera = levelCameraRight;  //We store the camera in the currentCamera variable (this doesn't have a use yet but it will be useful if we make transitions or camera movement from this state).
        levelCameraRight.Priority = 1;//We set the priority to a higher value than the rest.

        //We set the proper direction and deactivate the other one
        right = true;
        left = false;
    }

    public void SetFollowCameraLeft() //We change the active camera to the dialogue camera.
    {
        ResetAllPriorities(); //We reset all priorities to clean the slate.
        ChangeCurrentCameraState(CameraStates.PlayerFollow); //We set the current state to Follow

        currentCamera = levelCameraLeft; //We store the camera in the currentCamera variable (this doesn't have a use yet but it will be useful if we make transitions or camera movement from this state).

        levelCameraLeft.Priority = 1; //We set the priority to a higher value than the rest.

        //We set the proper direction and deactivate the other one
        left = true;
        right = false;
    }

    public void SetDialogueCamera() //We change the active camera to the dialogue camera.
    {
        ResetAllPriorities(); //We reset all priorities to clean the slate.
        ChangeCurrentCameraState(CameraStates.Conversation); //We set the current state to Conversation
        currentCamera = dialogueCamera; //We store the camera in the currentCamera variable (this doesn't have a use yet but it will be useful if we make transitions or camera movement from this state).
        dialogueCamera.Priority = 1; //We set the priority to a higher value than the rest.
    }

    

    public void ResetAllPriorities() //This sets all the priorities to 0.
    {
        levelCameraLeft.Priority = 0;
        levelCameraRight.Priority = 0;
        dialogueCamera.Priority = 0;
    }

    #endregion

    //This region holds all the methods related to changing to different camera areas.
    #region Camera Areas
    public void ChangeToCameraArea() //Method to use when changing into a different camera area.
    {
        ResetAllPriorities(); //We reset all priorities.
        previousCameraState = currentCameraState; //We set the previous camera to go back when we leave the area.
        ChangeCurrentCameraState(CameraStates.PlayerLook); //We set the current state to playerlook, this will change in the future if we have different types of cameras.
    }

    public void ReturnFromCameraArea() //Method to use when going back from the area.
    {
        ResetAllPriorities(); //We reset all priorities.

        ChangeCurrentCameraState(previousCameraState); //We go back to the previous state.

        switch (currentCameraState) //Depending on the previous state, we will change to a different camera.
        {
            case CameraStates.PlayerFollow:
                if (right)
                {
                    SetFollowCameraRight();
                }
                if (left)
                {
                    SetFollowCameraLeft();
                }
                break;
            case CameraStates.Conversation:
                SetDialogueCamera();
                break;
            default:
                break;
        }
    }
    #endregion

    #endregion
}
