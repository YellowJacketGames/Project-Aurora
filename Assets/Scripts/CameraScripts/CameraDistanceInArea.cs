using UnityEngine;


public class CameraDistanceInArea : MonoBehaviour
{
    [SerializeField] float camDitanceInArea;
    private float previousCamDistance;
    private bool inArea;

    private void Awake()
    {
        gameObject.tag = "CameraArea";
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (inArea)
            return;

        if (!other.CompareTag("Player")) return;
        inArea = true;

        previousCamDistance = GameManager.instance.currentCameraManager.GetCamCurrentDistance();
        GameManager.instance.currentCameraManager.ApplyNewDistanceToAllCams(camDitanceInArea);
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (!inArea)
            return;

        if (!other.CompareTag("Player")) return;
        inArea = false;
        GameManager.instance.currentCameraManager.ApplyNewDistanceToAllCams(previousCamDistance);
    }
}