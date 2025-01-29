using System;
using UnityEngine;

public class EnableZoltar : LevelEvent
{
    [SerializeField] private ConversationElement zoltarObject;
    private int amount = 0;

    private void Start()
    {
        amount = GameManager.instance.Data.HowManyOf("obj_Tícket_Minigame");
        zoltarObject.ignorePopup = true;
        zoltarObject.disableComponent = true;
        if (amount >= 3)
        {
            zoltarObject.ignorePopup = false;
            zoltarObject.disableComponent = false;
        }
        
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