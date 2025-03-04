using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPosition : MonoBehaviour
{
    [SerializeField] private Transform newPosition;
    [SerializeField] private float transitionwaittime = 2f;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.currentTransitionManager.CompleteTransition();
            GameManager.instance.currentCameraManager.LoseReferences();
            GameManager.instance.currentController.playerMovementComponent.DisableAllInput();
            Invoke("ChangePlayerPosition", transitionwaittime);
        }
    }

    public virtual void ChangePlayerPosition()
    {
        BroadcastMessage("ACH_5_FAILED");
        GameManager.instance.currentController.playerRigid.isKinematic = true;
        GameManager.instance.currentController.transform.position = newPosition.position;
        GameManager.instance.currentCameraManager.ReturnReferences();
        GameManager.instance.currentController.playerMovementComponent.EnableAllInput();
        GameManager.instance.currentController.ChangeState(PlayerState.Idle);
        GameManager.instance.currentController.playerRigid.isKinematic = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Vector3 objectSize = transform.localScale;
        Gizmos.DrawWireCube(transform.position, objectSize);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(newPosition.position, 0.2f);
    }
}