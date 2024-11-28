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
            GameManager.instance.currentController.playerMovementComponent.DisableAllInput();
            Invoke("ChangePlayerPosition", 2f);
        }
    }

    public void ChangePlayerPosition()
    {
        GameManager.instance.currentController.playerRigid.isKinematic = true;
        GameManager.instance.currentController.transform.position = newPosition.position;
        GameManager.instance.currentCameraManager.ReturnReferences();
        GameManager.instance.currentController.playerMovementComponent.EnableAllInput();
        GameManager.instance.currentController.ChangeState(PlayerState.Idle);
        GameManager.instance.currentController.playerRigid.isKinematic = false;
    }
}