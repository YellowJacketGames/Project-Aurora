    public class PlayerCollisionChecksF: PlayerCollisionChecks
    {
        protected override void ApplyTriggerBool(bool value)
        {
            _parent.playerMovementComponent.triggerCollisionsF = value;
        }
      
    }
