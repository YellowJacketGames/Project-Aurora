using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

//This script manages the dialogue on start in visual novel scenes
public class VisualNovelManager : MonoBehaviour
{
    [SerializeField] TextAsset visualNovelStory;


    public void Start()
    {
        Invoke("BeginVisualNovel", 1f);
    }
    public void BeginVisualNovel()
    {
        if(visualNovelStory != null)
        {
            Story newStory = new Story(visualNovelStory.text);
            GameManager.instance.currentController.playerConversationComponent.GetPlayerSpeaker().currentDirection = InteractDirection.Right;
            GameManager.instance.currentController.playerConversationComponent.SetCurrentDialogue(newStory);
            GameManager.instance.currentController.ChangeState(PlayerState.Conversation);
        }
    }
}
