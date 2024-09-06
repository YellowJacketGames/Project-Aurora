using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class GrabItem : InteractableElement
{
    [Header("Item Variables")]
    [SerializeField] private ObjectClass assignedObject;

    [SerializeField] bool useCollision;

    private void Start()
    {
        ignorePopup = true;
        if(assignedObject.GetObjectType() == ObjectType.TypeWriterObject)
        {
            //Implement if player already has this key
            if (GameManager.instance.CheckIfAlreadyHasTypewriter(assignedObject))
            {
                Destroy(gameObject);
            }
            //We set the typewriter animations
            Animator anim = GetComponent<Animator>();
            anim.SetFloat("keyIndex", assignedObject.assignedLetterIndex);
        }
    }
    public override void OnInteract()
    {
        if (useCollision)
            return;

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
                //For now, we won't have a conversation when grabbing items

                //Story dialogue = new Story(assignedObject.GetDialogue().text);

                //GameManager.instance.currentController.playerConversationComponent.SetCurrentDialogue(dialogue);
                //GameManager.instance.currentController.ChangeState(PlayerState.Conversation);

                //For now, we will just destroy the object when the player picks it up
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!useCollision)
            return;

        if (other.CompareTag("Player"))
        {
            //When interacted, we add the object to the player inventory
            GameManager.instance.currentController.playerInventoryComponent.AddObjectToTypewriterInventory(assignedObject);

            //For now, we will just destroy the object when the player picks it up
            Destroy(this.gameObject);
        }
    }
}
