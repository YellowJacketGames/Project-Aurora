using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script will handle transitions from level to level and different areas of the game
public class TransitionManager : MonoBehaviour
{
    [Header("Transition Variables")]

    //Bools to check which function to do
    bool fadeIn;
    bool fadeOut;

    [SerializeField] private Image transitionImage;

    [Range(0f, 1f)]
    [SerializeField] float transitionDuration;

    [SerializeField] float transitionTime = 0;


    private void Start()
    {
        //Give reference to Game Manager
        GameManager.instance.currentTransitionManager = this;

        //We begin with a fade out transition
        SetFadeOut();
    }
    private void Update()
    {
        if (fadeIn) //If faden in has been set, execute the method 
        {
            FadeIn();
        }

        if (fadeOut) //If faden out in has been set, execute the method 
        {
            FadeOut();
        }
    }

    #region Transition Methods

    public void SetFadeIn() //Set the transition to fade In
    {
        GameManager.instance.currentController.ChangeState(PlayerState.Transition); //Since we're entering a transition, we set the player state to transition

        if (!fadeOut)
            fadeIn = true;
    }

    public void SetFadeOut() //Set the transition to fade out
    {
        if(!fadeIn)
        fadeOut = true;
    }

    private void FadeIn() //Method to change the transition image alpha to 1, which means to fade in the image
    {
        transitionTime += Time.deltaTime; //We store the time since the transition began

        if (transitionTime <= transitionDuration) //If the transition time is higher than the duration, it means the lerp has ended and the image has faded in
        {
            //We lerp the value of the image alpha from 0 to 1
            transitionImage.color = new Color(transitionImage.color.r, transitionImage.color.g, transitionImage.color.b, Mathf.Lerp(0, 1, transitionTime / transitionDuration)); 
        }
        else
        {
            transitionImage.color = new Color(transitionImage.color.r, transitionImage.color.g, transitionImage.color.b, 1); //When the lerp is done, we set the value correctly to avoid bugs

            //We reset the check and the time since transition began

            fadeIn = false; 
            transitionTime = 0;
        }
    }

    private void FadeOut() //Method to change the transition image alpha to 0, which means to fade out the image
    {
        transitionTime += Time.deltaTime; //We store the time since the transition began

        if (transitionTime <= transitionDuration) //If the transition time is higher than the duration, it means the lerp has ended and the image has faded out
        {
            //We lerp the value of the image alpha from 1 to 0
            transitionImage.color = new Color(transitionImage.color.r, transitionImage.color.g, transitionImage.color.b, Mathf.Lerp(1, 0, transitionTime / transitionDuration));
        }
        else
        {
            transitionImage.color = new Color(transitionImage.color.r, transitionImage.color.g, transitionImage.color.b, 0); //When the lerp is done, we set the value correctly to avoid bugs

            //We reset the check and the time since transition began

            fadeOut = false;
            transitionTime = 0;

            //Since we're exiting the transition, we also need to change to player state to idle
            GameManager.instance.currentController.ChangeState(PlayerState.Idle);

        }
    }

    public bool ReturnTransitionStatus() //This returns true if either transition is playing and false if none are
    {
        if(fadeIn || fadeOut)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion
}
