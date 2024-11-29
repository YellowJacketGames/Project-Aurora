public class PlayerCollisionChecksL : PlayerCollisionChecks
{
    protected override void ApplyTriggerBool(bool value)
    {
        _parent.playerMovementComponent.triggerCollisionsL = value;
    }
}