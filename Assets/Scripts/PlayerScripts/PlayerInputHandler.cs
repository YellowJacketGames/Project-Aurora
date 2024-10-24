using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//This script handles input for the different player actions.

public class PlayerInputHandler : PlayerComponent
{
    [Header("Control Schemes")]
    [SerializeField] InputControlScheme keyboardScheme;
    //The input class for the player actions
    private PlayerInputAsset _playerInput = null;
    [SerializeField] PlayerInput test;

    public PlayerInputAsset PlayerInput => _playerInput;
    
    //Stick and movement value for the player movement and UI traversal.
    private Vector2 movementInput;

    private bool runningInput;    //Button values for running
    private bool crouchingInput;  //Button values for running
    private bool jumpingInput;    //Button values for jumping
    private bool interactInput;   //Button values for interacting
    private bool acceptInput;     //Button values for accepting
    private bool toggleZoomInput;  //Button values for zooming 
    //Controls to check which type of control the player is using

    private Vector2 moveInput;
    private bool pressInput;
    private bool exitInput; 
    
    
    private Gamepad gamepad;
    private Keyboard keyboard;
    public override void Awake()
    {
        base.Awake();
        _playerInput = new PlayerInputAsset();
        ChangeToLevelControls();
    }

    #region HandleDisableAndEnable

    //This method handles the adding of listeners and enabling the component when the gameobject is enabled and disabled.
    private void OnEnable()
    {
        
        _playerInput.Enable();

        
        Debug.Log(_playerInput.controlSchemes[0].name);
        //Adding listeners to the events when the input action is performed.

        //Movement
        _playerInput.PlayerMovement.Movement.performed += OnMovementPerformed;
        _playerInput.PlayerMovement.Movement.canceled += OnMovementCanceled;

        //Running
        _playerInput.PlayerMovement.Running.performed += OnRunningPerformed;
        _playerInput.PlayerMovement.Running.canceled += OnRunningCanceled;

        //Crouching
        _playerInput.PlayerMovement.Crouching.performed += OnCrouchingPerformed;
        _playerInput.PlayerMovement.Crouching.canceled += OnCrouchingCanceled;

        //Jumping
        _playerInput.PlayerMovement.Jump.performed += OnJumpingPerformed;
        _playerInput.PlayerMovement.Jump.canceled += OnJumpingCanceled;

        //Interacting
        _playerInput.PlayerMovement.Interact.performed += OnInteractPerformed;
        _playerInput.PlayerMovement.Interact.canceled += OnInteractCanceled;

        //Toggling Zoom
        _playerInput.PlayerMovement.ToggleCamera.performed += OnTogglePerformed;
        _playerInput.PlayerMovement.ToggleCamera.canceled += OnToggleCanceled;

        //Accepting
        _playerInput.PlayerUI.Accept.performed += OnAcceptPerformed;
        _playerInput.PlayerUI.Accept.canceled += OnAcceptCanceled;

        //Pausing
        _playerInput.PlayerPause.Pause.performed += OnPausePerformed;
        _playerInput.PlayerPause.Pause.canceled += OnPauseCanceled;

        //Diary
        _playerInput.PlayerOptions.Diary.performed += OnDiaryPerformed;
        _playerInput.PlayerOptions.Diary.canceled += OnDiaryCanceled;

        //Pass next tab
        _playerInput.PlayerOptions.NextTab.performed += OnNextTabPerformed;
        _playerInput.PlayerOptions.NextTab.canceled += OnNextTabCancelled;

        //Return to previous tab
        _playerInput.PlayerOptions.PreviousTab.performed += OnPreviousTabPerformed;
        _playerInput.PlayerOptions.PreviousTab.canceled += OnPreviousTabCancelled;


        _playerInput.PuzzleDoor.Move.performed += OnMovePerformed;
        _playerInput.PuzzleDoor.Move.canceled += OnMoveCanceled;
        
        _playerInput.PuzzleDoor.Press.performed += OnPressPerformed;
        _playerInput.PuzzleDoor.Press.canceled += OnPressCanceled;
        
        _playerInput.PuzzleDoor.Exit.performed += OnExitPerformed;
        _playerInput.PuzzleDoor.Exit.canceled += OnExitCanceled;
    }

