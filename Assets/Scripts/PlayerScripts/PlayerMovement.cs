using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is gonna handle the main movement for the character, such as walking, running, and crouching.

public class PlayerMovement : PlayerComponent
{
    //Movement Variables
    [Header("Movement Variables")]
    //The current speed of the player, it's value is only assigned through the other variables and their respective methods.
    [SerializeField] private float _currentSpeed;

    //The different player speeds.
    [Header("Player Speeds")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float crouchSpeed;
    [Space]

    [Header("Jump Values")]
    [SerializeField] private float jumpForce; //Value of the force that is applied to the player when it jumps
    [Space]
    [Header("Ground Check Values")]
    [SerializeField] private float groundCheckLength;  //Value of the lenght of the raycast that checks if the player is in the ground
    [SerializeField] private bool isGrounded;

    [Header("Rigid Limits")]
    [SerializeField] private RigidbodyConstraints idleConstraints;
    [SerializeField] private RigidbodyConstraints movingConstraints;
    //Bool variable to know if the player should be able to move
    bool shouldMove => _parent.CurrentPlayerState != PlayerState.Conversation && _parent.CurrentPlayerState != PlayerState.Transition &&
        _parent.playerInputHandlerComponent.GetMovementDirection().x != 0 && GameManager.instance.CanPlay();

    //Bool variable to know if the player should be able to jump.
    bool shouldJump => _parent.CurrentPlayerState != PlayerState.Conversation && _parent.CurrentPlayerState != PlayerState.Transition &&
        _parent.CurrentPlayerState != PlayerState.Jump && _parent.CurrentPlayerState != PlayerState.Crouch && _parent.playerInputHandlerComponent.GetJumpingInput()
        && GameManager.instance.CanPlay() && isGrounded;

    //Bool variable to check if the player should be able to crouch
    bool shouldCrouch => _parent.CurrentPlayerState != PlayerState.Conversation && _parent.CurrentPlayerState != PlayerState.Transition &&
        _parent.CurrentPlayerState != PlayerState.Jump && _parent.playerInputHandlerComponent.GetCrouchingInput() && GameManager.instance.CanPlay() && _parent.CurrentPlayerState != PlayerState.Crouch;

    #region PlayerMovementFunctions

    public void HandleMovement()    //This method will move the player with different speeds depending on the current state, which will be handled in a different method
    {
        //We set a new value to check only the z axis, this way we can handle the movement more easily.
        //We will also need to handle the rotation of the character so it always looks in the direction that he's walking.

        float movementValue = 0;
        movementValue = _parent.playerInputHandlerComponent.GetMovementDirection().x * _currentSpeed * Time.deltaTime;


        //We then set that value to the velocity of the player gameobject.
        //We have to preserve the y value so that is doesn't mess with the jump, which also alters the y velocity
        _parent.playerRigid.velocity = new Vector3(0, _parent.playerRigid.velocity.y, movementValue * _currentSpeed);

        
    }
    
    public void FreezePlayer()
    {
        _parent.playerRigid.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;

    }

    public void UnfreezePlayer()
    {
        _parent.playerRigid.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
    }
    //Method to handle player Jump
    public void HandleJump()
    {
        //if (_parent.CurrentPlayerState == PlayerState.Idle)
        //{
        //    ChangeToWalkSpeed();
        //}

        _parent.playerRigid.AddForce(Vector3.up * jumpForce);
        _parent.ChangeState(PlayerState.Jump);
    }

    //When the player falls, it should change the state back to whatever it was
    public void HandleFall()
    {
        //We only check for the Y value because it's the only one whose affected in the jump

        if (isGrounded)
        {
            _parent.playerRigid.velocity = new Vector3(_parent.playerRigid.velocity.x, 0, _parent.playerRigid.velocity.z);

            if(_parent.playerInputHandlerComponent.GetMovementDirection().x == 0)
            {
                _parent.ChangeState(PlayerState.Idle);
            }
            else if (_currentSpeed == walkSpeed)
            {
                _parent.ChangeState(PlayerState.Walk);
            }
            else if(_currentSpeed == runSpeed)
            {
                _parent.ChangeState(PlayerState.Run);
            }
            
        }            
    }

    public void HandleUncrouch() //This method handles if the player should be able to uncrouch, for example if he's in a tunnel and can't get up
    {
        if(_parent.CurrentPlayerState == PlayerState.Crouch)
        {
            if (_parent.playerInputHandlerComponent.GetCrouchingInput())
            {
                //if(!_parent.playerCollisionsComponent.CheckCrouching()) //Stopping this for now since we're not using crouch for any in-level mechanics and it's causing bugs
                _parent.ChangeState(PlayerState.Idle);
            }
        }
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

    public void ChangeToIdleSpeed()
    {
        _currentSpeed = 0;

        //The only value that remains the same is the Y value so that if the player is jumping it doesn't stop them midair.
        _parent.playerRigid.velocity = new Vector3(0, _parent.playerRigid.velocity.y, 0);
    }
    public void HandleMovementStates()
    {
        //If the player is in another restrictive state, they can't enter idle
        if (_parent.CurrentPlayerState == PlayerState.Conversation || _parent.CurrentPlayerState == PlayerState.Transition)
            return;

        //If the player isn't pressing any direction and isn't jumping, it's state should be idle.
        if(_parent.playerInputHandlerComponent.GetMovementDirection().x == 0)
        {
            if (_parent.CurrentPlayerState == PlayerState.Idle)
                return;

            if(_parent.CurrentPlayerState != PlayerState.Jump && _parent.CurrentPlayerState != PlayerState.Crouch)
            {
                ChangeToIdleSpeed();
                _parent.ChangeState(PlayerState.Idle);

            }

            if(_parent.CurrentPlayerState == PlayerState.Crouch)
            {
                _parent.playerAnimationComponent.AnimationMovement(0);
                ChangeToIdleSpeed();
            }
        }


        if(_parent.CurrentPlayerState == PlayerState.Jump || !isGrounded)
        {
            HandleFall();
        }
    }

    

    #endregion


    private void Update()
    {

        if (!GameManager.instance.CanPlay())
            return;

        //Handling different movement state changes
        HandleMovementStates();


        //We also handle the movement directions here
        //if the player is moving, we set the direction that the player is moving
        //This will be later replaced with animations
        if(_parent.playerInputHandlerComponent.GetMovementDirection().x != 0 && _parent.CurrentPlayerState != PlayerState.Transition && _parent.CurrentPlayerState != PlayerState.Conversation)
        {
            _parent.playerAnimationComponent.HandleModelDirection(_parent.playerInputHandlerComponent.GetMovementDirection().x);
        }

        

    }

    public void HandleGroundCheck(bool value) //this method will be handled with a different script that checks the collider of the ground check
    {
        isGrounded = value; //It sets the value to the variable depending on whether or not the player is grounded.
    }

    public void ChangePhysicialMaterialFriction(float value) //We will use this method when the player is grounded and airborn to change the friction of the player and prevent it from sliding and sticking to walls
    {
        _parent.characterCollider.material.dynamicFriction = value; //We're changing a physical material, not a regular material, hence the dynamic friction variable
    }
    private void FixedUpdate()
    {
        //If the player is able to meet the requirements to move, he can move.
        if (shouldMove)
        {
            HandleMovement();

            if (_parent.CurrentPlayerState == PlayerState.Crouch)
            {
                _parent.playerAnimationComponent.AnimationMovement(crouchSpeed);
                ChangeToCrouchSpeed();
            }
            //Handling of the different inputs

            //Running
            if (_parent.playerInputHandlerComponent.GetRunningInput() && _parent.CurrentPlayerState != PlayerState.Crouch && _parent.CurrentPlayerState != PlayerState.Jump)
            {
                _parent.ChangeState(PlayerState.Run);
            }

            //Walking
            else if(_parent.CurrentPlayerState != PlayerState.Jump && _parent.CurrentPlayerState != PlayerState.Crouch)
            {
                _parent.ChangeState(PlayerState.Walk);
            }
        }


        //We check in the update method if the player should jump
        if (shouldJump)
        {
            Debug.Log("Jump speed before running: " + _currentSpeed);
            HandleJump();
        }

        HandleUncrouch();

        //We check in the update method if the player should crouch
        if (shouldCrouch)
        {
            _parent.ChangeState(PlayerState.Crouch);
        }
        
    }


    #region Handle ground check values
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ground"))
        HandleGroundCheck(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground"))
        HandleGroundCheck(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        HandleGroundCheck(false);
    }

    #endregion

}
