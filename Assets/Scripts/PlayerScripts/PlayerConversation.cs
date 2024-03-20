using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UINavigation;

//This script handles dialogue and conversations the player might have at any moment in the level
//It checks the input of the UI to choose options and to read lines.

public class PlayerConversation : PlayerComponent
{
    [Header("Dialogue Components")]
    private Story currentDialogue;

    //This are variables to fix a small bug with the input of both action maps clashing when the dialogue is finished.
    [SerializeField] float storyFinishedTimer = 0.5f;
    float _storyFinishedTimer;
    bool storyfinished; //bool to check if the story is done
    [Header("Choices Components")]
    [SerializeField] private List<ChoiceClass> dialogueChoices;
    private NavigateOptions choiceNavigation = new NavigateOptions();

    private Coroutine displayLine; //Variable to stop the coroutine
    public bool canContinue;

    [Space]
    [Header("Speaker Variables")]
    [SerializeField] private Speaker playerSpeaker;
    private Speaker newSpeaker;


    private void Start()
    {
        canContinue = true;
        _storyFinishedTimer = storyFinishedTimer;
    }
    public void BeginStory()
    {
        SetStoryExternalFunctions();

        if (currentDialogue.canContinue)
        {
            displayLine = StartCoroutine(_parent.playerUIComponent.DisplayLine(currentDialogue.Continue()));
            //DisplayChoices();
            HandleTags();
        }
    }

    public void BeginStoryWithoutFunctions()
    {
        if (currentDialogue.canContinue)
        {
            displayLine = StartCoroutine(_parent.playerUIComponent.DisplayLine(currentDialogue.Continue()));
            //DisplayChoices();
            HandleTags();
        }
    }
    public void SetStoryExternalFunctions()
    {
        #region Set external functions

        currentDialogue.BindExternalFunction("CheckIfHasItem", (string itemId) =>
        {
            if (_parent.playerInventoryComponent.CheckIfObjectIsInInventory(itemId))
            {
                currentDialogue.variablesState["hasItem"] = true;
            }
            else
            {
                currentDialogue.variablesState["hasItem"] = false;
            }

        });

        currentDialogue.BindExternalFunction("CheckIfHasQuest", (int questIndex) =>
        {
            if (GameManager.instance.questManager.HasQuest(questIndex))
            {
                currentDialogue.variablesState["hasQuest"] = true;
            }
            else
            {
                currentDialogue.variablesState["hasQuest"] = false;
            }

        });

        currentDialogue.BindExternalFunction("GoToNextObjective", (string none) =>
        {
            GameManager.instance.questManager.UpdateObjective();
        });

        currentDialogue.BindExternalFunction("CallEvent", (int eventIndex) =>
        {
            GameManager.instance.currentLevelManager.TriggerEvent(eventIndex);
        });

        currentDialogue.BindExternalFunction("StopTyping", (string none) =>
        {
            canContinue = false;
        });


        currentDialogue.BindExternalFunction("NextLevel", (string none) =>
        {
            GameManager.instance.currentLevelManager.SetNextLevel();
            GameManager.instance.currentTransitionManager.NextLevel();
        });

        currentDialogue.BindExternalFunction("GoToMainMenu", (string none) =>
        {
            GameManager.instance.currentTransitionManager.GoToMainMenu();
        });
        currentDialogue.BindExternalFunction("BeginTransition", (string none) =>
        {
            GameManager.instance.currentTransitionManager.CompleteTransition();

        });

        currentDialogue.BindExternalFunction("PlayAudio", (string audioName) =>
        {
            AudioManager.instance.Play(audioName);
        });

        currentDialogue.BindExternalFunction("SetNewSpeaker", (string newSpeaker) =>
        {
            Speaker s = Resources.Load("ScriptableObjects/Speakers/" + newSpeaker) as Speaker;
            

            switch (GameManager.instance.currentController.playerInteractComponent.GetCurrentDirection())
            {
                case InteractDirection.Left:
                    s.currentDirection = InteractDirection.Right;
                    break;
                case InteractDirection.Right:
                    s.currentDirection = InteractDirection.Left;
                    break;
                default:
                    break;
            }
            SetNewSpeaker(s);
        });

        currentDialogue.BindExternalFunction("ChangeDialogue", (string dialogueFilePath) =>
        {
            ConversationElement e = _parent.playerInteractComponent.GetCurrentElement() as ConversationElement;
            e.ChangeDialogue(Resources.Load(dialogueFilePath) as TextAsset);
        });

        currentDialogue.BindExternalFunction("ChangeDoorDialogue", (string dialogueFilePath) =>
        {
            DoorElementLevel3 e = _parent.playerInteractComponent.GetCurrentElement() as DoorElementLevel3;
            e.ChangeDialogue(Resources.Load(dialogueFilePath) as TextAsset);
        });

        currentDialogue.BindExternalFunction("HasInteractedCheck", (string none) =>
        {
            _parent.playerInteractComponent.GetCurrentElement().hasBeenInteracted = true;
        });

        currentDialogue.BindExternalFunction("CheckIfHasInteracted", (string none) =>
        {
            currentDialogue.variablesState["hasInteracted"] = _parent.playerInteractComponent.GetCurrentElement().hasBeenInteracted;

        });

        #region Level 3 specific Conversation Methods

        currentDialogue.BindExternalFunction("ResetDoors", (string none) =>
        {
            DoorLevelManager specialManager = GameManager.instance.currentLevelManager as DoorLevelManager;
            specialManager.ResetDoorLevel();
            
        });

        currentDialogue.BindExternalFunction("AdvanceDoors", (string none) =>
        {
            DoorLevelManager specialManager = GameManager.instance.currentLevelManager as DoorLevelManager;
            specialManager.ProgressLevel();

        });

        #endregion

        #endregion
    }

