using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//This script handles input for the different player actions.

public class PlayerInputHandler : PlayerComponent
{
    //The input class for the player actions
    private PlayerInputAsset _playerInput = null;

    //Stick and movement value for the player movement and UI traversal.
    private Vector2 movementInput;

    private bool runningInput;    //Button values for running
    private bool crouchingInput;  //Button values for running
    private bool jumpingInput;    //Button values for jumping
    private bool interactInput;   //Button values for interacting
    private bool acceptInput;     //Button values for accepting
    private bool toggleZoomInput;  //Button values for zooming 
    //Controls to check which type of control the player is using
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
        _playerInput.PlayerMovement.ToggleCamera.performed += OnToggleCanceled;

        //Accepting
        _playerInput.PlayerUI.Accept.performed += OnAcceptPerformed;
        _playerInput.PlayerUI.Accept.canceled += OnAcceptCanceled;

        

    }

    private void OnDisable()
    {
        _playerInput.Disable();

        //Removing the listeners oon the component disable.
        _playerInput.PlayerMovement.Movement.performed -= OnMovementPerformed;
        _playerInput.PlayerMovement.Movement.canceled -= OnMovementCanceled;

        _playerInput.PlayerMovement.Running.performed -= OnRunningPerformed;
        _playerInput.PlayerMovement.Running.canceled -= OnRunningCanceled;

        _playerInput.PlayerMovement.Crouching.performed -= OnCrouchingPerformed;
        _playerInput.PlayerMovement.Crouching.canceled -= OnCrouchingCanceled;

        _playerInput.PlayerMovement.Jump.performed -= OnJumpingPerformed;
        _playerInput.PlayerMovement.Jump.canceled -= OnJumpingCanceled;

        _playerInput.PlayerMovement.Interact.performed -= OnInteractPerformed;
        _playerInput.PlayerMovement.Interact.canceled -= OnInteractCanceled;

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
    #endregion


    #region GetInputValues
    //Returning the value of the moving Vector, we will only be using the x values for now.
    public Vector2 GetMovementDirection()
    {
        return movementInput;
    }

    public bool GetRunningInput()
    {
        return runningInput;
    }

    public bool GetCrouchingInput()
    {
        return crouchingInput;

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
    #endregion

    private void Update()
    {
        gamepad = Gamepad.current;
        keyboard = Keyboard.current;

        if (gamepad != null)
        {
            _parent.currentControl = ControlType.Gamepad;
            return;
        }

        if (keyboard != null)
        {
            _parent.currentControl = ControlType.Keyboard;
            return;
        }
    }

    #region Change Action Maps
    public void ChangeToUIControls()
    {
        _playerInput.PlayerMovement.Disable();
        _playerInput.PlayerUI.Enable();
    }

    public void ChangeToLevelControls()
    {
        _playerInput.PlayerMovement.Enable();
        _playerInput.PlayerUI.Disable();
    }


    #endregion
}
