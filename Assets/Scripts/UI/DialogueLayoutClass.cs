using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ImageEffects;
public class DialogueLayoutClass : MonoBehaviour //This class is used to create a variable for the different layouts in the dialogue box
{
    private Speaker currentSpeaker; //Variable to store the current speaker, not necessary at the moment but could be useful in the future
    private Effects portraitEffects; //Effects to do stuff with the portrait

    //Dialogue layout components
    [Header("Dialogue Layout Components")]
    [SerializeField] private TextMeshProUGUI speakerName; 
    [SerializeField] private Image speakerPortrait;
    [SerializeField] private GameObject parent;
    [Space]
    [Header("Dialogue Layout Variables")]
    [SerializeField] private float portraitTime = 0.3f; //Variable to change how much time the portrait takes to fade in and out

    bool fillingLayout;
    bool deactivatingLayout;

    private void Start()
    {
        portraitEffects = new Effects();
    }
    public void FillLayout(Speaker s) //this method fills the references with the data from the scriptable objects
    {
        if(currentSpeaker != s) //We fill the components with the speaker data
        {
            currentSpeaker = s;
            speakerPortrait.sprite = currentSpeaker.speakerPortrait;
        }


        speakerName.SetText(currentSpeaker.speakerName);

        parent.SetActive(true); //We activate the parent
        fillingLayout = true; //We begin the portrait transition with the bool trigger
        Debug.Log("Filling layout for: " + speakerName);
    }

    public void DeactivateLayout()
    {
        deactivatingLayout = true; //we activate the bool trigger to deactivate the dialogue layout
    }

    public void Update()
    {
        if (fillingLayout) //if the portrait is being filled, we make it fade in
        {
            if(portraitEffects.FadeIn(speakerPortrait, portraitTime)) //When it's done, we deactivate the trigger
            {
                Debug.Log("Done filling layout for: " + speakerName);

                fillingLayout = false;
            }
        }

        if (deactivatingLayout) //If the portrait is being deactivated, we make it fade out
        {
            if (portraitEffects.FadeOut(speakerPortrait, portraitTime)) //When it's done, we deactivate the trigger
            {
                deactivatingLayout = false;
                parent.SetActive(false); //since we're deactivating the portrait, we also have to deactivate the layout parent
            }
        }
    }

}
