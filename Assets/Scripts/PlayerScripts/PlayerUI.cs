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

    [SerializeField] PlayerInput test; 
    public void ShowKeyboardControls() //This methods changes the current display controls to the keyboard
    {
        _parent.keyboardControls.SetActive(true);
        _parent.gamepadControls.SetActive(false);

    }

    private void Start()
    {

    }
    public void ShowGamepadControls() //This methods changes the current display controls to the gamepad
    {
        _parent.gamepadControls.SetActive(true);
        _parent.keyboardControls.SetActive(false);
    }

    private void Update()
    {
        gamepad = Gamepad.current;
        keyboard = Keyboard.current;

        if (gamepad != null)
        {
            ShowGamepadControls();
            return;
            Debug.Log("Test");
        }

        if(keyboard != null)
        {
            ShowKeyboardControls();
            return;
        }
    }
}
