using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorElement : InteractableElement
{
    [Header("Door Variables")]
    [SerializeField] Transform newPosition;

    public override void OnInteract()
    {
        base.OnInteract();
        GameManager.instance.currentController.transform.localPosition = newPosition.position;
        hasBeenInteracted = false;
    }

}