    private void OnDisable()
    {
        _playerInput.Disable();

        //Removing the listeners oon the component disable.

        //Movement
        _playerInput.PlayerMovement.Movement.performed -= OnMovementPerformed;
        _playerInput.PlayerMovement.Movement.canceled -= OnMovementCanceled;

        //Running
        _playerInput.PlayerMovement.Running.performed -= OnRunningPerformed;
        _playerInput.PlayerMovement.Running.canceled -= OnRunningCanceled;

        //Crouching
        _playerInput.PlayerMovement.Crouching.performed -= OnCrouchingPerformed;
        _playerInput.PlayerMovement.Crouching.canceled -= OnCrouchingCanceled;

        //Jumping
        _playerInput.PlayerMovement.Jump.performed -= OnJumpingPerformed;
        _playerInput.PlayerMovement.Jump.canceled -= OnJumpingCanceled;

        //Interacting
        _playerInput.PlayerMovement.Interact.performed -= OnInteractPerformed;
        _playerInput.PlayerMovement.Interact.canceled -= OnInteractCanceled;

        //Toggling Zoom
        _playerInput.PlayerMovement.ToggleCamera.performed -= OnTogglePerformed;
        _playerInput.PlayerMovement.ToggleCamera.performed -= OnToggleCanceled;

        //Accepting
        _playerInput.PlayerUI.Accept.performed -= OnAcceptPerformed;
        _playerInput.PlayerUI.Accept.canceled -= OnAcceptCanceled;

        //Pausing
        _playerInput.PlayerPause.Pause.performed -= OnPausePerformed;
        _playerInput.PlayerPause.Pause.canceled -= OnPauseCanceled;

        //Diary
        _playerInput.PlayerOptions.Diary.performed -= OnDiaryPerformed;
        _playerInput.PlayerOptions.Diary.canceled -= OnDiaryCanceled;

        //Pass next tab
        _playerInput.PlayerOptions.NextTab.performed -= OnNextTabPerformed;
        _playerInput.PlayerOptions.NextTab.canceled -= OnNextTabCancelled;

        //Return to previous tab
        _playerInput.PlayerOptions.PreviousTab.performed -= OnPreviousTabPerformed;
        _playerInput.PlayerOptions.PreviousTab.canceled -= OnPreviousTabCancelled;
        
        
        _playerInput.PuzzleDoor.Move.performed -= OnMovePerformed;
        _playerInput.PuzzleDoor.Move.canceled -= OnMoveCanceled;
        
        _playerInput.PuzzleDoor.Press.performed -= OnPressPerformed;
        _playerInput.PuzzleDoor.Press.canceled -= OnPressCanceled;
        
        _playerInput.PuzzleDoor.Exit.performed -= OnExitPerformed;
        _playerInput.PuzzleDoor.Exit.canceled -= OnExitCanceled;

    }
    #endregion


    //This region holds the different events that store the values of the inputs in variables and are added as listeners to the input actions
    #region InputEvents
    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        
        movementInput = value.ReadValue<Vector2>();

