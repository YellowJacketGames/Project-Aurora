using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ImageEffects;
//This script will handle the different quests of the player.
public class QuestManager : MonoBehaviour
{
    [Header("Quests Variables")]
    [SerializeField] private Quest currentQuest; //Current quest that will be displayed in the UI when the 

    [Header("Quest UI")]
    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private TextMeshProUGUI questObjective;


    private void Awake()
    {
        if(GameManager.instance.questManager==null)
        GameManager.instance.questManager = this;
    }
    private void Start() //On start, we want to set the main objective for the quest
    { 
        SetCurrentQuest();
    }


    #region Get variables 
    public Quest GetCurrentQuest()
    {
        return currentQuest;
    }

    #endregion

    #region Quest methods
    public void SetCurrentQuest() //Method to set the new current quest
    {
        //Set the quest
        if(GameManager.instance.currentLevelManager != null)
        {
            currentQuest = GameManager.instance.currentLevelManager.levelQuest;
        }

        //UI implementation
        if (currentQuest != null)
        {
            questName.text = currentQuest.GetQuestName();
            questObjective.text = currentQuest.GetQuestObjective().GetObjective();
        }
    }

    public void UpdateObjective() //Method to update the quest when the player completes an objective 
    {
        currentQuest.GetQuestObjective().CompleteObjective(); //We set the objective to complete.
        currentQuest.GoToNextObjective(); //We update the objective index
        SetCurrentQuest(); //We set the new variables
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            UpdateObjective();
        }
    }

    public bool HasQuest(int questIndex)
    {
        bool value = currentQuest.GetCurrentObjectivesIndex() == questIndex;
        return value;
    }


    #endregion
}

[System.Serializable]
public class QuestObjective //Simple class to store the quest objective and a simple description to display in the diary UI
{
    [TextArea(10, 15)][SerializeField] private string objective;
    [TextArea(10, 15)][SerializeField] private string description;
    private bool hasBeenCompleted;

    public void CompleteObjective()
    {
        hasBeenCompleted = true;
    }

    public void ResetObjective()
    {
        hasBeenCompleted = false;
    }
    public bool IsObjectiveComplete()
    {
        return hasBeenCompleted;
    }

    #region Get variables

    public string GetObjective()
    {
        return objective;
    }   
    public string GetDescription()
    {
        return description;
    }

    #endregion
}
