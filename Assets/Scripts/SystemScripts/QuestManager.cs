using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script will handle the different quests of the player.
public class QuestManager : MonoBehaviour
{
    [Header("Quests Variables")]
    [SerializeField] private List<Quest> allQuests; //List to store all quests in the game
    [SerializeField] private Quest currentQuest; //Current quest that will be displayed in the UI when the 

    public void AddQuest(Quest newQuest) //Method to add new quests to the list
    {
        if (!allQuests.Contains(newQuest))
            allQuests.Add(newQuest);
    }

    public void SetCurrentQuest(Quest newQuest) //Method to set the new current quest
    {
        if (newQuest == currentQuest)
            return;

        currentQuest = newQuest;
    }
}