    public void FixedUpdate()
    {       
        if (_parent.CurrentPlayerState == PlayerState.Conversation) //We only execute this if we're in the conversation state
        {
            if (_parent.playerInputHandlerComponent.GetAcceptInput()) //If we input the continue button, it will continue the story
            {
                if (canContinue || currentDialogue.canContinue)
                {
                    ContinueStory();
                }
            }

            if (storyfinished) //if the story is done, we clean it up
            {
                FinishStory();
            }
        }
    }

    public void SetNewSpeaker(Speaker s)
    {
        newSpeaker = s;
    }

    public Speaker GetPlayerSpeaker()
    {
        return playerSpeaker;
    }

    #region Story 
    public void ContinueStory() //Method to make story continue
    {
        if (currentDialogue.canContinue) // The first condition is if the story can continue to the next line
        {
            if (!_parent.playerUIComponent.ReturnTypingStatus()) //Check to make sure we're not overwriting 
            {
                //If we're not, we begin the write text coroutine
                displayLine = StartCoroutine(_parent.playerUIComponent.DisplayLine(currentDialogue.Continue()));

                //We check which speaker is actually talking to set the portraits
                HandleTags();
            }
            else
            {
                Debug.Log("Line skipped");
                SkipLine();
            }
        }
        else
        {
            //This part of the continue story method is reserved for finishing the story or waiting for choices to be answered

            if (_parent.playerUIComponent.ReturnTypingStatus()) //If this is the last line before the story can move on, we display it fully
            {
                if (displayLine != null) //We make sure to stop the coroutine so that we don't start another one and they overlap, it could cause bugs
                {
                    StopCoroutine(displayLine);
                    displayLine = null;
                }

                //We reset the dialogue to the current text
                SkipLine();
            }
            else
            {
                if (currentDialogue.currentChoices.Count <= 0) //If there are no choices to be answered (since that also makes the dialogue not able to continue) we finish the story
                {
                    _parent.playerUIComponent.HideConversationBox(); //Deactivating the dialogue ui

                    storyfinished = true;
                }
            }

        }
    }
    
    public void SkipLine()
    {
        //If we want to continue the story but the text is appearing currently we skip over to the next line
        if (displayLine != null) //We make sure to stop the coroutine so that we don't start another one and they overlap, it could cause bugs
        {
            StopCoroutine(displayLine);
            displayLine = null;
        }

        //We reset the dialogue to the current text
        _parent.dialogueText.text = currentDialogue.currentText;
        _parent.dialogueText.maxVisibleCharacters = currentDialogue.currentText.ToCharArray().Length;

        //Since we're no longer typing, we stop the bool in the player UI
        _parent.playerUIComponent.SetTypingStatus(false);

        //If there are any choices avaible, we display them
        DisplayChoices();
    }
    public void FinishStory() //This method is only performed when there is no more dialogue in the current story
    {
        //We set a timer so that the accept and jump input don't overlap
        _storyFinishedTimer -= Time.deltaTime;

        if (_storyFinishedTimer <= 0) //When it's done, we reset the timer and set the new state
        {
            _storyFinishedTimer = storyFinishedTimer;
            storyfinished = false;
            _parent.ChangeState(PlayerState.Idle);
        }
    }
    public void SetCurrentDialogue(Story dialogue) //Method to change the Current Dialogue
    {
        currentDialogue = dialogue;
    }

    #endregion

    #region Choices
    public void DisplayChoices() //Method to display the different choices
    {
        if(currentDialogue.currentChoices.Count > 0) //Execute only if there are choices to display
        {
            foreach(ChoiceClass c in dialogueChoices)
            {
                c.ReturnParent().SetActive(false);
            }

            for (int i = 0; i < currentDialogue.currentChoices.Count; i++) //Go through every choice avaible and fill out the variables
            {
                
                if (dialogueChoices[i] == null)
                    break;
                dialogueChoices[i].SetChoice(currentDialogue.currentChoices[i]);
            }

            StartCoroutine(choiceNavigation.SelectFirstOption(dialogueChoices[0].ReturnParent()));
        }
    }
    
    public void AssignChoices(int index) //Method to execute the coices when it appears
    {
        currentDialogue.ChooseChoiceIndex(index);

        //We use continue without the method so that it skips the response of the choice
        currentDialogue.Continue();

        ContinueStory();
        foreach (ChoiceClass c in dialogueChoices)
        {
            c.ReturnParent().SetActive(false);
        }
    }

    public void EnableChoiceGlow(int choice)
    {
        dialogueChoices[choice].EnableGlow();
    }

    public void DisableChoiceGlow(int choice)
    {
        dialogueChoices[choice].DisableGlow();
    }

    #endregion

    #region Speakers

    private void HandleTags() //This is a method to get the speakers in a dialogue and give them the reference to the UI
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
                            int numberValue = System.Convert.ToInt32(valueKey);

                            switch (numberValue)
                            {
                                case 0:
                                    _parent.playerUIComponent.SetDialogueLayout(playerSpeaker);
                                    break;

                                case 1:
                                    _parent.playerUIComponent.SetDialogueLayout(newSpeaker);
                                    break;

                                case 2:
                                    _parent.playerUIComponent.DeactivateDialogueLayout();
                                    break;

                                default:
                                    break;
                            }
                        break;

                    case "give_item":
                        ObjectClass obj = new ObjectClass();
                        obj = obj.CreateObject(valueKey);
                        _parent.playerInventoryComponent.AddObjectToKeyInventory(obj);
                        break;
                    case "take_item":
                        if (_parent.playerInventoryComponent.CheckIfObjectIsInInventory(valueKey))
                        {
                            _parent.playerInventoryComponent.UseItem(valueKey);
                        }
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
