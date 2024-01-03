using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Namespace for generic UI methods that we already use in multiple scripts.
namespace UINavigation
{
    public class NavigateOptions //This class selects options for the different menus.
    {
        public IEnumerator SelectFirstOption(GameObject firstOption) // Coroutine to set the first selectable option.
        {
            //Event System requires to be cleared first and then assigned in a different frame.
            EventSystem.current.SetSelectedGameObject(null);
            yield return new WaitForEndOfFrame();
            EventSystem.current.SetSelectedGameObject(firstOption);
        }
    }

    [System.Serializable]
    public class NavigateTab //This class manages methods to handle tab changes and it's navigation.
    {
        [SerializeField] private GameObject initialTab; //Variable to store the first tab and the one that initiates it all.
        private GameObject currentTab; //Variable to store the tab that is currently active.
        [SerializeField] private GameObject[] newTab;   //Array variable to store all the tabs that are stored in the current menu.

        public void OpenNewTab(int tabToOpen) //This methods opens a tab from the tab array with an int as a parameter and closes the other tabs.
        {
            if (currentTab == newTab[tabToOpen]) //if the tab we're trying to open is already active, we don't execute the method.
                return;

            if(newTab.Length == 0) //Also, if there are no new tabs stored in the class, we also don't execute the method.
            {
                Debug.Log("There are not tabs stored in this class");
                return;
            }
            CloseAllTabs(); //We close all tabs.
            newTab[tabToOpen].SetActive(true); //We open the new one.
            currentTab = newTab[tabToOpen]; //We set the current tab to the one we just opened.
        }

        public void OpenInitialTab() //This script opens the original tab and closes the rest
        {
            if (currentTab == initialTab) //if the tab we're trying to open is already active, we don't execute the method.
                return;

            CloseAllTabs(); //We close all tabs.
            initialTab.SetActive(true); //We open the initial tab.
            currentTab = initialTab; //We set the current tab to the one we just opened.
        }

        public void CloseAllTabs() //This method deactivates all tabs
        {
            if(newTab.Length > 0) //If new tab doesn't have anything in it, we don't go through the loop.
            {
                for (int i = 0; i < newTab.Length; i++)
                {
                    newTab[i].SetActive(false);
                }
            }
            initialTab.SetActive(false); //We also deactivate the original tab.

            currentTab = null; //We set the current tab to null
        }

        //This region holds the methods to return all variables.
        #region Return Tabs
        public GameObject GetCurrentTab()
        {
            return currentTab;
        }
        public GameObject GetInitialTab()
        {
            return initialTab;
        }
        public GameObject GetNewTab(int tabToGet)
        {
            return newTab[tabToGet];
        }

        public int GetTabsLength()
        {
            return newTab.Length;
        }
        #endregion
    }

}

