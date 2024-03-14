using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UINavigation;
using UnityEngine.SceneManagement;

//Script to handle the main menu options and functions
public class MainMenuManager : MonoBehaviour
{
    [Header("Main Menu Components")]
    [SerializeField] private GameObject firstOption;//The first option to select when the main menu is selected
    private NavigateOptions mainMenuNavigation = new NavigateOptions();//Navigation component 
    [SerializeField] private float loadingGameTimer;//Time to let the transition play before loading the scene
    private float _loadingGameTimer;//Variable that functions as the actual timer
    bool loading;//Check for the loading 
    bool exiting; //Check for the exiting
    bool optionsDisabled;//Check to not allow buttons to be pressed 
    [SerializeField] private string sceneToLoad;//Variable to store the name of the scene we want to load

    [Space]
    [Header("Button Components")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button collectablesButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        StartCoroutine(mainMenuNavigation.SelectFirstOption(firstOption));
        GameManager.instance.currentTransitionManager.SetFadeOut();
    }

    private void Update()
    {
        if (loading) 
        {
            _loadingGameTimer -= Time.deltaTime;

            if(_loadingGameTimer <= 0)
            {
                _loadingGameTimer = loadingGameTimer;
                SceneManager.LoadScene(sceneToLoad);
            }
        }

        if(exiting)
        {
            _loadingGameTimer -= Time.deltaTime;

            if (_loadingGameTimer <= 0)
            {
                _loadingGameTimer = loadingGameTimer;
                Application.Quit();
            }
        }
    }

    private void OnEnable()
    {
        newGameButton.onClick.AddListener(OnNewGame);
        continueButton.onClick.AddListener(OnContinue);
        collectablesButton.onClick.AddListener(OnCollectables);
        optionsButton.onClick.AddListener(OnOption);
        exitButton.onClick.AddListener(OnExit);
    }

    private void OnDisable()
    {
        newGameButton.onClick.RemoveListener(OnNewGame);
        continueButton.onClick.RemoveListener(OnContinue);
        collectablesButton.onClick.RemoveListener(OnCollectables);
        optionsButton.onClick.RemoveListener(OnOption);
        exitButton.onClick.RemoveListener(OnExit);
    }

    #region Button Methods
    public void OnNewGame() //Method to execute when the New Game button is pressed 
    {
        if (optionsDisabled) //If the options have been disabled, it will not execute any code.
            return;
        optionsDisabled = true; //We disable the other options

        GameManager.instance.currentTransitionManager.SpecificLevel("Cutscene");
    }

    public void OnContinue()
    {
        if (optionsDisabled) //If the options have been disabled, it will not execute any code.
            return;
    }

    public void OnOption()
    {
        if (optionsDisabled) //If the options have been disabled, it will not execute any code.
            return;
    }

    public void OnCollectables()
    {
        if (optionsDisabled) //If the options have been disabled, it will not execute any code.
            return;
    }

    public void OnExit()
    {
        if (optionsDisabled) //If the options have been disabled, it will not execute any code.
            return;
        exiting = true;
        optionsDisabled = true;
        GameManager.instance.currentTransitionManager.QuitGame();
    }

    #endregion
}
