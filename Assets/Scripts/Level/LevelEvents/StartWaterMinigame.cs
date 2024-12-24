using UnityEngine;

public class StartWaterMinigame : LevelEvent
{
    [SerializeField] private WaterMinigame minigame;

    public override void OnEvent()
    {
        base.OnEvent();
        if (minigame == null) return;
        minigame.StartMinigameFromInvoker();
    }
}