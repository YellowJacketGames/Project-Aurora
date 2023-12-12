using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabItem : InteractableElement
{
    [Header("Item Variables")]
    [SerializeField] private ObjectClass assignedObject;
    public override void OnInteract()
    {
        base.OnInteract();

        //When interacted, we add the object to the player inventory
        GameManager.instance.currentController.playerInventoryComponent.AddObjectToKeyInventory(assignedObject);

        //For now, we will just destroy the object when the player picks it up
        Destroy(this.gameObject);
    }
}
