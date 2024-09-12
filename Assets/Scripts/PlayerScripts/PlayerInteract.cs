using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//This component will check if the player can interact with the elements around it and
//execute the logic accordingly.
public class PlayerInteract : PlayerComponent
{
    [Header("Interact Variables")]
    [SerializeField]private InteractableElement currentElement;
    //bool to check if the player can interact with the other elements
    bool shouldInteract => _parent.CurrentPlayerState != PlayerState.Jump &&
                           _parent.CurrentPlayerState != PlayerState.Transition && 
                           _parent.CurrentPlayerState != PlayerState.Conversation &&
                           currentElement != null && CanInteract && GameManager.instance.CanPlay();

    public bool CanInteract = true;
    private InteractDirection direction;
    public override void Awake()
    {
        base.Awake(); CanInteract = true;
    }

    public void SetCurrentElement(InteractableElement newElement)
    {
        currentElement = newElement;
    }

    private void OnEnable()
    {
        _parent.playerInputHandlerComponent.PlayerInput.PlayerMovement.Interact.performed += TryToInteract;

    }

    private void OnDisable()
    {
        _parent.playerInputHandlerComponent.PlayerInput.PlayerMovement.Interact.performed -= TryToInteract;
    }

    private void TryToInteract(InputAction.CallbackContext obj)
    {
        if(shouldInteract)
            Interact();
    }

    public InteractableElement GetCurrentElement()
    {
        return currentElement;
    }
    public InteractDirection GetCurrentDirection()
    {
        return direction;
    }

    private void Interact()
    {
        direction = transform.position.z < currentElement.gameObject.transform.position.z ? InteractDirection.Right : InteractDirection.Left;
        _parent.playerConversationComponent.GetPlayerSpeaker().currentDirection = direction;
        currentElement.OnInteract();
        currentElement.HideInteractPrompt();
        _parent.playerUIComponent.HideInteractPrompt();
    }
//     public virtual void Update()
//     {
//         if (shouldInteract) //We check every frame if the player can interact
//         {
//             //Get direction
//            Interact();
//         }
//     }
// }

}

public enum InteractDirection
{
    Left, Right
}
