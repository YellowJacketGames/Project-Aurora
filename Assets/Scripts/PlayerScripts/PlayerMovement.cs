using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : PlayerComponent
{
    [Header("Movement Variables")] [SerializeField]
    private float _currentSpeed;

    [SerializeField] private MovementType movementType;
    [SerializeField] private MovementDirection movementDirection;
    [SerializeField] private bool horizontalAndVerticalMovement;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float crouchSpeed;
    [Space] [SerializeField] private float jumpForce;

    [Space] [Header("Checks")] [SerializeField]
    private bool isGrounded;

    [SerializeField] private bool isJumping;
    [SerializeField] private bool isCrouched;
    [SerializeField] private bool isColliding;
    [SerializeField] public bool triggerCollisionsL;

    [SerializeField] public bool triggerCollisionsR;
    // [SerializeField] public bool triggerCollisionsU;
    // [SerializeField] public bool triggerCollisionsD;


    [Header("Rigid Limits")] [SerializeField]
    private RigidbodyConstraints idleConstraints;

    [SerializeField] private RigidbodyConstraints movingConstraints;

    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;

    public float JumpTimeout = 0.50f; //time required before can jump again
    public float FallTimeout = 0.15f;


    private float targetSpeed = 0f;
    private float inputMagnitude = 0f;
    private float _speedChangeRate = 8.0f; //for the animationBlending
    private float animationBlend;

    #region Booleans

    private bool canMove => _parent.CurrentPlayerState != PlayerState.Conversation &&
                            _parent.CurrentPlayerState != PlayerState.Transition;

    #endregion

    private void Start()
    {
        _jumpTimeoutDelta = JumpTimeout;
        _fallTimeoutDelta = FallTimeout;
    }

    private void Update()
    {
        HandleFlipX();
        HandleStates();
    }

    private void FixedUpdate()
    {
        HandleCollisions();
        if (!canMove) return;
        HandleMovementType();
        HandleMovement();
        HandleJump();
        HandleCrouch();
    }

    private void HandleMovementType()
    {
        if (!horizontalAndVerticalMovement) return;
        if (_parent.playerInputHandlerComponent.GetMovementDirection().x != 0)
            movementType = MovementType.Horizontal;
        else if (_parent.playerInputHandlerComponent.GetMovementDirection().y != 0)
            movementType = MovementType.Vertical;
        UnfreezePlayer();
    }

    private void HandleCollisions()
    {
        if (_parent.playerInputHandlerComponent.GetMovementDirection().x == 0)
            isColliding = false;
        else
            isColliding = _parent.playerRigid.velocity.magnitude < 0.9f && (triggerCollisionsL || triggerCollisionsR);

        // _parent.playerAnimationComponent.SetColliding(isColliding);
    }

    private void HandleMovement()
    {
        // if (isColliding)
        //     targetSpeed = 0f;
        if (!isCrouched)
            targetSpeed = _parent.playerInputHandlerComponent.GetRunningInput() ? runSpeed : walkSpeed;
        else
            targetSpeed = crouchSpeed;


        switch (movementType)
        {
            case MovementType.Horizontal:
                inputMagnitude = _parent.playerInputHandlerComponent.GetMovementDirection().x;
                if (inputMagnitude == 0) targetSpeed = 0.0f;
                _currentSpeed = inputMagnitude * targetSpeed * Time.deltaTime;
                switch (movementDirection)
                {
                    case MovementDirection.Default:
                        _parent.playerRigid.velocity = new Vector3(0, _parent.playerRigid.velocity.y,
                            _currentSpeed * targetSpeed);
                        break;
                    case MovementDirection.Rot1:
                        _parent.playerRigid.velocity = new Vector3(0, _parent.playerRigid.velocity.y,
                            _currentSpeed * targetSpeed);
                        break;
                    case MovementDirection.Rot2:
                        _parent.playerRigid.velocity = new Vector3(0, _parent.playerRigid.velocity.y,
                            _currentSpeed * targetSpeed);
                        break;
                    case MovementDirection.Rot3:
                        _parent.playerRigid.velocity = new Vector3(-_currentSpeed * targetSpeed,
                            _parent.playerRigid.velocity.y, 0);
                        break;
                }

                break;
            case MovementType.Vertical:
                inputMagnitude = -_parent.playerInputHandlerComponent.GetMovementDirection().y;
                if (inputMagnitude == 0) targetSpeed = 0.0f;
                _currentSpeed = inputMagnitude * targetSpeed * Time.deltaTime;
                _parent.playerRigid.velocity =
                    new Vector3(_currentSpeed * targetSpeed, _parent.playerRigid.velocity.y, 0);

                break;
            // case MovementType.HorizontalAndVertical:
            //     inputMagnitude = _parent.playerInputHandlerComponent.GetMovementDirection().magnitude;
            //     var inputMagnitudeX = _parent.playerInputHandlerComponent.GetMovementDirection().x;
            //     var inputMagnitudeY = -_parent.playerInputHandlerComponent.GetMovementDirection().y;
            //     if (_parent.playerInputHandlerComponent.GetMovementDirection().x == 0 &&
            //         -_parent.playerInputHandlerComponent.GetMovementDirection().y == 0) targetSpeed = 0.0f;
            //     _currentSpeed = inputMagnitude * targetSpeed * Time.deltaTime;
            //     var currentSpeedX = inputMagnitudeX * targetSpeed * Time.deltaTime;
            //     var currentSpeedY = inputMagnitudeY * targetSpeed * Time.deltaTime;
            //     _parent.playerRigid.velocity = new Vector3(currentSpeedY * targetSpeed, _parent.playerRigid.velocity.y,
            //         currentSpeedX * targetSpeed);
            //     break;
        }

        animationBlend = Mathf.Lerp(animationBlend, targetSpeed, Time.deltaTime * _speedChangeRate);
        if (animationBlend < 0.1f) animationBlend = 0f;


        _parent.playerAnimationComponent.SetCharacterSpeed(animationBlend);
        _parent.playerAnimationComponent.SetInputSpeed(inputMagnitude);
    }

    private void HandleStates()
    {
        if (_parent.CurrentPlayerState == PlayerState.Conversation ||
            _parent.CurrentPlayerState == PlayerState.Transition)
        {
            animationBlend = 0;
            _currentSpeed = 0;
            _parent.playerAnimationComponent.SetCharacterSpeed(0);
            _parent.playerAnimationComponent.SetInputSpeed(0);
            return;
        }

        if (isJumping)
            _parent.ChangeState(PlayerState.Jump);
        else if (targetSpeed == runSpeed)
            _parent.ChangeState(PlayerState.Run);
        else if (targetSpeed == walkSpeed)
            _parent.ChangeState(PlayerState.Walk);
        else if (targetSpeed == crouchSpeed || isCrouched)
            _parent.ChangeState(PlayerState.Crouch);
        else if (targetSpeed == 0)
            _parent.ChangeState(PlayerState.Idle);
    }

    private void HandleCrouch()
    {
        if (!isCrouched)
        {
            if (!_parent.playerInputHandlerComponent.GetCrouchingInput()) return;
            isCrouched = true;
            _parent.playerAnimationComponent.SetCrouch(true);
        }
        else
        {
            if (!_parent.playerInputHandlerComponent.GetCrouchingInput()) return;
            isCrouched = false;
            _parent.playerAnimationComponent.SetCrouch(false);
        }
    }

    private void HandleJump()
    {
        if (isGrounded)
        {
            isJumping = false;
            _parent.playerAnimationComponent.SetJump(false);
            _parent.playerAnimationComponent.SetFreeFall(false);

            if (_parent.playerInputHandlerComponent.GetJumpingInput() && _jumpTimeoutDelta <= 0.0f)
            {
                isJumping = true;
                _parent.playerRigid.AddForce(Vector3.up * jumpForce);
                //set state maybe
                _parent.playerAnimationComponent.SetJump(true);
            }

            if (_jumpTimeoutDelta >= 0.0f)
                _jumpTimeoutDelta -= Time.deltaTime;
        }
        else
        {
            _jumpTimeoutDelta = JumpTimeout;
            if (_fallTimeoutDelta >= 0.0f)
                _fallTimeoutDelta -= Time.deltaTime;
            else
                _parent.playerAnimationComponent.SetFreeFall(true);
        }
    }

    public void ChangeMovementDirection(MovementType type, MovementDirection direction)
    {
        movementType = type;
        movementDirection = direction;
    }

    public (MovementType type, MovementDirection direction) GetMovementDirection() =>  (movementType, movementDirection);
    

    #region Stuff

    public void FreezePlayer()
    {
        switch (movementType)
        {
            case MovementType.Horizontal:
                _parent.playerRigid.constraints = RigidbodyConstraints.FreezePositionZ |
                                                  RigidbodyConstraints.FreezeRotation |
                                                  RigidbodyConstraints.FreezePositionX;
                break;
            case MovementType.Vertical:
                _parent.playerRigid.constraints = RigidbodyConstraints.FreezePositionZ |
                                                  RigidbodyConstraints.FreezeRotation |
                                                  RigidbodyConstraints.FreezePositionX;
                break;
   
        }
    }

    public void UnfreezePlayer()
    {
        switch (movementType)
        {
            case MovementType.Horizontal:
                _parent.playerRigid.constraints =
                    RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
                break;
            case MovementType.Vertical:
                _parent.playerRigid.constraints =
                    RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                break;
           
        }
    }

    public void ChangePhysicialMaterialFriction(float value) =>
        _parent.characterCollider.material.dynamicFriction = value;


    private void HandleFlipX()
    {
        float moveDir = 0;
        switch (movementType)
        {
            case MovementType.Horizontal:

                moveDir = _parent.playerInputHandlerComponent.GetMovementDirection().x;

                if (moveDir != 0 && _parent.CurrentPlayerState != PlayerState.Transition &&
                    _parent.CurrentPlayerState != PlayerState.Conversation)
                    _parent.playerAnimationComponent.HandleModelDirection(moveDir, movementType);
                break;
            case MovementType.Vertical:
                moveDir = -_parent.playerInputHandlerComponent.GetMovementDirection().y;
                if (moveDir != 0 && _parent.CurrentPlayerState != PlayerState.Transition &&
                    _parent.CurrentPlayerState != PlayerState.Conversation)
                    _parent.playerAnimationComponent.HandleModelDirection(moveDir, movementType);
                break;
            // case MovementType.HorizontalAndVertical:
            //     float moveDirX = 0, moveDirY = 0;
            //     moveDirX = _parent.playerInputHandlerComponent.GetMovementDirection().x;
            //     moveDirY = -_parent.playerInputHandlerComponent.GetMovementDirection().y;
            //     if ((moveDirX != 0 || moveDirY != 0) && _parent.CurrentPlayerState != PlayerState.Transition &&
            //         _parent.CurrentPlayerState != PlayerState.Conversation)
            //         _parent.playerAnimationComponent.HandleModelDirection(moveDirX, moveDirY, movementType);
            //     break;
        }
    }

    #endregion

    #region TRIGGERS

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ground")) return;
        isGrounded = true;
        _parent.playerAnimationComponent.SetGrounded(isGrounded);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Ground")) return;
        isGrounded = true;
        _parent.playerAnimationComponent.SetGrounded(isGrounded);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ground")) return;
        isGrounded = false;
        _parent.playerAnimationComponent.SetGrounded(isGrounded);
    }

    #endregion

    public enum MovementType
    {
        Horizontal,
        Vertical,
    }

    public enum MovementDirection
    {
        Default, //0 degrees
        Rot1, //90 degrees
        Rot2, //180 degrees
        Rot3, //270 degrees
    }
}