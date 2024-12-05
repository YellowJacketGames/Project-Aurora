using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using InteractableElements;
using UnityEngine;

public class PuzzleDoorElement : InteractableElement
{
    private bool hasFirstPaper;
    private bool hasSecondPaper;
    private bool hasThirdPaper;

    public bool SetFirstPaper
    {
        set { hasFirstPaper = value; }
    }

    public bool SetSecondPaper
    {
        set { hasSecondPaper = value; }
    }

    public bool SetThirdPaper
    {
        set { hasThirdPaper = value; }
    }


    private int playerLayer;
    [SerializeField] private CinemachineVirtualCamera puzzleCamera;
    [SerializeField] private CinemachineVirtualCamera levelCamera;
    [SerializeField] private Camera camera;
    private CodexManager _codexManager;
    private bool shouldEnter = false;

    protected override void Awake()
    {
        base.Awake();
        camera = Camera.main;
        playerLayer = LayerMask.NameToLayer("Player");
        _codexManager = GetComponentInChildren<CodexManager>();
    }

    private void OnEnable()
    {
        foreach (var codex in _codexManager.Codexes)
            if (codex.GetUnlocked())
                shouldEnter = true;

        ignorePopup = !shouldEnter;
    }

    public override void OnInteract()
    {
        if (ignorePopup) return;
        SetPuzzleCamera();
        ZoomToAvailable();
        ChangeInputScheme(true);
        HideInteractPrompt();
        ignorePopup = true;
    }

    private void ZoomToAvailable()
    {
        CodexGroup selectedCodex = null;
        foreach (var codex in _codexManager.Codexes)
            if (codex.GetUnlocked())
                selectedCodex = codex;

        if (selectedCodex)
            selectedCodex.Select(puzzleCamera);
        
    }

    public void ExitPuzzle()
    {
        ResetMainCamera();
        ChangeInputScheme(false);
        HideInteractPrompt();
        ignorePopup = false;
    }

    private void SetPuzzleCamera()
    {
        camera.cullingMask &= ~(1 << playerLayer);
        puzzleCamera.Priority = 5;
        levelCamera.Priority = 0;
    }

    private void ResetMainCamera()
    {
        Camera.main.cullingMask = -1;
        puzzleCamera.Priority = 0;
        levelCamera.Priority = 5;
    }

    private void ChangeInputScheme(bool puzzleMode)
    {
        if (puzzleMode)
            GameManager.instance.currentController.playerInputHandlerComponent.ChangeToPuzzleDoorControls();
        else
            GameManager.instance.currentController.playerInputHandlerComponent.ChangeToLevelControls();
    }
}