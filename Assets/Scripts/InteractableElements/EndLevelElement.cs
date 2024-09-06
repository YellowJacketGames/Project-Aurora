using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelElement : InteractableElement
{
    private void Start()
    {
        ignorePopup = true;
    }

    public override void OnInteract()
    {
        base.OnInteract();
        GameManager.instance.currentTransitionManager.NextLevel();
    }
}
