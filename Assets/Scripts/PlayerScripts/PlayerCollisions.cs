using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script will handle player collisions and trigger detection for conversations and interactible elements
public class PlayerCollisions : PlayerComponent
{
    [Header("Interaction Checks")] [SerializeField]
    private LayerMask interactionCheckLayer; //This variable limits the crouching check to this layer only

    //
    //    [Header("Uncrouch Check")] [SerializeField]
    // private float crouchCheckLength; //This variable will define how long the raycast for the crouching check will be 
    // [SerializeField]
    // private LayerMask crouchingCheckLayer; //This variable limits the crouching check to this layer only

    // #region Movement Checks
    //
    // public bool
    //     CheckCrouching() //this method throw a raycast upwards that checks if there is a collision and if the player can uncrouch
    // {
    //     return Physics.Raycast(transform.position, Vector3.up * crouchCheckLength);
    // }
    //
    // #endregion
    //

    //This region holds all the methods to check for interaction

    #region Interaction Checks


    private void OnTriggerEnter(Collider other)
    {
        if (_parent.CurrentPlayerState == PlayerState.Jump || _parent.CurrentPlayerState == PlayerState.Transition ||
            _parent.CurrentPlayerState == PlayerState.Conversation) return;
        if (!other.TryGetComponent<InteractableElement>(out InteractableElement element)) return;
        _parent.playerInteractComponent.SetCurrentElement(element);
        if (!element.ignoreInteraction)
            element.ShowInteractPrompt();
        // _parent.playerUIComponent.ShowInteractPrompt(element);  -- antiguo, abre en el HUD
    }

    private void OnTriggerStay(Collider other)
    {
        if (_parent.CurrentPlayerState == PlayerState.Jump || _parent.CurrentPlayerState == PlayerState.Transition ||
            _parent.CurrentPlayerState == PlayerState.Conversation) return;
        if (!other.TryGetComponent<InteractableElement>(out InteractableElement element)) return;
        _parent.playerInteractComponent.SetCurrentElement(element);
        if (!element.ignoreInteraction)
            element.ShowInteractPrompt();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<InteractableElement>(out InteractableElement element)) return;
        element.HideInteractPrompt();
        // _parent.playerUIComponent.HideInteractPrompt();  -- antiguo, cierra en el HUD
        _parent.playerInteractComponent.SetCurrentElement(null);
    }

    #endregion
}