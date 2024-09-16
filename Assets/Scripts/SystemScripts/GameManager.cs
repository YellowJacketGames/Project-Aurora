using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

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
    [SerializeField] private bool shouldSave;
    [SerializeField] private string[] levelNames;
    public string[] LevelNames => levelNames;
    [SerializeField] private SavingData data;
    public SavingData Data => data;
    private string levelToLoad;
    private int levelIndex = 0;

    [SerializeField] VideoClip loadingScreenClip;
    [SerializeField] VideoClip basicLoadingScreenClip;
    
    
    




    private void Start()
    {
        shouldSave = true;
        if(!shouldSave) return;
        if (SavingManager.HasDataSaved())
            data = SavingManager.Load<SavingData>();
        else
            SavingManager.SaveNew(new SavingData());
    }
    
    
    
    
    public GameStates GetCurrentGameState()
    {
        return currentGameState;
    }

    public void ChangeGameState(GameStates newState)
    {
        currentGameState = newState;
    }

    public void SetLevelToLoad(string newLevel)
    {
        levelToLoad = newLevel;
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
        Debug.Log("Loading Next Level");
        questManager.DeactivateQuestUI();

        //Level settings after ending a level
        if (currentLevelManager != null)
        {
            currentLevelManager.EndLevelMusic();
            SetLoadingScreenClip(currentLevelManager.nextLevelClip);
            currentLevelManager.SetNextLevel();
        }

        //Set the level index and begin the transition
        levelIndex++;
        StartCoroutine(LoadLevel("LoadingScreen"));
    }

    public void GoToMainMenu()
    {
        //End the music
        questManager.DeactivateQuestUI();

        SetLoadingScreenClip(basicLoadingScreenClip);
        if (currentLevelManager != null)
            currentLevelManager.EndLevelMusic();

        SetLevelToLoad("MainMenu");
        //Set the level index and begin the transition
        levelIndex = 0;
        StartCoroutine(LoadLevel("LoadingScreen"));
    }
    public void GoToSpecificLevel()
    {
        StartCoroutine(LoadLevel());
    }
    public void ResetLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().name));
    }

    IEnumerator LoadLevel()
    {
        AsyncOperation load = new AsyncOperation();
        
        load = SceneManager.LoadSceneAsync(levelToLoad);
        load.allowSceneActivation = false;

        while (!load.isDone)
        {
            if (load.progress >= 0.9f)
            {

                load.allowSceneActivation = true;
            }
            Debug.Log("Loading");
            yield return null;
        }
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

    public void ClearTypewriterInventory()
    {
        typewriterInventoryStatic.Clear();
    }
    public int GetTypewriterCount()
    {
        return typewriterInventoryStatic.Count;
    }

    public bool CheckIfAlreadyHasTypewriter(ObjectClass o)
    {
        return typewriterInventoryStatic.Contains(o);
    }

    public string GetCurrentLevelName()
    {
        return levelToLoad;
    }
    
    public VideoClip GetLoadingScreenClip()
    {
        return loadingScreenClip;
    }

    public void SetLoadingScreenClip(VideoClip clip)
    {
        if(clip!=null)
            loadingScreenClip = clip;
    }


    public void IncrementProgression()
    {
        if(!shouldSave) return;
        Debug.LogWarning("LOADING NEXT LEVEL");
        data.IncrementProgression();
        print("data.progressionIndex  -> " + data.progressionIndex);
        SavingManager.SaveNew(data);
    }

}
