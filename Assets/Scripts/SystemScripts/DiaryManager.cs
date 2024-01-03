using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UINavigation;

//This script handles diary navigation and the content of the tabs
//This includes the mission diary, the collectables tab and the level map.
public class DiaryManager : MonoBehaviour
{
    [SerializeField] private NavigateTab diaryTabs; //Diary tabs class to handle navigating the different game tabs.
    private int index = 0; //index to check the current tab opened, since it will be handled through input events.

    public void OpenDiaryTab() //Method to open the diary tab
    {
        GameManager.instance.ChangeGameState(GameStates.Diary); //We change the current game state to diary.
        diaryTabs.OpenInitialTab(); //We open the initial tab
        index = 0; //We set the index to 0, as it resets everytime we open the menu
    }

    public void OpenNextTab() //This method changes to the next tab in the array
    {
        if (GameManager.instance.GetCurrentGameState() != GameStates.Diary)
            return; 

        if (diaryTabs.GetTabsLength() <= 0) //If there are no tabs stored, we don't execute the method
            return;

        index++; //We increase the index by one

        if(index >= diaryTabs.GetTabsLength()) //If the index is higher than the array length, we reset it to 0 and the first element in the array.
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
            index = diaryTabs.GetTabsLength()-1;
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

    void Start()
    {
        CloseDiaryTab();
    }
}
