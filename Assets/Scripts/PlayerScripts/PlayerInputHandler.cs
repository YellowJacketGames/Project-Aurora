using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//This script handles input for the different player actions.

public class PlayerInputHandler : MonoBehaviour
{
    //Parent class
    private PlayerController _parent;
    //The input class for the player actions
    private PlayerInput _playerInput = null;

    //Stick and movement value for the player movement and UI traversal.
    private Vector2 movementInput;

    //Button values for running and crouching
    private bool runningInput;
    private bool crouchingInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();


        //We use try get component so that if the component is missing it doesn't crash the game.
        if (TryGetComponent<PlayerController>(out PlayerController p))
        {
            _parent = p;
        }

        else
        {
            //Warning for knowing what the issue is in the console.
            Debug.LogWarning("Player Controller component is missing in the gameobject");
        }
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

    }
    #endregion

    //This region holds the different events that store the values of the inputs in variables and are added as listeners to the input actions
    #region InputEvents
    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        movementInput = value.ReadValue<Vector2>();
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
    #endregion
}
