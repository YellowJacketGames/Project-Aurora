using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//This script is used as a parent and main communicator between the other player scripts like movement or conversations
//It serves as a intermediary through storing the components in the player Scripts region as well as handling the different player states
//It also enables and disables the usage of player input.

public class PlayerController : MonoBehaviour
{
    //This region handles what happens when the player states change and which events should execute.

    #region PlayerStates

    //Property of the different player states
    //It executes a public event on state change

    private PlayerState _currentPlayerState;
    public PlayerState CurrentPlayerState
    {
        get
        {
            return _currentPlayerState;
        }
        set
        {
            //Sets the exit of the different player states with the current state value
            ExitStates(_currentPlayerState);
            
            //Sets the new state value
            _currentPlayerState = value;

            //Sets the enter of the different player states with the new state value
            EnterStates(_currentPlayerState);
        }
    }

    #endregion


    //This region handles the variables for the different player components such as movement and conversation
    //In said scripts, the components will store a 'parent' variable to access this components.

    #region PlayerComponents
    //This are public variables so the 'children' in this components can access each other without having to require a reference to each other
    //in their scripts.

    //Scripts
    [HideInInspector] public PlayerMovement playerMovementComponent;
    [HideInInspector] public PlayerInputHandler playerInputHandlerComponent;
    [HideInInspector] public PlayerAnimations playerAnimationComponent;
    [HideInInspector] public PlayerCollisions playerCollisionsComponent;
    [HideInInspector] public PlayerUI playerUIComponent;

    //PlayerComponents
    [Header("Player Components")]
    //This is just placeholder to check the crouch
    public GameObject idleModel;
    public GameObject crouchingModel;
    //We will use a rigid component so that the jump physics are a bit more realistic and collisions are easier to handle.
    [HideInInspector] public Rigidbody playerRigid;
    [Space]
    [Header("Player UI Components")]
    public GameObject keyboardControls;   //Placeholder for contextual keyboard and mouse controls
    public GameObject gamepadControls; //Placeholder for contextual gamepad controls
    //This is just for testing purposes
    [Header("Testing variables")]
    [SerializeField] bool seeStateChange;
    #endregion


    //This region stores all events to handle the entrance and exit of the different player states.

    #region PlayerStateEvents

    //List of events to handle whenever the player enters each different state
    [Header("Player Enter State Events")]
    [SerializeField] UnityEvent _enterIdle;
    [SerializeField] UnityEvent _enterWalk;
    [SerializeField] UnityEvent _enterRun;
    [SerializeField] UnityEvent _enterCrouch;
    [SerializeField] UnityEvent _enterJump;
    [SerializeField] UnityEvent _enterConversation;
    [SerializeField] UnityEvent _enterTransition;

    //List of events to handle whenever the player exits each different state
    [Header("Player Exit State Events")]
    [SerializeField] UnityEvent _exitIdle;
    [SerializeField] UnityEvent _exitWalk;
    [SerializeField] UnityEvent _exitRun;
    [SerializeField] UnityEvent _exitCrouch;
    [SerializeField] UnityEvent _exitJump;
    [SerializeField] UnityEvent _exitConversation;
    [SerializeField] UnityEvent _exitTransition;

    #endregion

    //This region holds the methods to handle exiting and entering states in the PlayerStates variable, as well as the method to change the current player state

    #region HandlingPlayerStates

    //This method takes a switch statement with a PlayerState variable to know which state to exit from and call the correponding event.
    public void ExitStates(PlayerState _currentState)
    {
        switch (_currentState)
        {
            case PlayerState.Idle:
                _exitIdle.Invoke();
                break;

            case PlayerState.Walk:
                _exitWalk.Invoke();
                break;

            case PlayerState.Crouch:
                _exitCrouch.Invoke();
                break;

            case PlayerState.Run:
                _exitRun.Invoke();
                break;

            case PlayerState.Jump:
                _exitJump.Invoke();
                break;

            case PlayerState.Conversation:
                _exitConversation.Invoke();
                break;

            case PlayerState.Transition:
                _exitTransition.Invoke();
                break;

            default:
                break;
        }
    }

    //This method takes a switch statement with a PlayerState variable to know which state to enter and call the correponding event.

    public void EnterStates(PlayerState _currentState)
    {
        switch (_currentState)
        {
            case PlayerState.Idle:
                _enterIdle.Invoke();
                break;

            case PlayerState.Walk:
                _enterWalk.Invoke();
                break;

            case PlayerState.Crouch:
                _enterCrouch.Invoke();
                break;

            case PlayerState.Run:
                _enterRun.Invoke();
                break;

            case PlayerState.Jump:
                _enterJump.Invoke();
                break;

            case PlayerState.Conversation:
                _enterConversation.Invoke();
                break;

            case PlayerState.Transition:
                _enterTransition.Invoke();
                break;

            default:
                break;
        }
    }

    //Changes the current player state
    public void ChangeState(PlayerState _newState)
    {
        CurrentPlayerState = _newState;

        if (seeStateChange)
        {
            Debug.Log("Current State: " + _newState);
        }
    }

    #endregion


    private void Awake()
    {
        //Get references of the different children components.

        //We use 'TryGetComponent' so that if the component is missing in the player object it doesn't crash the game with a missing reference.
        if(TryGetComponent<PlayerMovement>(out PlayerMovement movement))
        {
            playerMovementComponent = movement;
        }
        else
        {
            //Warning so that we know what the issue is in the console
            Debug.LogWarning("Player Movement component is missing in the player object");
        }

        //We use 'TryGetComponent' so that if the component is missing in the player object it doesn't crash the game with a missing reference.
        if (TryGetComponent<PlayerInputHandler>(out PlayerInputHandler handler))
        {
            playerInputHandlerComponent = handler;
        }
        else
        {
            //Warning so that we know what the issue is in the console
            Debug.LogWarning("Player Input Handler component is missing in the player object");
        }


        //We use 'TryGetComponent' so that if the component is missing in the player object it doesn't crash the game with a missing reference.
        if (TryGetComponent<Rigidbody>(out Rigidbody rigid))
        {
            playerRigid = rigid;
        }
        else
        {
            //Warning so that we know what the issue is in the console
            Debug.LogWarning("Rigidbody component is missing in the player object");
        }

        //We use 'TryGetComponent' so that if the component is missing in the player object it doesn't crash the game with a missing reference.
        if (TryGetComponent<PlayerAnimations>(out PlayerAnimations animations))
        {
            playerAnimationComponent = animations;
        }
        else
        {
            //Warning so that we know what the issue is in the console
            Debug.LogWarning("Player Animations component is missing in the player object");
        }

        //We use 'TryGetComponent' so that if the component is missing in the player object it doesn't crash the game with a missing reference.
        if (TryGetComponent<PlayerCollisions>(out PlayerCollisions collisions))
        {
            playerCollisionsComponent = collisions;
        }
        else
        {
            //Warning so that we know what the issue is in the console
            Debug.LogWarning("Player Collisions component is missing in the player object");
        }

        //We use 'TryGetComponent' so that if the component is missing in the player object it doesn't crash the game with a missing reference.
        if (TryGetComponent<PlayerUI>(out PlayerUI ui))
        {
            playerUIComponent = ui;
        }
        else
        {
            //Warning so that we know what the issue is in the console
            Debug.LogWarning("Player UI component is missing in the player object");
        }
    }

    private void Start()
    {
        //We will set the beginning state to idle, since the player isn't moving when the game begins.
        ChangeState(PlayerState.Idle);
    }


}
