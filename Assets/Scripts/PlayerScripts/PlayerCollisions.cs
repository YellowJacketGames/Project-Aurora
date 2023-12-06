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

    [SerializeField] List<Collider> placeholder;
    //This region checks movement collision checks
    #region Movement Checks
    public bool CheckCrouching() //this method throw a raycast upwards that checks if there is a collision and if the player can uncrouch
    {
        return Physics.Raycast(transform.position, Vector3.up * crouchCheckLength);
    }

    #endregion


    //This region holds all the methods to check for interaction
    #region Interaction Checks

    public List<Collider> ReturnInteractableObjects() //this method returns the closest interactable objects in range
    {
        return new List<Collider>(Physics.OverlapSphere(transform.position, interactionRadius, interactionCheckLayer));
    }

    #endregion

}
