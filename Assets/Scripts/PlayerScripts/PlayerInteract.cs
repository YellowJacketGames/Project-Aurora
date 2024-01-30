using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This component will check if the player can interact with the elements around it and
//execute the logic accordingly.
public class PlayerInteract : PlayerComponent
{
    [Header("Interact Variables")]
    private InteractableElement currentElement;
    //bool to check if the player can interact with the other elements
    bool shouldInteract => _parent.CurrentPlayerState != PlayerState.Jump && _parent.CurrentPlayerState != PlayerState.Transition && _parent.CurrentPlayerState != PlayerState.Conversation &&
        currentElement != null && _parent.playerInputHandlerComponent.GetInteractInput() && GameManager.instance.CanPlay();

    private InteractDirection direction;
    public void GetCurrentElement(InteractableElement newElement)
    {
        currentElement = newElement;
    }

    public InteractDirection GetCurrentDirection()
    {
        return direction;
    }

    private void Update()
    {
        if (shouldInteract) //We check every frame if the player can interact
        {
            if(!currentElement.hasBeenInteracted)
            {
                //Get direction
                if(transform.position.z < currentElement.gameObject.transform.position.z)
                {
                    direction = InteractDirection.Right;
                }
                else
                {
                    direction = InteractDirection.Left;
                }

                _parent.playerConversationComponent.GetPlayerSpeaker().currentDirection = direction;
                currentElement.OnInteract();
                _parent.playerUIComponent.HideInteractPrompt();
            }
        }
    }
}


public enum InteractDirection
{
    Left, Right
}
