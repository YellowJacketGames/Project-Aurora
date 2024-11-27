using System;
using System.Collections.Generic;
using Cinemachine;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager3D : MonoBehaviour
{
    [SerializeField] private List<GameObject> selectableObjects;
    [SerializeField] private MainMenuElement3D selectedObject;

    [SerializeField] private CinemachineVirtualCamera defaultCamera;
    [SerializeField] private CinemachineVirtualCamera playCamera;
    [SerializeField] private CinemachineVirtualCamera newgameCamera;


    private string targetLayer = "MainMenuElement";
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        GameManager.instance.currentTransitionManager.SetFadeOut();
    }

    public void ZoomOutCamToDefault()
    {
        if (!defaultCamera || !newgameCamera) return;
        newgameCamera.Priority = 0;
        playCamera.Priority = 0;
        defaultCamera.Priority = 5;
    }

    public void ZoomInCamToPlay()
    {
        if (!defaultCamera || !newgameCamera) return;
        defaultCamera.Priority = 0;
        newgameCamera.Priority = 0;
        playCamera.Priority = 5;
    }

    public void LoadGame()
    {
        if (GameManager.instance.Data.HasSavedData())
            Continue();
        NewGame();
    }

    public void Exit()
    {
        GameManager.instance.currentTransitionManager.QuitGame();
    }

    private void NewGame()
    {
        GameManager.instance.ClearTypewriterInventory();
        GameManager.instance.ResetData();
        GameManager.instance.currentTransitionManager.SpecificLevel("Cutscene");
    }

    private void Continue()
    {
            var index = GameManager.instance.Data.progressionIndex;
            GameManager.instance.SetLevelToLoad(GameManager.instance.LevelNames[index]);
            GameManager.instance.currentTransitionManager.SetLoadingClip();
            GameManager.instance.currentTransitionManager.NextLevel();
    }

    private void Update()
    {
        Hover();
        if (Input.GetMouseButtonDown(0))
            Click();
    }

    private void Hover()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        var layerMask = LayerMask.GetMask(targetLayer);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            selectedObject = hit.collider.gameObject.GetComponent<MainMenuElement3D>();
            selectedObject.PerformHoverAction();
        }
        else
        {
            if (!selectedObject) return;
            selectedObject.PerformResetHoverAction();
            selectedObject = null;
        }
    }

    private void Click()
    {
        if (!selectedObject) return;
        selectedObject.PerformClick();
    }
}