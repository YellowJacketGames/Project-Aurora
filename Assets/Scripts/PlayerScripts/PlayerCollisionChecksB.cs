public class PlayerCollisionChecksB : PlayerCollisionChecks
{
    protected override void ApplyTriggerBool(bool value)
    {
        _parent.playerMovementComponent.triggerCollisionsB = value;
    }
}