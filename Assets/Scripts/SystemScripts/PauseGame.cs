using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Script to handle pausing and unpausing the game.
//We'll try to do it with Time.timeScale but i don't know if this will cause trouble with the physics and the input.
//Check in the future for better implementation.
public class PauseGame : MonoBehaviour
{
    [Header("Pause Components")]
    private float originalTimeScale;
    [SerializeField] private GameObject pauseCanvas;

    private void Start()
    {
        //Set the game manager variable
        GameManager.instance.pauseManager = this;

        InitialSettings();
    }


    //Region that holds all methods related to the intiial settings of the pause manager.
    #region Initial Settings

    //Method to store the original timescale value, execute it on start
    public void SaveOriginalTimeScale()
    {
        originalTimeScale = Time.timeScale;
    }

    public void InitialSettings() //This is a method for the initial settings of the pause manager
    {
        SaveOriginalTimeScale(); //We save the original time scale to use later.
        pauseCanvas.gameObject.SetActive(false); //We deactivate the canvas since the game will begin in the gameplay state.
    }

    #endregion
    

    //Region that holds all methods related to pausing and unpausing the game.
    #region Pause and unpause
    public void Pause() //Method to pause the game
    {
        
        Time.timeScale = 0; //We set the timescale to 0, this will stop everything in the game
        GameManager.instance.ChangeGameState(GameStates.Pause); //We set the current game state to pause.

        GameManager.instance.currentCameraManager.StopCameraTimer(); //We stop the timer so that it doesn't turn the camera on accident while we're paused.
        //Add additional relevant UI methods
        pauseCanvas.gameObject.SetActive(true); //For now, we will just activate a canvas that gives feedback of the pause menu
                                                //In the future we will add a proper pause menu with different options
    }

    public void UnPause()
    {
        Time.timeScale = originalTimeScale; //We set the timescale to it's original value, this will resume everything in the game
        GameManager.instance.ChangeGameState(GameStates.Gameplay); //We set the game state back to gameplay.

        //Add additional relevant UI methods
        pauseCanvas.gameObject.SetActive(false); //When we return to the gameplay, we deactivate the placeholder canvas.
    }

    #endregion


}
