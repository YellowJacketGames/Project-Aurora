using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;

//This script handles dialogue and conversations the player might have at any moment in the level
//It checks the input of the UI to choose options and to read lines.

public class PlayerConversation : PlayerComponent
{
    [Header("Dialogue Components")]
    private Story currentDialogue;


    //This variables are a placeholder measure to stop the input from skipping all dialogue as soon as they press it
    //I will try to fix it later on
    bool skipLineCooldown;
    [SerializeField] float skipLineTimer;
    float timer;

    //This are variables to fix a small bug with the input of both action maps clashing when the dialogue is finished.
    [SerializeField] float storyFinishedTimer = 2f;
    bool storyfinished; //bool to check if the story is done

    [Header("Choices Components")]
    [SerializeField] private List<ChoiceClass> dialogueChoices;
    private void Start()
    {
        timer = skipLineTimer;
    }
    public void BeginStory()
    {
        if (currentDialogue.canContinue)
        {
            _parent.playerUIComponent.ChangeConversationText(currentDialogue.Continue());
            DisplayChoices();
            HandleSpeakers();
        }
    }
    private void Update()
    {
        
        if(_parent.CurrentPlayerState == PlayerState.Conversation)
        {
            if (_parent.playerInputHandlerComponent.GetAcceptInput() && !skipLineCooldown)
            {

                if (currentDialogue.canContinue)
                {
                    skipLineCooldown = true;
                    _parent.playerUIComponent.ChangeConversationText(currentDialogue.Continue());
                    HandleSpeakers();
                    DisplayChoices();
                }
                else
                {
                    _parent.playerUIComponent.HideConversationBox();
                    storyfinished = true;
                    skipLineCooldown = false;
                }
            }

            if (skipLineCooldown) //this is a placeholder solution to make the player not skip the entire dialogue by just pressing a button
            {
                timer -= Time.deltaTime;

                if(timer <= 0)
                {
                    skipLineCooldown = false;
                    timer = skipLineTimer;
                }
            }

            if (storyfinished)
            {
                storyFinishedTimer -= Time.deltaTime;

                if(storyFinishedTimer <= 0)
                {
                    storyFinishedTimer = 2f;
                    storyfinished = false;
                    _parent.ChangeState(PlayerState.Idle);
                }
            }
        }

        
    }

    public void SetCurrentDialogue(Story dialogue)
    {
        currentDialogue = dialogue;
    }

    #region Choices
    public void DisplayChoices() //Method to display the different choices
    {
        if(currentDialogue.currentChoices.Count > 0) //Execute only if there are choices to display
        {
            for (int i = 0; i < currentDialogue.currentChoices.Count; i++) //Go through every choice avaible and fill out the variables
            {
                if (dialogueChoices[i] == null)
                    break;

                dialogueChoices[i].SetChoice(currentDialogue.currentChoices[i]);
            }

            StartCoroutine(SelectFirstChoice(dialogueChoices[0].ReturnParent()));
        }
    }

    IEnumerator SelectFirstChoice(GameObject firstChoice) // Coroutine to set the first selectable option
    {
        //Event System requires to be cleared first and then assigned in a different frame
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(firstChoice);
    }

    
    public void AssignChoices(int index) //Method to execute the coices when it appears
    {
        currentDialogue.ChooseChoiceIndex(index);

        foreach(ChoiceClass c in dialogueChoices)
        {
            c.ReturnParent().SetActive(false);
        }
    }

    #endregion


    #region Speakers

    private void HandleSpeakers() //This is a method to get the speakers in a dialogue and give them the reference to the UI
    {
        if(currentDialogue.currentTags.Count > 0) //if we have a tag, we perform the 
        {
            foreach (string tag in currentDialogue.currentTags)
            {
                string[] splitTag = tag.Split(":");

                string tagKey = splitTag[0];
                string valueKey = splitTag[1];

                switch (tagKey)
                {
                    case "speaker":
                        _parent.playerUIComponent.SetDialogueLayout(GameManager.instance.speakerManager.ReturnSpeaker(valueKey));
                        Debug.Log("Current Speaker: " + valueKey);
                        break;
                    default:
                        Debug.LogWarning("Tag not found use for");
                        break;
                }
            }
        }
    }
    #endregion
}
