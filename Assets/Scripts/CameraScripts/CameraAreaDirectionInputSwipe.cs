using UnityEngine;
using UnityEngine.Serialization;

public class CameraAreaDirectionInputSwipe : CameraArea
{
    [SerializeField] private PlayerMovement.MovementType newMovementType;
    [SerializeField] private PlayerMovement.MovementDirection newMovementDirection;
    [SerializeField] private bool shouldRestorePreviousMovementType;
    private PlayerMovement.MovementType _initialMovementType;
    private PlayerMovement.MovementDirection  _initialMovementDirection;
    protected override void ChangeToArea()
    {
        base.ChangeToArea(); // camera manager code
        
        //player movement update direction
       (_initialMovementType, _initialMovementDirection)  = GameManager.instance.currentController.playerMovementComponent.GetMovementDirection();
        GameManager.instance.currentController.playerMovementComponent.ChangeMovementDirection(newMovementType, newMovementDirection);
    }

    protected override void ExitArea()
    {
        // if(GameManager.instance.currentController.playerMovementComponent.IsInCameraAreaSwipe)
        base.ExitArea(); // camera manager code
        
        //player movement update direction
        if(shouldRestorePreviousMovementType)
        GameManager.instance.currentController.playerMovementComponent.ChangeMovementDirection(_initialMovementType, _initialMovementDirection);

    }
    
    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            base.ChangeToArea();
        }

    }
} 