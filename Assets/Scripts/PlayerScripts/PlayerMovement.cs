using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PlayerScripts;
using UnityEngine;
using UnityEngine.Serialization;


public class PlayerMovement : PlayerComponent
{
    [Header("Movement Variables")] [SerializeField]
    private float _currentSpeed;

    [SerializeField] private MovementType movementType;
    [SerializeField] private MovementDirection movementDirection;
    [SerializeField] public bool horizontalAndVerticalMovement;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float crouchSpeed;

    [Space(10)] [Header("Movement Limits")] [SerializeField]
    private bool disableA;

    [SerializeField] private bool disableD;
    [SerializeField] private bool disableW;
    [SerializeField] private bool disableS;
    // [FormerlySerializedAs("playerSlidesOnSlopes")] [SerializeField] private bool playerDontSlideOnSlopes;

    [Space] [Header("Ground")] [SerializeField]
    private bool isGrounded;

    private CapsuleCollider playerCollider;

    [SerializeField] private Transform playerCenter;
    [SerializeField] private Transform playerLeft;
    [SerializeField] private Transform playerRight;
    [SerializeField] private Transform playerLeft1;
    [SerializeField] private Transform playerRight1;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float playerHeight = 1.8f;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown = .4f;
    [SerializeField] private bool readyToJump = true;
    [SerializeField] private float movementMultiplier = 1.0f;
    [SerializeField] private float airMovementMultiplier = 1.2f;

    private bool hasObstacleAbove;
    private float _fallTimeoutDelta;
    public float FallTimeout = 0.5f;

    [Space] [Header("Checks")] [SerializeField]
    private bool isJumping;

    [SerializeField] private bool isCrouched;
    [SerializeField] private bool isColliding;
    [SerializeField] public bool triggerCollisionsL;
    [SerializeField] public bool triggerCollisionsR;
    [SerializeField] public bool triggerCollisionsF;
    [SerializeField] public bool triggerCollisionsB;

    private List<PlayerCollisionChecks> _collisionChecks;

    // [SerializeField] public bool triggerCollisionsU;
    // [SerializeField] public bool triggerCollisionsD;


    [Header("Rigid Limits")] [SerializeField]
    private RigidbodyConstraints idleConstraints;

    [SerializeField] private RigidbodyConstraints movingConstraints;


    private float targetSpeed = 0f;
    public event Action<float> OnTargetSpeedChanged;
    [SerializeField] public CameraManagerRelativeToMovement relativeToMovementSimpleCamera;
    [SerializeField] private float rotSpeed = 110f;

    public float TargetSpeed
    {
        get { return targetSpeed; }
        set
        {
            if (targetSpeed != value)
            {
                targetSpeed = value;
                OnTargetSpeedChanged?.Invoke(targetSpeed);
            }
        }
    }

    private float inputMagnitude = 0f;
    private float _speedChangeRate = 7.0f; //for the animationBlending
    private float animationBlend;
    [SerializeField] private float gravityValue = 10f;

    #region Booleans

    private bool canMove => _parent.CurrentPlayerState != PlayerState.Conversation &&
                            _parent.CurrentPlayerState != PlayerState.Transition;

    #endregion

    private void Start()
    {
        _fallTimeoutDelta = FallTimeout;
        playerCollider = _parent.characterCollider.GetComponent<CapsuleCollider>();
        _collisionChecks = GetComponentsInChildren<PlayerCollisionChecks>().ToList();
    }

    private void OnEnable()
    {
        // _parent.playerInputHandlerComponent.onJumpPressed.AddListener(HandleJump);
    }

    private void OnDisable()
    {
        // _parent.playerInputHandlerComponent.onJumpPressed.RemoveListener(HandleJump);
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
        GroundCheck();
        HandleJump();
        HandleMovementType();
        HandleMovement();
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
            isColliding = _parent.playerRigid.velocity.magnitude < 0.9f && (triggerCollisionsL || triggerCollisionsR ||
                                                                            triggerCollisionsF || triggerCollisionsB);


        // _parent.playerAnimationComponent.SetColliding(isColliding);
    }

