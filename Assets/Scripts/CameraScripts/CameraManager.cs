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
    [SerializeField] CinemachineVirtualCamera levelCameraRight;
    private CinemachineFramingTransposer transposerRight;

    [SerializeField] CinemachineVirtualCamera levelCameraLeft;
    private CinemachineFramingTransposer transposerLeft;

    [SerializeField] CinemachineVirtualCamera dialogueCamera;

  
    [Space]
    [Header("Camera Variables")]
    [SerializeField] private float zoomInValue;
    [SerializeField] private float zoomOutValue;
    private CameraStates currentCameraState;
    private bool zoomActivate;
    private bool zoomed = false;

    [SerializeField] private float zoomTime;
    [SerializeField] private float zoomDuration;
    [SerializeField] private float changeTimer;
    private float _changeTimer;

    bool changeToRight;
    bool changeToLeft;

    bool right;
    bool left;
    private void Start()
    {
        #region Give Game Manager Reference

        GameManager.instance.currentCameraManager = this;

        #endregion

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
        if(currentCameraState == CameraStates.PlayerFollow)
        {
            #region Zoom in and out
            if (GameManager.instance.currentController.CurrentPlayerState != PlayerState.Conversation && GameManager.instance.currentController.CurrentPlayerState != PlayerState.Transition)
            {
                if (GameManager.instance.currentController.playerInputHandlerComponent.GetToggleZoomInput())
                {
                    if (zoomActivate)
                    {
                        zoomed = !zoomed;
                        zoomTime = 0;
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

                    float currentZoomValue = Mathf.Lerp(transposerLeft.m_CameraDistance, zoomInValue, zoomTime / zoomDuration);
                    transposerLeft.m_CameraDistance = currentZoomValue;
                    transposerRight.m_CameraDistance = currentZoomValue;

                    if (zoomTime >= zoomDuration)
                    {
                        zoomTime = 0;
                        zoomActivate = false;
                        zoomed = true;
                    }
                }

                else
                {
                    zoomTime += Time.deltaTime;

                    float currentZoomValue = Mathf.Lerp(transposerLeft.m_CameraDistance, zoomOutValue, zoomTime / zoomDuration);

                    transposerLeft.m_CameraDistance = currentZoomValue;
                    transposerRight.m_CameraDistance = currentZoomValue;

                    if (zoomTime >= zoomDuration)
                    {
                        zoomTime = 0;
                        zoomActivate = false;
                        zoomed = false;
                    }
                }
            }

            #endregion

            #region Change Between Right and Left

            if (changeToRight)
            {

                _changeTimer -= Time.deltaTime;
                
                if(_changeTimer <= 0)
                {
                    changeToRight = false;
                    _changeTimer = changeTimer;
                    SetFollowCameraRight();
                }
            }

            if (changeToLeft)
            {
                _changeTimer -= Time.deltaTime;

                if (_changeTimer <= 0)
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



    public void ChangeToDialogueCamera()
    {
        ResetAllPriorities();
        dialogueCamera.Priority = 1;
    }


    public void SetCameraRightTimer()
    {
        if (right)
        {
            changeToLeft = false;
            _changeTimer = changeTimer;
        }
        else
        {
            changeToRight = true;
        }

    }
    public void SetCameraLeftTimer()
    {
        if (left)
        {
            changeToRight = false;
            _changeTimer = changeTimer;
        }
        else
        {
            changeToLeft = true;
        }
    }

    public void StopCameraTimer()
    {
        changeToRight = false;
        changeToLeft = false;

        _changeTimer = changeTimer;
    }
    public void SetFollowCameraRight()
    {
        ResetAllPriorities();

        if (currentCameraState == CameraStates.PlayerFollow)
        {
            levelCameraRight.Priority = 1;
            levelCameraLeft.Priority = 0;
            right = true;
            left = false;
        }
    }

    public void SetFollowCameraLeft()
    {
        ResetAllPriorities();

        if (currentCameraState == CameraStates.PlayerFollow)
        {
            levelCameraLeft.Priority = 1;
            levelCameraRight.Priority = 0;
            left = true;
            right = false;
        }
    }

    public void GetFollowCamerasTransposers()
    {
        transposerLeft = levelCameraLeft.GetCinemachineComponent<CinemachineFramingTransposer>();
        transposerRight = levelCameraRight.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    public void ResetAllPriorities()
    {
        levelCameraLeft.Priority = 0;
        levelCameraRight.Priority = 0;
        dialogueCamera.Priority = 0;
    }
    #endregion
}
