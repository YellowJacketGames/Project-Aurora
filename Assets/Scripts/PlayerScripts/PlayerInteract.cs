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
        currentElement != null && _parent.playerInputHandlerComponent.GetInteractInput() && GameManager.instance.GetCurrentGameState()!= GameStates.Pause; 

    public void GetCurrentElement(InteractableElement newElement)
    {
        currentElement = newElement;
    }

    private void Update()
    {
        if (shouldInteract) //We check every frame if the player can interact
        {
            if(!currentElement.hasBeenInteracted)
            {
                currentElement.OnInteract();
                _parent.playerUIComponent.HideInteractPrompt();
            }
        }
    }
}
