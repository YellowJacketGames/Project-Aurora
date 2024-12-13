using UnityEngine;

public class ResetPlayerPositionArray : MonoBehaviour
{
    [SerializeField] private Transform[] newPositions;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameManager.instance.currentController.transform;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.currentTransitionManager.CompleteTransition();
            GameManager.instance.currentCameraManager.LoseReferences();
            GameManager.instance.currentController.playerMovementComponent.DisableAllInput();
            Invoke("ChangePlayerPosition", 2f);
        }
    }

    public virtual void ChangePlayerPosition()
    {
        GameManager.instance.currentController.playerRigid.isKinematic = true;
        GameManager.instance.currentController.transform.position = GetClosestCheckpoint().position;
        GameManager.instance.currentCameraManager.ReturnReferences();
        GameManager.instance.currentController.playerMovementComponent.EnableAllInput();
        GameManager.instance.currentController.ChangeState(PlayerState.Idle);
        GameManager.instance.currentController.playerRigid.isKinematic = false;
    }

    private Transform GetClosestCheckpoint()
    {
        return null; //not working correctly
        
        Transform closest = null;
        var shortestDistanceSquared = float.MaxValue;

        foreach (Transform checkpoint in newPositions)
        {
            if (checkpoint.position.z < playerTransform.position.z)
            {
                var distanceSquared = (checkpoint.position - playerTransform.position).sqrMagnitude;
                if (distanceSquared < shortestDistanceSquared)
                {
                    shortestDistanceSquared = distanceSquared;
                    closest = checkpoint;
                }
            }
        }

        return closest;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Vector3 objectSize = transform.localScale;
        Gizmos.DrawWireCube(transform.position, objectSize);

        Gizmos.color = Color.yellow;
        foreach (var pos in newPositions)
        {
            Gizmos.DrawWireSphere(pos.position, 0.2f);
        }
    }
}