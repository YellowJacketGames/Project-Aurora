using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;

//Class that doesn't inherit from mono behaviour to hold variables in the player conversation script
[System.Serializable]
public class ChoiceClass
{
    [Header("Choise Variables")]
    [SerializeField] private TextMeshProUGUI choiceText; //Variable to hold the text component in the choice
    [SerializeField] private GameObject choiceParent;    //Variable to hold the parent of the choice button
    [SerializeField] private Button choiceButton;    //Variable to hold the parent of the choice button

    private Choice assignedChoice = new Choice();

    public void SetChoice(Choice newChoice) //Method to assign the current choice to the component and fill it's variables, and also activate the parent object
    {
        assignedChoice = newChoice;
        choiceText.SetText(assignedChoice.text);
        choiceParent.SetActive(true);
    }

    public GameObject ReturnParent()
    {
        return choiceParent;
    }
}
