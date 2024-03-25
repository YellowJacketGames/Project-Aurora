using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class TriggerConversation : MonoBehaviour
{
    bool conversationTriggered;
    [SerializeField] TextAsset conversationToTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !conversationTriggered)
        {
            conversationTriggered = true;
            GameManager.instance.currentController.playerConversationComponent.SetCurrentDialogue(new Story(conversationToTrigger.text));
            GameManager.instance.currentController.ChangeState(PlayerState.Conversation);
        }
    }
}
