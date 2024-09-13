[System.Serializable]
public class SavingData
{
    private int progressionIndex;
    private string timePlayed;

    // private string[] keysIds;
    // private string[] framesIds;
    
    //game settings configuration - res, vol, quality.. 

    
    public SavingData()
    {
        ResetData();
    }

    public SavingData(int progressionIndex, string timePlayed)
    {
        this.progressionIndex = progressionIndex;
        this.timePlayed = timePlayed;
    }
    
    public bool HasSavedData() => (timePlayed != "00:00:00" || progressionIndex != 0);
    private void ResetData()
    {
        progressionIndex = 0; 
        timePlayed = "00:00:00";
    }

    public void IncrementProgression()
    {
        progressionIndex++; 
    }
    
    /*
      [SerializeField] private string[] levelNames; 
      in game manager, change it to only hold all the playable lvls / scenes for saving purposes
      and use progressionIndex for loading in that array
    
    */
}