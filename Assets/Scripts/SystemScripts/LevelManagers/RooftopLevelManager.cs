using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class RooftopLevelManager : LevelManager
{

    [Header("Rooftop Level Components")]

    [Header("Timer")]    
    [Tooltip("The list goes from fastest to slowest")][SerializeField] private float[] rankedTimes;
    [SerializeField] private TextMeshProUGUI[] rankedTimesComponents;
    [SerializeField] GameObject[] typeWriterObjects;

    [SerializeField] private TextMeshProUGUI timeText;

    bool raceActive;
    private float currentTime;

    [SerializeField] TextAsset initialDialogue;

    public override void Start()
    {
        base.Start();

        Invoke("BeginConversation", 0.3f);
    }

    public void BeginConversation()
    {
        GameManager.instance.currentController.playerConversationComponent.SetCurrentDialogue(new Story(initialDialogue.text));
        GameManager.instance.currentController.ChangeState(PlayerState.Conversation);
    }
    // Update is called once per frame
    void Update()
    {
        if (!playtestingMoveScene)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                GameManager.instance.currentTransitionManager.NextLevel();
                playtestingMoveScene = true;
            }
        }

        if (raceActive)
        {
            currentTime += Time.deltaTime;
            UpdateTimeText(currentTime.ToString("F2"));
        }

        if (Input.GetKeyDown(KeyCode.L))
            FinishRace();
    }

    public void SetUpRace()
    {
        BeginRace();
        SetBestTimes();
    }
    public void BeginRace()
    {
        timeText.gameObject.SetActive(true);
        raceActive = true;
        currentTime = 0;
    }

    public void UpdateTimeText(string newText)
    {
        if(timeText!=null)
        timeText.SetText(newText);
    }

    public void SetBestTimes()
    {
        for (int i = 0; i < rankedTimes.Length; i++)
        {
            rankedTimesComponents[i].text +=""+rankedTimes[i].ToString() + " s";
        }
    }

    public void FinishRace()
    {
        //Deactivate timer
        raceActive = false;

        //Check timer 
        for (int i = 0; i < rankedTimes.Length; i++)
        {
            if(currentTime <= rankedTimes[i])
            {
                for (int e = typeWriterObjects.Length-1; e <= i; e--)
                {
                    typeWriterObjects[e].SetActive(true);
                }

                break;
            }
        }
    }

    public void SpawnTypeWriters()
    {

    }
}
