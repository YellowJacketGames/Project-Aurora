using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//We will use a static class for the GameManager as it's usually a singleton
//It will hold static variables that are easily accessable in each scene
public class GameManager : MonoBehaviour
{
    //Singleton static variable
    public static GameManager instance;


    //This is the code necessary to create a singleton
    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    #endregion

    [Header("GameManager Components")]
    public CameraManager currentCameraManager;
    public PlayerController currentController;
    public TagManager speakerManager; //This is a placeholder, it should be another scriptable object with a list of all the speakers in game so that it doesn't depend on references.
    public TransitionManager currentTransitionManager;
    public PauseGame pauseManager;
    public DiaryManager diaryManager;
    public QuestManager questManager;
    public LevelManager currentLevelManager;
    private GameStates currentGameState;

    //Player Inventory save
    [SerializeField]  private List<ObjectClass> typewriterInventoryStatic = new List<ObjectClass>();

    //Level variables
    [SerializeField] private string[] levelNames;
    private int levelIndex = 0;
    public GameStates GetCurrentGameState()
    {
        return currentGameState;
    }

    public void ChangeGameState(GameStates newState)
    {
        currentGameState = newState;
    }


    public bool CanPlay()
    {
        if(GetCurrentGameState() == GameStates.Gameplay)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void GoToNextLevel()
    {
        //End the music
        if(currentLevelManager != null)
        currentLevelManager.EndLevelMusic();

        //Set the level index and begin the transition
        levelIndex++;
        StartCoroutine(LoadLevel());
    }

    public void GoToMainMenu()
    {
        //End the music
        if (currentLevelManager != null)
            currentLevelManager.EndLevelMusic();

        //Set the level index and begin the transition
        levelIndex = 0;
        StartCoroutine(LoadLevel("MainMenu"));
    }
    public void GoToSpecificLevel(int index)
    {
        levelIndex = index;
        StartCoroutine(LoadLevel());
    }
    public void ResetLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().name));
    }

    IEnumerator LoadLevel()
    {
        AsyncOperation load = new AsyncOperation();
        
        load = SceneManager.LoadSceneAsync(levelNames[levelIndex]);
        load.allowSceneActivation = false;

        while(load.progress < 0.9f)
        {
            yield return null;
        }

        load.allowSceneActivation = true;
    }
    IEnumerator LoadLevel(string name)
    {
        AsyncOperation load = new AsyncOperation();
        load = SceneManager.LoadSceneAsync(name);
        load.allowSceneActivation = false;

        while (load.progress < 0.9f)
        {
            yield return null;
        }

        load.allowSceneActivation = true;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void AddTyperwriterKey(ObjectClass o)
    {
        typewriterInventoryStatic.Add(o);
    }

    public int GetTypewriterCount()
    {
        return typewriterInventoryStatic.Count;
    }

    public bool CheckIfAlreadyHasTypewriter(ObjectClass o)
    {
        return typewriterInventoryStatic.Contains(o);
    }
}
