using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.Serialization;

public class ConversationElement : InteractableElement
{
    [FormerlySerializedAs("elementDialogue")] [Header("Conversation")] [SerializeField]
    TextAsset elementDialogueESP;

    [SerializeField] TextAsset elementDialogueENG;

    [SerializeField] Speaker conversationSpeaker;

    [ContextMenu("On Interact")]
    public override void OnInteract()
    {
        TextAsset usableText = null;
        if(elementDialogueENG != null && elementDialogueESP != null)
            usableText = GameManager.instance.IsSpanishSet() ? elementDialogueESP : elementDialogueENG;

        //temp to allow dialogues while there is no ENG file yet 
        if (elementDialogueENG == null)
            usableText = elementDialogueESP;
        //if we forgot to add the dialogue asset to the element, it should warn us and not execute the code
        if (usableText != null)
        {
            Story dialogue = new Story(usableText.text);

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
        elementDialogueESP = dialogue;
    }

    public override bool HasDialogue()
    {
        TextAsset usableText = GameManager.instance.IsSpanishSet() ? elementDialogueESP : elementDialogueENG;
        return usableText != null;
    }
}