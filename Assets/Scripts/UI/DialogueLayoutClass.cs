using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class DialogueLayoutClass //This class is used to create a variable for the different layouts in the dialogue box
{
    private Speaker currentSpeaker;
    [SerializeField] private TextMeshProUGUI speakerName;
    [SerializeField] private Image speakerPortrait;
    [SerializeField] private GameObject parent;
    public void FillLayout(Speaker s) //this method fills the references with the data from the scriptable objects
    {
        if(currentSpeaker != s)
        {
            currentSpeaker = s;
            speakerName.SetText(currentSpeaker.speakerName);
            speakerPortrait.sprite = currentSpeaker.speakerPortrait;
        }

        parent.SetActive(true);
    }

    public void DeactivateLayout()
    {
        if (parent == null) //Security check to not crash the game if the parent is null
            return;

        parent.SetActive(false);
    }
 
}
