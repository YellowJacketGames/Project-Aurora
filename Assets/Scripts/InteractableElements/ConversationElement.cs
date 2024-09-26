using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class ConversationElement : InteractableElement
{
    [Header("Conversation")] [SerializeField]
    TextAsset elementDialogue;

    [SerializeField] Speaker conversationSpeaker;

    [ContextMenu("On Interact")]
    public override void OnInteract()
    {
        //if we forgot to add the dialogue asset to the element, it should warn us and not execute the code
        if (elementDialogue != null)
        {
            Story dialogue = new Story(elementDialogue.text);

            if (conversationSpeaker != null)
            {
                GameManager.instance.currentController.playerConversationComponent.SetNewSpeaker(conversationSpeaker);

                switch (GameManager.instance.currentController.playerConversationComponent.GetPlayerSpeaker()
                            .currentDirection)
                {
                    case InteractDirection.Left:
                        conversationSpeaker.currentDirection = InteractDirection.Right;
                        break;
                    case InteractDirection.Right:
                        conversationSpeaker.currentDirection = InteractDirection.Left;
                        break;
                    default:
                        break;
                }
            }

            GameManager.instance.currentController.playerConversationComponent.SetCurrentDialogue(dialogue);
            GameManager.instance.currentController.ChangeState(PlayerState.Conversation);
        }
        else
        {
            Debug.LogWarning("The element " + elementName +
                             " is a dialogue element and does not possess a ink story file");
            return;
        }

        base.OnInteract();
    }

    public void ChangeDialogue(TextAsset dialogue)
    {
        elementDialogue = dialogue;
    }

    public override bool HasDialogue()
    {
        return elementDialogue != null;
    }
}