using System;
using UnityEngine;

public class EnableZoltar : LevelEvent
{
    [SerializeField] private ConversationElement zoltarObject;
    private int amount = 0;

    private void Start()
    {
        zoltarObject.ignorePopup = true;
        zoltarObject.disableComponent = true;
    }

    public void EnableZoltarDialogation()
    {
        amount++;

        if (amount >= 3)
        {
            zoltarObject.ignorePopup = false;
            zoltarObject.disableComponent = false;
        }
    }
}