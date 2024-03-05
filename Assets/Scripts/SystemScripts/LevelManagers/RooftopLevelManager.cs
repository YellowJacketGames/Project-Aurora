using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RooftopLevelManager : LevelManager
{

    [Header("Rooftop Level Components")]

    [Header("Timer")]    
    [Tooltip("The list goes from fastest to slowest")][SerializeField] private float[] rankedTimes;
    [SerializeField] private TextMeshProUGUI[] rankedTimesComponents;

    [SerializeField] private TextMeshProUGUI timeText;

    bool raceActive;
    private float currentTime;
    void Start()
    {
        BeginRace();
        SetBestTimes();
    }

    // Update is called once per frame
    void Update()
    {
        if (raceActive)
        {
            currentTime += Time.deltaTime;
            UpdateTimeText(currentTime.ToString("F2"));
        }

        if (Input.GetKeyDown(KeyCode.L))
            FinishRace();
    }

    public void BeginRace()
    {
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
                rankedTimesComponents[i].color = Color.red;
                //Give number of keys

                break;
            }
        }
    }
}
