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
    [SerializeField] private string questObjective; //This will be displayed in the player's hud, it's what the player needs to do to complete the quest.
    [Space]
    [TextArea(15,20)] [SerializeField] private string questDescription; //This will be displayed in the diary when the quest is selected to explain the player the context of the quest.

    private bool isQuestCompleted; //Bool to check if the quest has been completed.

    public bool IsQuestCompleted() //Method to see if the quest has been completed.
    { 
        return isQuestCompleted;
    }

    public void CompleteQuest() //When the quest conditions are met, we complete it.
    {
        isQuestCompleted = true;
    }

    #region Get quest variables

    public string GetQuestName()
    {
        return questName;
    }
    
    public string GetQuestObjective()
    {
        return questObjective;
    }

    public string GetQuestDescription()
    {
        return questDescription;
    }
    #endregion
}
