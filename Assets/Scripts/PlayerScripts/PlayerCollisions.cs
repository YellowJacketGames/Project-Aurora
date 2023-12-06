using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script will handle player collisions and trigger detection for conversations and interactible elements
public class PlayerCollisions : PlayerComponent
{
    [Header("Uncrouch Check")]
    [SerializeField] private float crouchCheckLength;    //This variable will define how long the raycast for the crouching check will be 
    [SerializeField] private LayerMask crouchingCheckLayer;    //This variable limits the crouching check to this layer only

    [Header("Interaction Checks")]
    [SerializeField] private float interactionRadius;  //This variable indicates how big is the radius for the interaction spehere
    [SerializeField] private LayerMask interactionCheckLayer;    //This variable limits the crouching check to this layer only



    //This region checks movement collision checks
    #region Movement Checks
    public bool CheckCrouching() //this method throw a raycast upwards that checks if there is a collision and if the player can uncrouch
    {
        return Physics.Raycast(transform.position, Vector3.up * crouchCheckLength);
    }

    #endregion


    //This region holds all the methods to check for interaction
    #region Interaction Checks

    public InteractableElement ReturnClosestInteractableObject() //this method returns the closest interactable objects in range
    {
        List<Collider> interactable = new List<Collider>(Physics.OverlapSphere(transform.position, interactionRadius, interactionCheckLayer)); //We get the list of all the elements in the sphere
         
        if(interactable.Count > 0)
        {
            if (interactable[0].TryGetComponent<InteractableElement>(out InteractableElement element)) //We see if we can get the Interactable Component Script
            {
                _parent.playerInteractComponent.GetCurrentElement(element);
                return element;
            }
            else
            {
                Debug.LogWarning("The element: " + interactable[0].name + " does not possess a interactable element component");
                _parent.playerInteractComponent.GetCurrentElement(null);

                return null;
            }
        }

        _parent.playerInteractComponent.GetCurrentElement(null);
        return null;
        
    }

    #endregion


    private void Update()
    {
        if(_parent.CurrentPlayerState != PlayerState.Jump && _parent.CurrentPlayerState != PlayerState.Transition && _parent.CurrentPlayerState != PlayerState.Conversation)
        {
            if (ReturnClosestInteractableObject() != null) //If there is an interactable element nearby, give the player the option to interact with it
            {
                if (!ReturnClosestInteractableObject().hasBeenInteracted)
                {
                    //Show UI prompt
                    
                    _parent.playerUIComponent.ShowInteractPrompt(ReturnClosestInteractableObject());
                }
                else
                {
                    _parent.playerUIComponent.HideInteractPrompt();
                }
            }
            else
            {
                _parent.playerUIComponent.HideInteractPrompt();
            }
        }

        else
        {
            _parent.playerUIComponent.HideInteractPrompt();
        }
    }
}
