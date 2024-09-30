using Ink.Runtime;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DialogueTrigger : MonoBehaviour
{
    
    [SerializeField] private TextAsset dialogue;
    private bool _interacted; 
    
    private void OnTriggerEnter(Collider other)
    {
        if(!other.tag.Equals("Player") || _interacted) return;
        other.GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
        GameManager.instance.currentController.playerConversationComponent.SetCurrentDialogue(new Story(dialogue.text));
        GameManager.instance.currentController.ChangeState(PlayerState.Conversation);
        _interacted = true;
    }
    
    
}