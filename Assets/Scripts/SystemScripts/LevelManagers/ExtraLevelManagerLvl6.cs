using Cinemachine;
using UnityEngine;

public class ExtraLevelManagerLvl6 : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera levelCamera;
    [SerializeField] private CinemachineVirtualCamera zoltarCamera;

    public void Start()
    {
        EventsManager.AddConversationEvent("ZoomInCameraTimelapseToZoltar");
        EventsManager.AddConversationEvent("ZoomOutCameraTimelapseToZoltar");
        EventsManager.GetConversationEvent("ZoomInCameraTimelapseToZoltar").AddListener(ZoomInCameraToZoltar);
        EventsManager.GetConversationEvent("ZoomOutCameraTimelapseToZoltar").AddListener(ZoomOutCameraToZoltar);
    }

    private void OnDisable()
    {
        EventsManager.GetConversationEvent("ZoomInCameraTimelapseToZoltar")
            .RemoveListener(ZoomInCameraToZoltar);
        EventsManager.GetConversationEvent("ZoomOutCameraTimelapseToZoltar")
            .RemoveListener(ZoomOutCameraToZoltar);
    }

    private void ZoomInCameraToZoltar()
    {
        zoltarCamera.Priority = 10;
        levelCamera.Priority = 0;
    }

    private void ZoomOutCameraToZoltar()
    {
        zoltarCamera.Priority = 0;
        levelCamera.Priority = 10;
    }
}