using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PuzzleDoorElement : InteractableElement
{
    private bool hasFirstPaper;
    private bool hasSecondPaper;
    private bool hasThirdPaper;
    
    public bool SetFirstPaper { set { hasFirstPaper = value; } } 
    public bool SetSecondPaper { set { hasSecondPaper = value; } } 
    public bool SetThirdPaper { set { hasThirdPaper = value; } }


    private int playerLayer;
    [SerializeField] private CinemachineVirtualCamera puzzleCamera; 
    [SerializeField] private CinemachineVirtualCamera levelCamera; 
    [SerializeField] private Camera camera;

    protected override void Awake()
    {
        base.Awake();
        camera = Camera.main;
        playerLayer = LayerMask.NameToLayer("Player");
    }
    
    public override void OnInteract()
    {
        SetPuzzleCamera();
        ChangeInputScheme(true);
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
        if(puzzleMode)
            GameManager.instance.currentController.playerInputHandlerComponent.ChangeToPuzzleDoorControls();
        else
            GameManager.instance.currentController.playerInputHandlerComponent.ChangeToLevelControls();

    }
}
