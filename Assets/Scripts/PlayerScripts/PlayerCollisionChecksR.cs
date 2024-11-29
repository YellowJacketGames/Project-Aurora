public class PlayerCollisionChecksR : PlayerCollisionChecks
{
    protected override void ApplyTriggerBool(bool value)
    {
        _parent.playerMovementComponent.triggerCollisionsR = value;
    }
}