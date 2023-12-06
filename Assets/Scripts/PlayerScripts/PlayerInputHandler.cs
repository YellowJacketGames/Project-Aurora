using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//This script handles input for the different player actions.

public class PlayerInputHandler : PlayerComponent
{
    //The input class for the player actions
    private PlayerInput _playerInput = null;

    //Stick and movement value for the player movement and UI traversal.
    private Vector2 movementInput;

    private bool runningInput;    //Button values for running
    private bool crouchingInput;  //Button values for running
    private bool jumpingInput;    //Button values for jumping


    public override void Awake()
    {
        base.Awake();
        _playerInput = new PlayerInput();
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
    #endregion


    //This region stores the methods that return the input values when accessed by other scripts
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

    #endregion
}
