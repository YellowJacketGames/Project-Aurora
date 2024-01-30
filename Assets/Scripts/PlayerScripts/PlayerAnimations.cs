using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is a placeholder for when we have a proper character model available.
//This script takes care of handling the character animator to change into the different animations.
public class PlayerAnimations : PlayerComponent
{

    [Header("Animation Components")]
    [SerializeField] private Animator playerAnimations; //Component to store player animations

    #region Model Rotation

    public void ChangeModelToTheLeft() //This is a placeholder method before we get a proper character model
    {
        //We check in which state is the player so that he looks in the correct direction
        _parent.characterModel.transform.localRotation = Quaternion.Euler(0, 180, 0);
        GameManager.instance.currentCameraManager.SetCameraLeftTimer();
    }

    public void ChangeModelToTheRight() //This is a placeholder method before we get a proper character model
    {
        //We check in which state is the player so that he looks in the correct direction
        _parent.characterModel.transform.localRotation = Quaternion.Euler(0, 0, 0);
        GameManager.instance.currentCameraManager.SetCameraLeftTimer();
    }

    public void HandleModelDirection(float value)
    {
        if(value < 0)
        {
            ChangeModelToTheLeft();
        }
        else
        {
            ChangeModelToTheRight();
        }
    }

    #endregion

    #region Player Animations
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
