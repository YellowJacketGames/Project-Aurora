using UnityEngine;

public class StartBallsMinigame : LevelEvent
{
    [SerializeField] private BallMinigame minigame;
    public override void OnEvent()
    {
        base.OnEvent();
        if (minigame == null) return;
        minigame.StartMinigameFromInvoker();
    }
}