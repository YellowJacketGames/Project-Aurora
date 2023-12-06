using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script will handle player collisions and trigger detection for conversations and interactible elements
public class PlayerCollisions : PlayerComponent
{
    [Header("Uncrouch Check")]
    //This variable will define how long the raycast for the crouching check will be 
    [SerializeField] private float crouchCheckLength;
    [SerializeField] LayerMask crouchingCheckLayer;

    //This region checks movement collision checks
    #region Movement Checks
    public bool CheckCrouching() //this method throw a raycast upwards that checks if there is a collision and if the player can uncrouch
    {
        return Physics.Raycast(transform.position, Vector3.up * crouchCheckLength);
    }

    #endregion

}
