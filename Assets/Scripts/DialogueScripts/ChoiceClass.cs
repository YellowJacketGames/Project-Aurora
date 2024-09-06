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

    private Color32 hoverColor = new Color32(255, 255, 0, 255);
    private Color32 defaultColor = new Color32(255, 255, 225, 255);
    private Choice assignedChoice = new Choice();

    public void SetChoice(Choice newChoice) //Method to assign the current choice to the component and fill it's variables, and also activate the parent object
    {
        assignedChoice = newChoice;
        choiceText.SetText(assignedChoice.text);
        choiceParent.SetActive(true);
    }
    
    public void AssignListeners()
    {

    }
    public void EnableGlow()
    {
        // choiceText.fontSharedMaterial.SetFloat(ShaderUtilities.ID_GlowPower, 0.6f);
        choiceText.color = hoverColor;
    }

    public void DisableGlow()
    {
        // choiceText.fontSharedMaterial.SetFloat(ShaderUtilities.ID_GlowPower, 0f);
        choiceText.color = defaultColor;
    }
    
    public GameObject ReturnParent()
    {
        return choiceParent;
    }
}
