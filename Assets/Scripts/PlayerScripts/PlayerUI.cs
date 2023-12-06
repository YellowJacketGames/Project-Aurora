using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//This script will handle most of the player HUD and UI
//Like contexual controls and UI animations
public class PlayerUI : PlayerComponent
{
    private Gamepad gamepad; //This variable is used to see if the player is using a gamepad
    private Keyboard keyboard; //This variable is used to see if the player is using a gamepad


    //Region to hold the show context control methods
    #region Context Controls
    private void ShowKeyboardControls() //This methods changes the current display controls to the keyboard
    {
        _parent.keyboardControls.SetActive(true);
        _parent.gamepadControls.SetActive(false);

    }

    private void ShowGamepadControls() //This methods changes the current display controls to the gamepad
    {
        _parent.gamepadControls.SetActive(true);
        _parent.keyboardControls.SetActive(false);
    }

    #endregion


    //This region holds the methods related to interact prompt
    #region Interact Prompt
    public void ShowInteractPrompt(InteractableElement element)
    {
        _parent.interactablePromptObject.gameObject.SetActive(true); //First we set the parent object of the interaction prompt active

        switch (element.GetElementType()) //We check which type of element is the current one to put the correct text
        {
            case InteractionType.PickUp:
                _parent.interactableText.SetText("Pick up " + element.GetElementName());
                break;
            case InteractionType.Location:
                _parent.interactableText.SetText("Go to " + element.GetElementName());
                break;
            case InteractionType.Conversation:
                _parent.interactableText.SetText("Talk to " + element.GetElementName());
                break;
            default:
                break;
        }

        //We deactivate both controls so that we don't keep a wrong control type active
        _parent.controllerInteractPrompt.SetActive(false);
        _parent.keyboardInteractPrompt.SetActive(false);

        //And we activate the correct one depending on the current control type

        switch (_parent.currentControl)
        {
            case ControlType.Gamepad:
                _parent.controllerInteractPrompt.SetActive(true);
                break;
            case ControlType.Keyboard:
                _parent.keyboardInteractPrompt.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void HideInteractPrompt()
    {
        _parent.interactablePromptObject.gameObject.SetActive(false); //We set the parent object of the interaction prompt to not active
    }

    #endregion

    private void Update()
    {
        if (_parent.seeControls)
        {
            switch (_parent.currentControl)
            {
                case ControlType.Gamepad:
                    ShowGamepadControls();
                    break;
                case ControlType.Keyboard:
                    ShowKeyboardControls();
                    break;
                default:
                    break;
            }
        }
    }


}