    private void HandleMovement()
    {
        if (!isCrouched)
        {
            TargetSpeed = _parent.playerInputHandlerComponent.GetRunningInput() ? runSpeed : walkSpeed;
            if (!isGrounded && Math.Abs(TargetSpeed - runSpeed) < 0.1f && isColliding)
                TargetSpeed = walkSpeed;
            if (!isGrounded)
                TargetSpeed = runSpeed;
        }
        else
            TargetSpeed = crouchSpeed;

        switch (movementType)
        {
            case MovementType.RelativeToPlayer:
                HandleRelativeMovement();
                break;
            case MovementType.Horizontal:
                inputMagnitude = _parent.playerInputHandlerComponent.GetMovementDirection().x;
                inputMagnitude = ManageDisables(inputMagnitude, true);
                if (inputMagnitude == 0) targetSpeed = 0.0f;
                _currentSpeed = inputMagnitude * targetSpeed * Time.deltaTime;


                if (_currentSpeed != 0)
                    switch (movementDirection)
                    {
                        case MovementDirection.Default:
                            if (triggerCollisionsR)
                            {
                                if (inputMagnitude > 0)
                                    _currentSpeed = 0;
                            }
                            else if (triggerCollisionsL)
                            {
                                if (inputMagnitude < 0)
                                    _currentSpeed = 0;
                            }

                            _parent.playerRigid.velocity = new Vector3(0, _parent.playerRigid.velocity.y,
                                _currentSpeed * targetSpeed * movementMultiplier);
                            break;
                        case MovementDirection.Rot1:
                            if (triggerCollisionsL)
                            {
                                if (inputMagnitude > 0)
                                    _currentSpeed = 0;
                            }
                            else if (triggerCollisionsR)
                            {
                                if (inputMagnitude < 0)
                                    _currentSpeed = 0;
                            }

                            _parent.playerRigid.velocity = new Vector3(0, _parent.playerRigid.velocity.y, -
                                _currentSpeed * targetSpeed * movementMultiplier);
                            break;
                        case MovementDirection.Rot2: //  this one not used for now i guess
                            if (triggerCollisionsR)
                            {
                                if (inputMagnitude > 0)
                                    _currentSpeed = 0;
                            }
                            else if (triggerCollisionsL)
                            {
                                if (inputMagnitude < 0)
                                    _currentSpeed = 0;
                            }

                            _parent.playerRigid.velocity = new Vector3(0, _parent.playerRigid.velocity.y,
                                _currentSpeed * targetSpeed * movementMultiplier);
                            break;
                        case MovementDirection.Rot3:
                            if (triggerCollisionsF)
                            {
                                if (inputMagnitude > 0)
                                    _currentSpeed = 0;
                            }
                            else if (triggerCollisionsB)
                            {
                                if (inputMagnitude < 0)
                                    _currentSpeed = 0;
                            }

                            _parent.playerRigid.velocity = new Vector3(
                                -_currentSpeed * targetSpeed * movementMultiplier,
                                _parent.playerRigid.velocity.y, 0);
                            break;
                        case MovementDirection.Rot4:
                            if (triggerCollisionsB)
                            {
                                if (inputMagnitude > 0)
                                    _currentSpeed = 0;
                            }
                            else if (triggerCollisionsF)
                            {
                                if (inputMagnitude < 0)
                                    _currentSpeed = 0;
                            }

                            _parent.playerRigid.velocity = new Vector3(
                                _currentSpeed * targetSpeed * movementMultiplier,
                                _parent.playerRigid.velocity.y, 0);
                            break;
                    }

                // if (playerDontSlideOnSlopes) _parent.playerRigid.isKinematic = targetSpeed == 0.0f;

                break;

            case MovementType.Vertical:

                inputMagnitude = -_parent.playerInputHandlerComponent.GetMovementDirection().y;
                inputMagnitude = ManageDisables(inputMagnitude, false);
                if (inputMagnitude == 0) targetSpeed = 0.0f;
                _currentSpeed = inputMagnitude * targetSpeed * Time.deltaTime;
                if (_currentSpeed != 0)
                    switch (movementDirection)
                    {
                        case MovementDirection.Default:
                            if (triggerCollisionsB)
                            {
                                if (inputMagnitude > 0)
                                    _currentSpeed = 0;
                            }
                            else if (triggerCollisionsF)
                            {
                                if (inputMagnitude < 0)
                                    _currentSpeed = 0;
                            }

                            _parent.playerRigid.velocity =
                                new Vector3(_currentSpeed * targetSpeed * movementMultiplier,
                                    _parent.playerRigid.velocity.y, 0);
                            break;
                        case MovementDirection.Rot1:
                            if (triggerCollisionsF)
                            {
                                if (inputMagnitude > 0)
                                    _currentSpeed = 0;
                            }
                            else if (triggerCollisionsB)
                            {
                                if (inputMagnitude < 0)
                                    _currentSpeed = 0;
                            }

                            _parent.playerRigid.velocity = new Vector3(
                                -_currentSpeed * targetSpeed * movementMultiplier,
                                _parent.playerRigid.velocity.y, 0);
                            break;
                        case MovementDirection.Rot2: // Not used for now, thats why its broken
                            return;
                            if (triggerCollisionsB)
                            {
                                if (inputMagnitude > 0)
                                    _currentSpeed = 0;
                            }
                            else if (triggerCollisionsF)
                            {
                                if (inputMagnitude < 0)
                                    _currentSpeed = 0;
                            }
                            _parent.playerRigid.velocity = new Vector3(_parent.playerRigid.velocity.x,
                                0, _currentSpeed * targetSpeed * movementMultiplier);
                            break;
                        case MovementDirection.Rot3:
                            if (triggerCollisionsR)
                            {
                                if (inputMagnitude > 0)
                                    _currentSpeed = 0;
                            }
                            else if (triggerCollisionsL)
                            {
                                if (inputMagnitude < 0)
                                    _currentSpeed = 0;
                            }
                            _parent.playerRigid.velocity = new Vector3(0,
                                _parent.playerRigid.velocity.y,
                                _currentSpeed * targetSpeed * movementMultiplier);
                            break;
                        case MovementDirection.Rot4:
                            if (triggerCollisionsL)
                            {
                                if (inputMagnitude > 0)
                                    _currentSpeed = 0;
                            }
                            else if (triggerCollisionsR)
                            {
                                if (inputMagnitude < 0)
                                    _currentSpeed = 0;
                            }
                            _parent.playerRigid.velocity = new Vector3(0,
                                _parent.playerRigid.velocity.y,
                                -_currentSpeed * targetSpeed * movementMultiplier);
                            break;
                    }
                // if (playerDontSlideOnSlopes) _parent.playerRigid.isKinematic = targetSpeed == 0.0f;


                break;
        }

        animationBlend = Mathf.Lerp(animationBlend, targetSpeed, Time.deltaTime * _speedChangeRate);
        if (animationBlend < 0.1f) animationBlend = 0f;

        _parent.playerAnimationComponent.SetCharacterSpeed(animationBlend);
        _parent.playerAnimationComponent.SetInputSpeed(inputMagnitude);
    }

