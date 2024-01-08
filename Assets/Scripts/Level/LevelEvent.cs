using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a parent class to store the events that happen in the level 
//Each level will have a different set of events
public class LevelEvent : MonoBehaviour
{
    protected bool eventRunning; //Variable to check if the current event is executing.
    private bool hasTriggeredEvent; //Variable to check if the current event has been triggered.
    public virtual void OnEvent() //Every event should activate this checks, so we add a virtual method that does this.
    {
        eventRunning = true;
        hasTriggeredEvent = true;
    }


    //This region holds methods to get the different event checks.
    #region Event checks
    public bool IsEventRunning()
    {
        return eventRunning;
    }
    public bool HasBeenTriggered()
    {
        return hasTriggeredEvent;
    }

    #endregion
}
