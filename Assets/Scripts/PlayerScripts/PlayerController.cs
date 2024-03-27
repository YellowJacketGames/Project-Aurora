using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;


//This script is used as a parent and main communicator between the other player scripts like movement or conversations
//It serves as a intermediary through storing the components in the player Scripts region as well as handling the different player states
//It also enables and disables the usage of player input.

public class PlayerController : MonoBehaviour
{
    //This region handles what happens when the player states change and which events should execute.

    #region PlayerStates

    //Property of the different player states
    //It executes a public event on state change
    public PlayerState checkPlayerState;
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
    [HideInInspector] public PlayerInteract playerInteractComponent;
    [HideInInspector] public PlayerConversation playerConversationComponent;
    [HideInInspector] public PlayerInventory playerInventoryComponent;

    //PlayerComponents
    [Header("Player Components")]
    //This is just placeholder to check the crouch
    [Header("Models")]
    public GameObject characterModel;
    public GameObject crouchingModel;
    //We will use a rigid component so that the jump physics are a bit more realistic and collisions are easier to handle.
    [HideInInspector] public Rigidbody playerRigid;
    [Space]
    [Header("Player UI Components")]
    [Space]

    [Header("Controls")]
    public GameObject keyboardControls;   //Placeholder for contextual keyboard and mouse controls
    public GameObject gamepadControls; //Placeholder for contextual gamepad controls
    [Space]

    [Header("Interactable Prompts")]
    public GameObject interactablePromptObject; //Parent object of the interactable prompt
    public TextMeshProUGUI interactableText;    //Text component for the interactable prompt
    public GameObject keyboardInteractPrompt;   //Keyboard button for the interactable prompt
    public GameObject controllerInteractPrompt; //Controller button for the interactable prompt
    [Space]

    [Header("Dialogue")] 
    public GameObject dialogueBox; //the whole dialogue box gameobject that holds the other components
    public TextMeshProUGUI dialogueText; //Text component to display the dialogue text
    public GameObject keyboardContinueButton; //Keyboard continue prompt
    public GameObject controllerContinueButton; //Controller continue prompt
    public DialogueLayoutClass[] dialogueLayouts; //A layout array to hold the left and right speakers
    [Space]

    [Header("Inventory")]
    public Animator inventoryAnimations; //the inventory gameobject that holds the other components
    public TextMeshProUGUI objectName; //The text component to display the object name
    public Image objectIcon; //The image component to display the object icon

    //This is just for testing purposes
    [Header("Testing variables")]
    [SerializeField] bool seeStateChange;  //Variable to check for status in console for player states
    public bool seeControls;               //Variable to check for the context controls in the player UI
    #endregion

    [Header("Player controls")] //Control enum to check the current controls of the player
    public ControlType currentControl;

    [Header("Player Colliders")]
    public Collider characterCollider;

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
        #region Get Children References
        //Get references of the different children components.

        //We use 'TryGetComponent' so that if the component is missing in the player object it doesn't crash the game with a missing reference.
        if (TryGetComponent<PlayerMovement>(out PlayerMovement movement))
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

        if (TryGetComponent<PlayerInteract>(out PlayerInteract interact))
        {
            playerInteractComponent = interact;
        }
        else
        {
            //Warning so that we know what the issue is in the console
            Debug.LogWarning("Player Interact component is missing in the player object");
        }

        if (TryGetComponent<PlayerConversation>(out PlayerConversation conversation))
        {
            playerConversationComponent = conversation;
        }
        else
        {
            //Warning so that we know what the issue is in the console
            Debug.LogWarning("Player Conversation component is missing in the player object");
        }

        if (TryGetComponent<PlayerInventory>(out PlayerInventory inventory))
        {
            playerInventoryComponent = inventory;
        }
        else
        {
            //Warning so that we know what the issue is in the console
            Debug.LogWarning("Player Inventory component is missing in the player object");
        }
        #endregion

        #region Give Game Manager Reference
        GameManager.instance.currentController = this;
        #endregion

        GameManager.instance.currentTransitionManager.SetFadeOut();

        #region Alter Events

        //We add this through code so that we don't have to reference it in the editor every time we enter a new scene
        _enterConversation.AddListener(GameManager.instance.currentCameraManager.SetDialogueCamera);
        _exitConversation.AddListener(GameManager.instance.currentCameraManager.SetFollowCameraRight);
        _enterIdle.AddListener(GameManager.instance.currentCameraManager.StopCameraTimer);

        #endregion 
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        checkPlayerState = CurrentPlayerState;
    }
}
