using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script handles teleporting the player from one location to another
//It's called door since most interactable locations will be doors, but it can be used with different elements
public class DoorElement : InteractableElement
{
    [Header("Door Variables")]
    [SerializeField] protected Transform newPosition;
    [SerializeField] private bool returnFromTransition = true;
    [Space]
    [Header("Door Lock Variables")]
    [SerializeField] bool needKey;
    [SerializeField] ObjectClass keyNeeded;

    protected bool inTransition;
    public override void OnInteract() 
    {
        base.OnInteract();

        if (!needKey) //If we don't need a key, we just go to the location
        {
            OpenDoor();
        }
        else
        {
            if (CheckIfCanOpen())
            {
                UnlockDoor();
                OpenDoor();
            }

            else
            {
                hasBeenInteracted = false;
                //Play door locked sound
                //Since we don't have the key, we can't go to the location
            }
        }
        
    }

    public virtual void OpenDoor()
    {
        //When interacted, we want to set a transition before moving to the new location
        GameManager.instance.currentTransitionManager.SetFadeIn();

        //We deactivate the interaction check since we want to interact with this door again
        hasBeenInteracted = false;
        inTransition = true;

        //Play the sound of the door opening
        AudioManager.instance.Play("DoorOpen");
    }

    public void UnlockDoor()
    {
        //Since we have the key, we can open the door
        needKey = false;

        //We use the item and remove it from the inventory
        GameManager.instance.currentController.playerInventoryComponent.UseItem(keyNeeded);

        //Play unlocking sound
    }
    public virtual void Update()
    {
        if (!GameManager.instance.currentTransitionManager.ReturnTransitionStatus() && inTransition) //If the transition is done, we move the player and then set the transition
        {
            inTransition = false;
            GameManager.instance.currentController.transform.position = newPosition.transform.position;

            Invoke("TransitionDelay", 0.2f); //We invoke the transition method with a little delay so that it doesn't play when the player is moving

        }
    }

    public virtual void TransitionDelay()
    {
        GameManager.instance.currentTransitionManager.SetFadeOut();
    }

    public bool CheckIfCanOpen()
    {
        if(keyNeeded != null)
        {
            if (GameManager.instance.currentController.playerInventoryComponent.CheckIfObjectIsInInventory(keyNeeded))
                return true;
            else
                return false;
        }

        return false;
    }
    #region GetValues

    public bool GetKeyValue()
    {
        return needKey;
    }
    #endregion
}
