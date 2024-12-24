using UnityEngine;

public class StartHorsesMinigame : LevelEvent
{
    [SerializeField] private HorseMinigame minigame;

    public override void OnEvent()
    {
        base.OnEvent();
        if (minigame == null) return;
        minigame.StartMinigameFromInvoker();
    }
}