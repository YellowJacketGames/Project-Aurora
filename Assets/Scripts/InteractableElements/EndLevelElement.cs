using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelElement : InteractableElement
{
    public override void OnInteract()
    {
        base.OnInteract();
        GameManager.instance.currentTransitionManager.NextLevel();
    }
}