    private void HandleRelativeMovement()
    {
        // Rotation first: rotate left/right with A/D or arrow keys
        var rotationInput = _parent.playerInputHandlerComponent.GetRotationInput().x; // Get rotation input
        if (rotationInput != 0)
        {
            transform.Rotate(Vector3.up, rotationInput * rotSpeed * Time.deltaTime);
        }

        Vector2 movementInput = _parent.playerInputHandlerComponent.GetMovementDirection();
        _currentSpeed = targetSpeed * Time.deltaTime;

        // Only forward/backward input matters for movement (W/S)
        var moveDirection = movementInput.y;

        if (relativeToMovementSimpleCamera)
        {
            switch (moveDirection)
            {
                case < 0:
                    relativeToMovementSimpleCamera.SetCameraInFront();
                    break;
                case > 0:
                    relativeToMovementSimpleCamera.SetCameraInBack();
                    break;
            }
        }

        if (movementInput.y == 0)
            TargetSpeed = 0;
        else if (!isCrouched)
            TargetSpeed = _parent.playerInputHandlerComponent.GetRunningInput() ? runSpeed : walkSpeed;
        else
            TargetSpeed = crouchSpeed;

        // Calculate movement vector in the player's forward direction (using transform.forward)
        var movement = transform.forward * (moveDirection * _currentSpeed * TargetSpeed);

        // Update the player's rigidbody velocity (movement on X and Z axes only)
        _parent.playerRigid.velocity = new Vector3(movement.x, _parent.playerRigid.velocity.y, movement.z);


        animationBlend = Mathf.Lerp(animationBlend, TargetSpeed, Time.deltaTime * _speedChangeRate);
        if (animationBlend < 0.1f) animationBlend = 0f;

        _parent.playerAnimationComponent.SetCharacterSpeed(animationBlend);
        _parent.playerAnimationComponent.SetInputSpeed(moveDirection);
    }


