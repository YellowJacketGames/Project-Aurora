using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UINavigation;
using TMPro;

//This script handles diary navigation and the content of the tabs
//This includes the mission diary, the collectables tab and the level map.
public class DiaryManager : MonoBehaviour
{
    [SerializeField] private NavigateTab diaryTabs; //Diary tabs class to handle navigating the different game tabs.
    private int index = 0; //index to check the current tab opened, since it will be handled through input events.

    [Space]
    [Header("Diary Variables")]
    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private GameObject questObjectivesPrefab;
    private List<QuestObjective> currentObjectives = new List<QuestObjective>();
    private List<TextMeshProUGUI> objectiveDescriptions = new List<TextMeshProUGUI>();
    [SerializeField] private GameObject questObjectivesParent;
    [SerializeField] private TextMeshProUGUI questDescription;

    [SerializeField] private Color activeObjectiveColor;
    [SerializeField] private Color completedObjectiveColor;

    void Start()
    {
        CloseDiaryTab();
    }

    #region Quest diary

    public void SetUpDiary()
    {
        Quest currentQuest = GameManager.instance.questManager.GetCurrentQuest(); //We store the current quest in a local variable to access it and make it more readable.

        //Set up main quest name
        questName.text = currentQuest.GetQuestName(); //We set the text to the quest name.

        //Set up objectives
        int currentObjectiveIndex = currentQuest.GetCurrentObjectivesIndex(); //First, we get the current index for the quest.

        for (int i = 0; i <= currentObjectiveIndex; i++) //We go through every objective with a for loop.
        {
            QuestObjective newObjective = currentQuest.GetObjectives()[i]; //We store the objective in a local variable to make it more readable.

            if (!currentObjectives.Contains(newObjective)) //If we already registered and spawned the objective, we just set up the text again just in case.
            {
                currentObjectives.Add(newObjective); //We register the new objective
                TextMeshProUGUI newDescription = Instantiate(questObjectivesPrefab, questObjectivesParent.transform).GetComponent<TextMeshProUGUI>(); //We create a new text prefab, which we store in the list
                newDescription.SetText(newObjective.GetObjective()); //We set up the objective text.
                newDescription.transform.SetAsFirstSibling(); //We set it as the first sibling so that if we already have more objective, it comes out on top.
                objectiveDescriptions.Add(newDescription); //We register it in the text list.
            }
            else
            { 
                objectiveDescriptions[i].SetText(newObjective.GetObjective()); //If we have already spawned the description, we set the text again just in case.
            }


            if (newObjective.IsObjectiveComplete()) //If the objective is already complete, we use the disable color
            {
                objectiveDescriptions[i].color = completedObjectiveColor; //it would be cool to use rich text with this and cross out the text.
            }
            else
            {
                objectiveDescriptions[i].color = activeObjectiveColor; //if not, we set the regular color.
            }
        }


        //Set up quest description
        questDescription.text = currentQuest.GetQuestObjective().GetDescription();

    }

    #endregion

    #region Tabs
    public void OpenDiaryTab() //Method to open the diary tab
    {
        GameManager.instance.ChangeGameState(GameStates.Diary); //We change the current game state to diary.
        diaryTabs.OpenInitialTab(); //We open the initial tab.
        index = 0; //We set the index to 0, as it resets everytime we open the menu.
        SetUpDiary(); //We set up the diary.
    }

    public void OpenNextTab() //This method changes to the next tab in the array
    {
        if (GameManager.instance.GetCurrentGameState() != GameStates.Diary)
            return;

        if (diaryTabs.GetTabsLength() <= 0) //If there are no tabs stored, we don't execute the method
            return;

        index++; //We increase the index by one

        if (index >= diaryTabs.GetTabsLength()) //If the index is higher than the array length, we reset it to 0 and the first element in the array.
        {
            index = 0;
        }

        diaryTabs.OpenNewTab(index);
    }

    public void OpenPreviousTab()
    {
        if (GameManager.instance.GetCurrentGameState() != GameStates.Diary)
            return;

        if (diaryTabs.GetTabsLength() <= 0)
            return;

        index--;

        if (index < 0)
        {
            index = diaryTabs.GetTabsLength() - 1;
        }

        diaryTabs.OpenNewTab(index);
    }

    public void CloseDiaryTab()
    {
        if (GameManager.instance.GetCurrentGameState() != GameStates.Diary)
            return;

        GameManager.instance.ChangeGameState(GameStates.Gameplay); //We change the current game state to gameplay.
        diaryTabs.CloseAllTabs();
        index = 0;
    }

    #endregion

}



