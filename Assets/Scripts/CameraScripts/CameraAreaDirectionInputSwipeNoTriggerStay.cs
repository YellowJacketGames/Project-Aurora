using UnityEngine;
using UnityEngine.Serialization;

public class CameraAreaDirectionInputSwipeNoTriggerStay : CameraArea
{
    [SerializeField] private PlayerMovement.MovementType newMovementType;
    [SerializeField] private PlayerMovement.MovementDirection newMovementDirection;
    
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
        base.ExitArea(); // camera manager code
        
        //player movement update direction
        GameManager.instance.currentController.playerMovementComponent.ChangeMovementDirection(_initialMovementType, _initialMovementDirection);

    }
    
  
} 