        //We normalize the vector so that it always gives a flat number.
        movementInput.Normalize();
    }
    private void OnMovementCanceled(InputAction.CallbackContext value)
    {
        movementInput = Vector2.zero;
    }

    private void OnRunningPerformed(InputAction.CallbackContext value)
    {
        runningInput = true;
    }
    private void OnRunningCanceled(InputAction.CallbackContext value)
    {
        runningInput = false;
    }

    private void OnCrouchingPerformed(InputAction.CallbackContext value)
    {
        crouchingInput = true;
    }
    private void OnCrouchingCanceled(InputAction.CallbackContext value)
    {
        crouchingInput = false;
    }

    private void OnJumpingPerformed(InputAction.CallbackContext value)
    {
        jumpingInput = true;
    }

    private void OnJumpingCanceled(InputAction.CallbackContext value)
    {
        jumpingInput = false;
    }

    private void OnInteractPerformed(InputAction.CallbackContext value)
    {
        interactInput = true;
    }

    private void OnInteractCanceled(InputAction.CallbackContext value)
    {
        interactInput = false;
    }

    private void OnTogglePerformed(InputAction.CallbackContext value)
    {
        toggleZoomInput = true;
    }

    private void OnToggleCanceled(InputAction.CallbackContext value)
    {
        toggleZoomInput = false;
    }

    private void OnAcceptPerformed(InputAction.CallbackContext value)
    {
        acceptInput = true;
    }

    private void OnAcceptCanceled(InputAction.CallbackContext value)
    {
        acceptInput = false;
    }

    private void OnDiaryPerformed(InputAction.CallbackContext value) //We set this in the event to execute the code without having to require the update method
    {
        if (GameManager.instance.GetCurrentGameState() == GameStates.Pause)
            return;
        switch (GameManager.instance.GetCurrentGameState()) //Depending on the current state, it will pause or unpause the game
        {
            case GameStates.Gameplay: //If we're currently playing, we pause the game.
                GameManager.instance.pauseManager.Pause();
                GameManager.instance.diaryManager.OpenDiaryTab();
                
                break;
            case GameStates.Diary: //If we're already in the diary menu, we close it
                GameManager.instance.diaryManager.CloseDiaryTab();
                GameManager.instance.pauseManager.UnPause();
                break;
            default:
                break;
        }
    }

    private void OnDiaryCanceled(InputAction.CallbackContext value)
    {

    }

    private void OnNextTabPerformed(InputAction.CallbackContext value)
    {
        GameManager.instance.diaryManager.OpenNextTab();
    }

    private void OnNextTabCancelled(InputAction.CallbackContext value)
    {

    }
    private void OnPreviousTabPerformed(InputAction.CallbackContext value)
    {
        GameManager.instance.diaryManager.OpenNextTab();

    }
    private void OnPreviousTabCancelled(InputAction.CallbackContext value)
    {

    }
    private void OnPausePerformed(InputAction.CallbackContext value) //We set this in the event to execute the code without having to require the update method
    {
        if (GameManager.instance.GetCurrentGameState() == GameStates.Diary)
            return;
        switch (GameManager.instance.GetCurrentGameState()) //Depending on the current state, it will pause or unpause the game
        {
            case GameStates.Gameplay: //If we're currently playing, we pause the game.
                GameManager.instance.pauseManager.Pause();
                GameManager.instance.pauseManager.OpenPauseTab();
                break;
            case GameStates.Pause: //If we're already in the pause menu, we unpause it
                GameManager.instance.pauseManager.UnPause();
                break;
            default:
                break;
        }
    }

    private void OnPauseCanceled(InputAction.CallbackContext value)
    {

    }


    private void OnMovePerformed(InputAction.CallbackContext value)
    {
        moveInput = value.ReadValue<Vector2>();
        moveInput.Normalize();
    }
    private void OnMoveCanceled(InputAction.CallbackContext value)
    {
        moveInput = Vector2.zero;
    }
    private void OnPressPerformed(InputAction.CallbackContext value)
    {
        pressInput = true;
    }
    private void OnPressCanceled(InputAction.CallbackContext value)
    {
        pressInput = false;
    }

    private void OnExitPerformed(InputAction.CallbackContext value)
    {
        exitInput = true;
    }
    private void OnExitCanceled(InputAction.CallbackContext value)
    {
        exitInput = false;
    }
    
    #endregion


    #region GetInputValues
    //Returning the value of the moving Vector, we will only be using the x values for now.
    public Vector2 GetMovementDirection()
    {
        // return movementInput;
        return GetMovementDirectionClamped();
    }    
    public Vector2 GetMovementDirectionClamped()
    {
        float newX,newY;
        if (movementInput.x > 0) newX = 1; 
        else if (movementInput.x < 0) newX = -1; 
        else newX = 0; 
        if (movementInput.y > 0) newY = 1; 
        else if (movementInput.y < 0) newY = -1; 
        else newY = 0; 

        return new Vector2(newX,newY);
    }

    public bool GetRunningInput()
    {
        return runningInput;
    }

    public bool GetCrouchingInput()
    {
        bool value = crouchingInput;
        crouchingInput = false;
        return value;

    }

    public bool GetJumpingInput()
    {
        return jumpingInput;

    }

    public bool GetInteractInput()
    {
        bool result = interactInput;
        interactInput = false;
        return result;
    }

    public bool GetAcceptInput()
    {
        bool result = acceptInput;
        acceptInput = false;
        return result;
    }

    public bool GetToggleZoomInput()
    {
        bool result = toggleZoomInput;
        toggleZoomInput = false;
        return result;
    }
    public Vector2 GetRotationInput()
    {
        float rotationX = Input.GetKey(KeyCode.A) ? -1 : Input.GetKey(KeyCode.D) ? 1 : 0;
        return new Vector2(rotationX, 0); // Only X-axis input for rotation
    }
    #endregion

    private void Update()
    {
        // Debug.Log(test.currentControlScheme);
        if (test.currentControlScheme == "Controller")
        {
            _parent.currentControl = ControlType.Gamepad;
            return;
        }

        if (test.currentControlScheme == "Keyboard")
        {
            _parent.currentControl = ControlType.Keyboard;
            return;
        }
    }

    #region Change Action Maps
    public void ChangeToUIControls()
    {
        _playerInput.PlayerMovement.Disable();
        _playerInput.PuzzleDoor.Disable();
        _playerInput.PlayerUI.Enable();
        
    }

    public void ChangeToLevelControls()
    {
        _playerInput.PlayerMovement.Enable();
        _playerInput.PuzzleDoor.Disable();
        _playerInput.PlayerUI.Disable();
        
    }
    public void ChangeToPuzzleDoorControls()
    {
        _playerInput.PuzzleDoor.Enable();
        _playerInput.PlayerMovement.Disable();
        _playerInput.PlayerUI.Disable();
    }

    #endregion
}
