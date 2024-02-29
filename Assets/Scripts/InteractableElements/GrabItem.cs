using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class GrabItem : InteractableElement
{
    [Header("Item Variables")]
    [SerializeField] private ObjectClass assignedObject;

    private void Start()
    {
        if(assignedObject.GetObjectType() == ObjectType.TypeWriterObject)
        {
            Animator anim = GetComponent<Animator>();
            anim.SetFloat("keyIndex", assignedObject.assignedLetterIndex);
        }
    }
    public override void OnInteract()
    {
        base.OnInteract();

        switch (assignedObject.GetObjectType())
        {
            case ObjectType.KeyObject:
                //When interacted, we add the object to the player inventory
                GameManager.instance.currentController.playerInventoryComponent.AddObjectToKeyInventory(assignedObject);

                //For now, we will just destroy the object when the player picks it up
                Destroy(this.gameObject);
                break;
            case ObjectType.TypeWriterObject:
                //This is a collectable, so it will trigger a conversation as well
                //When interacted, we add the object to the player inventory
                GameManager.instance.currentController.playerInventoryComponent.AddObjectToTypewriterInventory(assignedObject);

                //We set the current conversation and we set it
                Story dialogue = new Story(assignedObject.GetDialogue().text);

                GameManager.instance.currentController.playerConversationComponent.SetCurrentDialogue(dialogue);
                GameManager.instance.currentController.ChangeState(PlayerState.Conversation);

                //For now, we will just destroy the object when the player picks it up
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
        
    }
}
