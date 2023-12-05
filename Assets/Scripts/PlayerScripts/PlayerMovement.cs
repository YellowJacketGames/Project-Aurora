using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is gonna handle the main movement for the character, such as walking, running, and crouching.

public class PlayerMovement : MonoBehaviour
{
    //Parent class
    private PlayerController _parent;

    //Movement Variables
    [Header("Movement Variables")]
    //The current speed of the player, it's value is only assigned through the other variables and their respective methods.
    [SerializeField] private float _currentSpeed;

    //The different player speeds.
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float crouchSpeed;


    //Bool variable to know if the player should be able to move
    bool shouldMove => _parent.CurrentPlayerState != PlayerState.Conversation && _parent.CurrentPlayerState != PlayerState.Transition &&
        _parent.playerInputHandlerComponent.GetMovementDirection().x != 0;


    #region PlayerMovementFunctions

    public void HandleMovement()    //This method will move the player with different speeds depending on the current state, which will be handled in a different method
    {
        //We set a new value to check only the z axis, this way we can handle the movement more easily.
        //We will also need to handle the rotation of the character so it always looks in the direction that he's walking.

        float movementValue = 0;
        movementValue = _parent.playerInputHandlerComponent.GetMovementDirection().x * _currentSpeed * Time.deltaTime;


        //We then set that value to the transform.position of the player gameobject.
        transform.position += new Vector3(transform.position.x, transform.position.y, movementValue);
    }

    //The next three methods handle the different player speeds
    public void ChangeToWalkSpeed()    //This method changes the speed value to the walk value
    {
        _currentSpeed = walkSpeed;
    }

    public void ChangeToRunSpeed()      //This method changes the speed value to the run value
    {
        _currentSpeed = runSpeed;
    }

    public void ChangeToCrouchSpeed()   //This method changes the speed value to the crouch value
    {
        _currentSpeed = crouchSpeed;
    }

    public void HandleMovementStates()
    {
        //If the player isn't pressing any direction, it's state should be idle.

        if(_parent.playerInputHandlerComponent.GetMovementDirection().x == 0)
        {
            if (_parent.CurrentPlayerState == PlayerState.Idle)
                return;
            _parent.ChangeState(PlayerState.Idle);
        }

        //Handle next state changes when inputs are added

    }

    #endregion


    private void Awake()
    {
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
    private void Start()
    {
        //On the start we will set the speed to walking
        ChangeToWalkSpeed();
    }

    private void Update()
    {
        //Handling different movement state changes
        HandleMovementStates();
    }

    private void FixedUpdate()
    {
        //If the player is able to meet the requirements to move, he can move.
        if (shouldMove)
        {
            HandleMovement();

            if (_parent.playerInputHandlerComponent.GetRunningInput() && _parent.CurrentPlayerState != PlayerState.Crouch)
            {
                _parent.ChangeState(PlayerState.Run);
            }

            else if (_parent.playerInputHandlerComponent.GetCrouchingInput())
            {
                _parent.ChangeState(PlayerState.Crouch);
            }

            else
            {
                _parent.ChangeState(PlayerState.Walk);
            }
        }



        //If the player is pressing the run input, it runs

    }
}
