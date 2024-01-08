using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a scriptable object class that stores the data of the player quests.
//We will use simple data to be able to save quests and create them in a easier way.

//Dialogue implementation:
//To add quests from dialogue, we will create a method that will get the name of the quest from
//the resources folder and add it to the current quest list.
[CreateAssetMenu(fileName = "Quest", menuName = "ScriptableObjects/Create new quest", order = 1)]
public class Quest : ScriptableObject
{
    [Header("Quest variables")]
    [Space]
    [SerializeField] private string questName; //This will be displayed in the diary.
    [SerializeField] private QuestObjective[] questObjectives;

    private int currentObjectiveIndex = 0; //index to check which is the current active objective

    private bool isQuestCompleted; //Bool to check if the quest has been completed.

    private void OnEnable()
    {
        currentObjectiveIndex = 0; //We reset the current objective of the quest
        isQuestCompleted = false; //We reset the complete check

        for (int i = 0; i < questObjectives.Length; i++) //We reset the objective completion, this is just a placeholder solution that will be later be addressed with a save system.
        {
            questObjectives[i].ResetObjective();
        }
    }

    #region Quest Completion
    public bool IsQuestCompleted() //Method to see if the quest has been completed.
    { 
        return isQuestCompleted;
    }

    public void CompleteQuest() //When the quest conditions are met, we complete it.
    {
        isQuestCompleted = true;
    }

    //This method updates the objective index, if there are no more objectives in the quests,
    //it completes the quest and returns true, if not, it updates the quest index and returns false.
    public bool GoToNextObjective() 
    {
        if (currentObjectiveIndex + 1 >= questObjectives.Length) //We check if the index is higher than the objectives count. 
        {
            CompleteQuest(); //We complete the quest.
            return true; //We return true.
        }
        else
        {
            currentObjectiveIndex++; //We increase the objective index.
            return false; //We return false.
        }
    }

    #endregion

    #region Get quest variables

    public string GetQuestName()
    {
        return questName;
    }
    
    public QuestObjective GetQuestObjective()
    {
        return questObjectives[currentObjectiveIndex];
    }

    public int GetCurrentObjectivesIndex()
    {
        return currentObjectiveIndex;
    }

    public QuestObjective[] GetObjectives()
    {
        return questObjectives;
    }
    #endregion
}
