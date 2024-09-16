[System.Serializable]
public class SavingData
{
    public int progressionIndex;

    // private string[] keysIds;
    // private string[] framesIds;
    
    //game settings configuration - res, vol, quality.. 

    
    public SavingData()
    {
        ResetData();
    }

    public SavingData(int progressionIndex)
    {
        this.progressionIndex = progressionIndex;
    }
    
    public bool HasSavedData() => ( progressionIndex != 0);
    private void ResetData()
    {
        progressionIndex = 0; 
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