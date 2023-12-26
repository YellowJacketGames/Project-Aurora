using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Namespace for generic UI methods that we already use in multiple scripts.
namespace UINavigation
{
    public class NavigateOptions
    {
        public IEnumerator SelectFirstOption(GameObject firstOption) // Coroutine to set the first selectable option
        {
            //Event System requires to be cleared first and then assigned in a different frame
            EventSystem.current.SetSelectedGameObject(null);
            yield return new WaitForEndOfFrame();
            EventSystem.current.SetSelectedGameObject(firstOption);
        }
    }

}

