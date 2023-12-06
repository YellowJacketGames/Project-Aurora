using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is a placeholder for when we have a proper character model available.
//This script takes care of handling the character animator to change into the different animations.
public class PlayerAnimations : PlayerComponent
{
    private void Start()
    {
        //Set default state
        ChangeModelToIdle();

    }

    //This is a placeholder method
    //It activates the idle model and deactivates the crouching one
    public void ChangeModelToIdle()
    {
        _parent.idleModel.gameObject.SetActive(true);
        _parent.crouchingModel.gameObject.SetActive(false);
    }

    //This is a placeholder method
    //It activates the crouching one and deactivates the jumping one
    public void ChangeModelToCrouching()
    {
        _parent.crouchingModel.gameObject.SetActive(true);
        _parent.idleModel.gameObject.SetActive(false);
    }

    public void ChangeModelToTheLeft() //This is a placeholder method before we get a proper character model
    {
        //We check in which state is the player so that he looks in the correct direction
        switch (_parent.CurrentPlayerState)
        {
            case PlayerState.Idle:
                _parent.idleModel.transform.localRotation = Quaternion.Euler(0, 180, 0);
                break;
            case PlayerState.Walk:
                _parent.idleModel.transform.localRotation = Quaternion.Euler(0, 180, 0);

                break;
            case PlayerState.Crouch:
                _parent.crouchingModel.transform.localRotation = Quaternion.Euler(0, 180, 0);
                break;
            default:
                break;
        }
    }

    public void ChangeModelToTheRight() //This is a placeholder method before we get a proper character model
    {
        //We check in which state is the player so that he looks in the correct direction
        switch (_parent.CurrentPlayerState)
        {
            case PlayerState.Idle:
                _parent.idleModel.transform.localRotation = Quaternion.Euler(0, 0, 0);
                break;
            case PlayerState.Walk:
                _parent.idleModel.transform.localRotation = Quaternion.Euler(0, 0, 0);
                break;
            case PlayerState.Crouch:
                _parent.crouchingModel.transform.localRotation = Quaternion.Euler(0, 0, 0);
                break;
            default:
                break;
        }
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
}
