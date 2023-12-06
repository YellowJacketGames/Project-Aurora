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

    //Controls to check which type of control the player is using
    private Gamepad gamepad;
    private Keyboard keyboard;
    public override void Awake()
    {
        base.Awake();
        _playerInput = new PlayerInputAsset();
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

        _playerInput.PlayerMovement.Crouching.performed += OnCrouchingPerformed;
        _playerInput.PlayerMovement.Crouching.canceled += OnCrouchingCanceled;

        _playerInput.PlayerMovement.Jump.performed += OnJumpingPerformed;
        _playerInput.PlayerMovement.Jump.canceled += OnJumpingCanceled;

        _playerInput.PlayerMovement.Interact.performed += OnInteractPerformed;
        _playerInput.PlayerMovement.Interact.canceled += OnInteractCanceled;
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
        return interactInput;
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

}
