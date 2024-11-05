using System;
using System.Collections;
using Cinemachine;
using Ink.Runtime;
using UnityEngine;

public class Level0Manager : LevelManager
{
    [SerializeField] private TextAsset initialDialogue;
    [SerializeField] private CinemachineVirtualCamera levelCamera;
    [SerializeField] private CinemachineVirtualCamera typewriteCamera;

    public override void Start()
    {
        EventsManager.AddConversationEvent("ZoomInCameraTimelapseToTypewriter");
        EventsManager.AddConversationEvent("ZoomOutCameraTimelapseToTypewriter");
        EventsManager.GetConversationEvent("ZoomInCameraTimelapseToTypewriter").AddListener(ZoomInCameraToTypewriter);
        EventsManager.GetConversationEvent("ZoomOutCameraTimelapseToTypewriter").AddListener(ZoomOutCameraToTypewriter);

        base.Start();
        LoadIntroDialogue();
    }

    private void OnDisable()
    {
        EventsManager.GetConversationEvent("ZoomInCameraTimelapseToTypewriter")
            .RemoveListener(ZoomInCameraToTypewriter);
        EventsManager.GetConversationEvent("ZoomOutCameraTimelapseToTypewriter")
            .RemoveListener(ZoomOutCameraToTypewriter);
    }

    private void ZoomInCameraToTypewriter()
    {
        typewriteCamera.Priority = 10;
        levelCamera.Priority = 0;
    }

    private void ZoomOutCameraToTypewriter()
    {
        typewriteCamera.Priority = 0;
        levelCamera.Priority = 10;
    }

    private void LoadIntroDialogue()
    {
        GameManager.instance.currentController.playerConversationComponent.SetCurrentDialogue(
            new Story(initialDialogue.text));
        GameManager.instance.currentController.ChangeState(PlayerState.Conversation);
    }
}