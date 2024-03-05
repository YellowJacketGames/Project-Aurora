using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPosition : MonoBehaviour
{
    [SerializeField] private Transform newPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.currentTransitionManager.CompleteTransition();
            GameManager.instance.currentCameraManager.LoseReferences();
            Invoke("ChangePlayerPosition", 1.1f);
        }
    }

    public void ChangePlayerPosition()
    {
        GameManager.instance.currentController.transform.position = newPosition.position;
        GameManager.instance.currentCameraManager.ReturnReferences();
    }
}
