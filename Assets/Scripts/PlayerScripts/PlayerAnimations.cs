using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is a placeholder for when we have a proper character model available.
//This script takes care of handling the character animator to change into the different animations.
public class PlayerAnimations : PlayerComponent
{
    [Header("Animation Components")] [SerializeField]
    private Animator playerAnimations; //Component to store player animations

    [SerializeField] private bool lerpInTurns;
    [SerializeField] private float rotationSpeed = 2.0f;


    // animation IDs
    private int _animIDSpeed;
    private int _animIDMotionSpeed;
    private int _animIDJump;
    private int _animIDGrounded;
    private int _animIDFreeFall;
    private int _animIDCrouch;
    private int _animIDColliding;

    public override void Awake()
    {
        base.Awake();
        _animIDSpeed = Animator.StringToHash("CharacterSpeed");
        _animIDMotionSpeed = Animator.StringToHash("InputSpeed");
        _animIDCrouch = Animator.StringToHash("Crouch");
        _animIDJump = Animator.StringToHash("Jump");
        _animIDGrounded = Animator.StringToHash("Grounded");
        _animIDColliding = Animator.StringToHash("Colliding");
        _animIDFreeFall = Animator.StringToHash("FreeFall");
    }

    #region Model Rotation

    private void ChangeModelToTheLeft(bool disabled)
    {
        if(disabled) return;
        if (!lerpInTurns)
            _parent.characterModel.transform.localRotation = Quaternion.Euler(0, 180, 0);
        else
            StartCoroutine(LerpRotation(Quaternion.Euler(0, 180, 0)));
        GameManager.instance.currentCameraManager.SetCameraLeftTimer();
    }

    private void ChangeModelToTheFront(bool disabled)
    {
        if(disabled) return;
        if (!lerpInTurns)
            _parent.characterModel.transform.localRotation = Quaternion.Euler(0, -90, 0);
        else
            StartCoroutine(LerpRotation(Quaternion.Euler(0, -90, 0)));
        GameManager.instance.currentCameraManager.SetCameraLeftTimer();
    }

    private void ChangeModelToTheBack(bool disabled)
    {        
        if(disabled) return;
        if (!lerpInTurns)
            _parent.characterModel.transform.localRotation = Quaternion.Euler(0, 90, 0);
        else
            StartCoroutine(LerpRotation(Quaternion.Euler(0, 90, 0)));
        GameManager.instance.currentCameraManager.SetCameraLeftTimer();
    }

    private void ChangeModelToTheRight(bool disabled)
    {
        if(disabled) return;
        if (!lerpInTurns)
            _parent.characterModel.transform.localRotation = Quaternion.Euler(0, 0, 0);
        else
            StartCoroutine(LerpRotation(Quaternion.Euler(0, 0, 0)));
        GameManager.instance.currentCameraManager.SetCameraRightTimer();
    }

    public void HandleModelDirection(float value, PlayerMovement.MovementType movementType, PlayerMovement.MovementDirection movementDirection,bool disabledW,bool disabledS, bool disabledA,bool disabledD)
    {
        switch (movementType)
        {
            case PlayerMovement.MovementType.Horizontal:
                switch (movementDirection)
                {
                    case PlayerMovement.MovementDirection.Default:
                        if (value < 0) ChangeModelToTheLeft(disabledA); else ChangeModelToTheRight(disabledD);
                        break;
                    case PlayerMovement.MovementDirection.Rot1:
                        if (value < 0) ChangeModelToTheRight(disabledA); else ChangeModelToTheLeft(disabledD);
                        break;
                    case PlayerMovement.MovementDirection.Rot2:
                        if (value < 0) ChangeModelToTheLeft(disabledA); else ChangeModelToTheRight(disabledD);
                        break;
                    case PlayerMovement.MovementDirection.Rot3:
                        if (value < 0) ChangeModelToTheBack(disabledA); else ChangeModelToTheFront(disabledD);           
                        break;
                    case PlayerMovement.MovementDirection.Rot4:
                        if (value < 0) ChangeModelToTheFront(disabledA); else ChangeModelToTheBack(disabledD);
                        break;
                    
                }
                break;
            case PlayerMovement.MovementType.Vertical:
                if (value < 0)
                    ChangeModelToTheFront(disabledW);
                else
                    ChangeModelToTheBack(disabledS);
                break;
        }
    }

    private IEnumerator LerpRotation(Quaternion targetRotation)
    {
        Quaternion startRotation = _parent.characterModel.transform.localRotation;
        float timeElapsed = 0;

        while (timeElapsed < 1f)
        {
            _parent.characterModel.transform.localRotation = Quaternion.Lerp(
                startRotation,
                targetRotation,
                timeElapsed
            );

            timeElapsed += Time.deltaTime * rotationSpeed;
            yield return null;
        }

        _parent.characterModel.transform.localRotation = targetRotation;
    }

    // public void HandleModelDirection(float valueX, float valueY, PlayerMovement.MovementType movementType)
    // {
    //     if (movementType != PlayerMovement.MovementType.HorizontalAndVertical) return;
    //
    //     if (valueX < 0)
    //         ChangeModelToTheLeft();
    //     else if (valueX > 0)
    //         ChangeModelToTheRight();
    //
    //     else if (valueY < 0)
    //         ChangeModelToTheFront();
    //     else
    //         ChangeModelToTheBack();
    // }

    #endregion

    #region Player Animations

    public void SetCharacterSpeed(float animationBlend)
    {
        playerAnimations.SetFloat(_animIDSpeed, animationBlend);
    }

    public void SetInputSpeed(float inputMagnitude)
    {
        playerAnimations.SetFloat(_animIDMotionSpeed, inputMagnitude);
    }

    public void SetColliding(bool value)
    {
        playerAnimations.SetBool(_animIDColliding, value);
    }

    public void SetJump(bool value)
    {
        playerAnimations.SetBool(_animIDJump, value);
    }

    public void SetGrounded(bool value)
    {
        playerAnimations.SetBool(_animIDGrounded, value);
    }

    public void SetCrouch(bool value)
    {
        playerAnimations.SetBool(_animIDCrouch, value);
    }

    public void SetFreeFall(bool value)
    {
        playerAnimations.SetBool(_animIDFreeFall, value);
    }

    public void AnimationCrouch(bool value)
    {
        playerAnimations.SetBool("Crouch", value);
    }

    public void AnimationMovement(float speed)
    {
        playerAnimations.SetFloat("CharacterSpeed", speed);
    }

    public void AnimationJump(bool value)
    {
        playerAnimations.SetBool("Jump", value);
    }

    #endregion
}