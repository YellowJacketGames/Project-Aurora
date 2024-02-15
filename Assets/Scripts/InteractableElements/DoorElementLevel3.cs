using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DoorElementLevel3 : DoorElement
{
    [Header("Special Door Variables")]
    [SerializeField] private TextAsset wrongDoorConversation;
    private DoorLevelManager owner;

    void Start()
    {
        owner = GameManager.instance.currentLevelManager as DoorLevelManager;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (!GameManager.instance.currentTransitionManager.ReturnTransitionStatus() && inTransition) //If the transition is done, we move the player and then set the transition
        {
            inTransition = false;
        }
    }

    public void SetShadowDirection()
    {
        InteractDirection dir = GameManager.instance.currentController.playerInteractComponent.GetCurrentDirection();

        switch (dir)
        {
            case InteractDirection.Left:
                owner.shadowSpeaker.currentDirection = InteractDirection.Right;
                break;
            case InteractDirection.Right:
                owner.shadowSpeaker.currentDirection = InteractDirection.Left;
                break;
            default:
                break;
        }
    }

    public override void OpenDoor()
    {
        if (owner.CheckIfCorrectDoor(this))
        {
            owner.AdvanceDoorConversation();
        }
        else
        {
            SetShadowDirection();
            GameManager.instance.currentController.playerConversationComponent.SetCurrentDialogue(new Story(wrongDoorConversation.text));
            GameManager.instance.currentController.playerConversationComponent.SetNewSpeaker(owner.shadowSpeaker);
            GameManager.instance.currentController.ChangeState(PlayerState.Conversation);
        }
    }
    public void ReturnToInitialState()
    {
        Invoke("TransitionDelay", 0.2f); //We invoke the transition method with a little delay so that it doesn't play when the player is moving
        GameManager.instance.currentController.transform.position = owner.initialPosition.position;

        owner.ResetDoorLevel();
    }
    public override void TransitionDelay()
    {
        base.TransitionDelay();
    }
}
