using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script handles interactable elements that, when interacted, give you an item.
public class GiveItem : InteractableElement
{
    [SerializeField] private ObjectClass itemToGive;
    private void Start()
    {
        ignorePopup = true;
    }

    public override void OnInteract()
    {
        base.OnInteract();

        switch (itemToGive.GetObjectType())
        {
            case ObjectType.KeyObject:
                GameManager.instance.Data.AddObject(itemToGive.GetId());
                GameManager.instance.currentController.playerInventoryComponent.AddObjectToKeyInventory(itemToGive);

                break;
            case ObjectType.TypeWriterObject:
                GameManager.instance.Data.AddTypewrite(itemToGive.GetId());
                GameManager.instance.currentController.playerInventoryComponent.AddObjectToTypewriterInventory(itemToGive);
                break;
            default:
                break;
        }

        
    }
}
