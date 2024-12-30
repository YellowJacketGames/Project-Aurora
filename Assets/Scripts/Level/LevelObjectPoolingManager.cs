using System;

public class LevelObjectPoolingManager : ObjectPooling
{
    public FallingHatsManager FallingHatsManagerRef;

    public void Start()
    {
        GameManager.instance.currentLevelObjectPoolingManager = this;
    }
}    
