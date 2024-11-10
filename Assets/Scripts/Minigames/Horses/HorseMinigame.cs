using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class HorseMinigame : InteractableElement
{
    private int playerLayer;

    [FormerlySerializedAs("puzzleCamera")] [SerializeField]
    private CinemachineVirtualCamera minigameCamera;

    [SerializeField] private CinemachineVirtualCamera levelCamera;
    [SerializeField] private Camera camera;

    public  Horse selectedHorse;
    [SerializeField] private List<Horse> _horses;

    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private bool winner;
    private bool aHorseFinished = false;
    private void OnEnable()
    {
        if (selectedHorse)
            EventsManager.onMinigamePress.AddListener(selectedHorse.IncreaseTicks);
        EventsManager.onMinigameExit.AddListener(ExitMinigame);
    }

    private void OnDisable()
    {
        if (selectedHorse)
            EventsManager.OnCodexDown.RemoveListener(selectedHorse.IncreaseTicks);
        EventsManager.onMinigameExit.RemoveListener(ExitMinigame);

    }

    public void CheckIfWinner(Horse horse)
    {
        if(aHorseFinished) return;
        
        if (horse == selectedHorse)
            winner = true;
        else
            winner = false;
        aHorseFinished = true;
        foreach (var h in _horses)
        {
            h.StopRunning();
        }
        countdownText.text = winner  ? "Has ganado!" : "Has perdido";
    }
    protected override void Awake()
    {
        foreach (var horse in _horses)
            horse.InjectParentRef(this);
        
        base.Awake();
        camera = Camera.main;
        playerLayer = LayerMask.NameToLayer("Player");
        
    }

    public override void OnInteract()
    {
        SetMinigameCamera();
        ChangeInputScheme(true);
        HideInteractPrompt();
        ignorePopup = true;
        StartCoroutine(StartMinigameCorr());

    }

    private IEnumerator StartMinigameCorr()
    {
        yield return new WaitForSeconds(2f);

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        countdownText.text = "";

        StartMinigame();
        yield return null;
    }
    
    private void StartMinigame()
    {
        foreach (var horse in _horses)
        {
            horse.StartRace();
        }
    }
    
    public void ExitMinigame()
    {
        ResetMainCamera();
        ChangeInputScheme(false);
        HideInteractPrompt();
        ignorePopup = false;
        countdownText.text = "";
        foreach (var horse in _horses)
        {
            horse.Reset();
        }
        winner = false;
        aHorseFinished = false;
    }

    private void SetMinigameCamera()
    {
        camera.cullingMask &= ~(1 << playerLayer);
        minigameCamera.Priority = 5;
        levelCamera.Priority = 0;
    }

    private void ResetMainCamera()
    {
        Camera.main.cullingMask = -1;
        minigameCamera.Priority = 0;
        levelCamera.Priority = 5;
    }

    private void ChangeInputScheme(bool minigameMode)
    {
        if (minigameMode)
            GameManager.instance.currentController.playerInputHandlerComponent.ChangeToMinigamesControls();
        else
            GameManager.instance.currentController.playerInputHandlerComponent.ChangeToLevelControls();
    }
}