    private float ManageDisables(float inputMagnitude, bool axisX)
    {
        if (axisX)
        {
            if (disableA)
                if (inputMagnitude < 0)
                    return 0;
            if (!disableD) return inputMagnitude;
            if (inputMagnitude > 0)
                return 0;
        }
        else
        {
            if (disableW)
                if (inputMagnitude < 0)
                    return 0;
            if (!disableS) return inputMagnitude;
            if (inputMagnitude > 0)
                return 0;
        }

        return inputMagnitude;
    }

    public void EnableAllInput()
    {
        disableA = false;
        disableD = false;
        disableW = false;
        disableS = false;
        readyToJump = true;
    }

    public void DisableAllInput()
    {
        disableA = true;
        disableD = true;
        disableW = true;
        disableS = true;
        readyToJump = false;
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
            foreach (var cols in _collisionChecks)
                cols.SetCrouchDimensions();
        }
        else
        {
            if (!_parent.playerInputHandlerComponent.GetCrouchingInput()) return;
            if (hasObstacleAbove) return;
            isCrouched = false;
            _parent.playerAnimationComponent.SetCrouch(false);
            foreach (var cols in _collisionChecks)
                cols.ResetDefaultDimensions();
        }
    }

    private void ResetJump()
    {
        Debug.LogWarning("resetjump called");
        readyToJump = true;
        movementMultiplier = 1.0f;
    }

    public void StopPlayerForAFrameAnim()
    {
        _parent.playerRigid.velocity = new Vector3(0, _parent.playerRigid.velocity.y, 0);
    }

    public void JumpPhysicsFromAnim()
    {
        Debug.LogWarning("Jumpphysics called");
        movementMultiplier = airMovementMultiplier;
        readyToJump = false;
        var velocity = _parent.playerRigid.velocity;
        velocity = new Vector3(velocity.x, 0, velocity.y);
        _parent.playerRigid.velocity = velocity;

        Vector3 jumpVelocity = transform.up * jumpForce;
        _parent.playerRigid.velocity = new Vector3(_parent.playerRigid.velocity.x, 0, _parent.playerRigid.velocity.z) +
                                       jumpVelocity;

        // _parent.playerRigid.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        Invoke(nameof(ResetJump), jumpCooldown);
    }

    private void SetColliderToDefaultSize()
    {
        playerCollider.radius = 0.4f;
        playerCollider.height = 2.597304f;
        playerCollider.center = new Vector3(-0.000805974f, 1.280554f, 0.002288818f);
    }

    private void SetColliderToCrouchSize()
    {
        playerCollider.height = 1.55f;
        playerCollider.center = new Vector3(-0.000805974f, 0.77f, 0.002288818f);
    }

    private void SetColliderToJumpSize()
    {
        // playerCollider.height = 1.72f;
        // playerCollider.center = new Vector3(-0.000805974f, 2f, 0.002288818f);
    }

    private void HandleJump()
    {
        if (isGrounded)
        {
            hasObstacleAbove = Physics.Raycast(playerCenter.position, Vector3.up, playerHeight * 0.5f + 0.2f);
            Debug.DrawRay(playerCenter.position, Vector3.up * (playerHeight * 0.5f + 0.2f),
                hasObstacleAbove ? Color.red : Color.green);


            _parent.playerAnimationComponent.SetJump(false);
            _parent.playerAnimationComponent.SetFreeFall(false);

            if (_parent.playerInputHandlerComponent.GetJumpingInput() && readyToJump && !hasObstacleAbove)
            {
                _parent.playerAnimationComponent.SetJump(true);
            }
        }
        else
        {
            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                _parent.playerAnimationComponent.SetFreeFall(true);
            }
        }


        // if (isGrounded)
        // {
        //     isJumping = false;
        //     _parent.playerAnimationComponent.SetJump(false);
        //     _parent.playerAnimationComponent.SetFreeFall(false);
        //
        //     if (_parent.playerInputHandlerComponent.GetJumpingInput() && _jumpTimeoutDelta <= 0.0f)
        //     {
        //         isJumping = true;
        //         _parent.playerRigid.AddForce(Vector3.up * jumpForce);
        //         //set state maybe
        //         _parent.playerAnimationComponent.SetJump(true);
        //     }
        //
        //     if (_jumpTimeoutDelta >= 0.0f)
        //         _jumpTimeoutDelta -= Time.deltaTime;
        // }
        // else
        // {
        //     _jumpTimeoutDelta = JumpTimeout;
        //     if (_fallTimeoutDelta >= 0.0f)
        //         _fallTimeoutDelta -= Time.deltaTime;
        //     else
        //         _parent.playerAnimationComponent.SetFreeFall(true);
        // }
    }

    private void GroundCheck()
    {
        bool isGroundedCenter =
            Physics.Raycast(playerCenter.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);
        bool isGroundedLeft =
            Physics.Raycast(playerLeft.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);
        bool isGroundedRight =
            Physics.Raycast(playerRight.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);

        bool isGroundedLeft1 =
            Physics.Raycast(playerLeft1.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);
        bool isGroundedRight1 =
            Physics.Raycast(playerRight1.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);

        // isGrounded = Physics.Raycast(playerCenter.position, Vector3.down, playerHeight * .5f + .2f, groundLayer);

        Debug.DrawRay(playerCenter.position, Vector3.down * (playerHeight * 0.5f + 0.2f), Color.green);
        Debug.DrawRay(playerLeft.position, Vector3.down * (playerHeight * 0.5f + 0.2f), Color.green);
        Debug.DrawRay(playerRight.position, Vector3.down * (playerHeight * 0.5f + 0.2f), Color.green);
        Debug.DrawRay(playerLeft1.position, Vector3.down * (playerHeight * 0.5f + 0.2f), Color.green);
        Debug.DrawRay(playerRight1.position, Vector3.down * (playerHeight * 0.5f + 0.2f), Color.green);

        isGrounded = isGroundedCenter || isGroundedLeft || isGroundedRight || isGroundedLeft1 || isGroundedRight1;


        _parent.playerAnimationComponent.SetGrounded(isGrounded);
        if (!isGrounded)
        {
            _parent.playerRigid.AddForce(Vector3.down * gravityValue, ForceMode.Acceleration);
            ChangePhysicalMaterialFriction(0f);
            SetColliderToJumpSize();
        }
        else
        {
            ChangePhysicalMaterialFriction(1f);
            if (isCrouched)
                SetColliderToCrouchSize();
            else
                SetColliderToDefaultSize();
        }
    }

    private void ChangePhysicalMaterialFriction(float frictionValue)
    {
        var material = playerCollider.material;
        material.dynamicFriction = frictionValue;
        material.staticFriction = frictionValue;
    }

    public void ChangeMovementDirection(MovementType type, MovementDirection direction)
    {
        movementType = type;
        movementDirection = direction;

        if (!GameManager.instance.currentLevelObjectPoolingManager) return;
        switch (movementDirection)
        {
            case MovementDirection.Rot1:
            case MovementDirection.Default:
                if (GameManager.instance.currentLevelObjectPoolingManager.FallingHatsManagerRef)
                    GameManager.instance.currentLevelObjectPoolingManager.FallingHatsManagerRef.SetAxis(FallingHat.Axis
                        .Z);
                break;
            case MovementDirection.Rot3:
            case MovementDirection.Rot4:
                if (GameManager.instance.currentLevelObjectPoolingManager.FallingHatsManagerRef)
                    GameManager.instance.currentLevelObjectPoolingManager.FallingHatsManagerRef.SetAxis(FallingHat.Axis
                        .X);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public (MovementType type, MovementDirection direction) GetMovementDirection() => (movementType, movementDirection);


    #region Stuff

    public void FreezePlayer()
    {
        _parent.playerRigid.velocity = new Vector3(0, _parent.playerRigid.velocity.y, 0);

        return;
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
        return;
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
            case MovementType.RelativeToPlayer:
                if (_parent.playerInputHandlerComponent.GetMovementDirection().y < 0)
                    _parent.playerAnimationComponent.FlipModelY(true);
                else if (_parent.playerInputHandlerComponent.GetMovementDirection().y > 0)
                    _parent.playerAnimationComponent.FlipModelY(false);


                break;
            case MovementType.Horizontal:

                moveDir = _parent.playerInputHandlerComponent.GetMovementDirection().x;

                if (moveDir != 0 && _parent.CurrentPlayerState != PlayerState.Transition &&
                    _parent.CurrentPlayerState != PlayerState.Conversation)
                    _parent.playerAnimationComponent.HandleModelDirection(moveDir, movementType, movementDirection,
                        disableW, disableS, disableA, disableD);
                break;
            case MovementType.Vertical:
                moveDir = -_parent.playerInputHandlerComponent.GetMovementDirection().y;
                if (moveDir != 0 && _parent.CurrentPlayerState != PlayerState.Transition &&
                    _parent.CurrentPlayerState != PlayerState.Conversation)
                    _parent.playerAnimationComponent.HandleModelDirection(moveDir, movementType, movementDirection,
                        disableW, disableS, disableA, disableD);
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
        // if (!other.CompareTag("Ground")) return;
        // isGrounded = true;
        // _parent.playerAnimationComponent.SetGrounded(isGrounded);
    }

    private void OnTriggerStay(Collider other)
    {
        // if (!other.CompareTag("Ground")) return;
        // isGrounded = true;
        // _parent.playerAnimationComponent.SetGrounded(isGrounded);
    }

    private void OnTriggerExit(Collider other)
    {
        // if (!other.CompareTag("Ground")) return;
        // isGrounded = false;
        // _parent.playerAnimationComponent.SetGrounded(isGrounded);
    }

    #endregion

    public enum MovementType
    {
        Horizontal,
        Vertical,
        RelativeToPlayer,
    }

    public enum MovementDirection
    {
        Default, //0 degrees
        Rot1, //90 degrees
        Rot2, //180 degrees
        Rot3, //270 degrees
        Rot4, //-270 degrees
    }
}