
    using UnityEngine;

    public class PlayerCollisionChecksB: PlayerComponent
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) return;
            _parent.playerMovementComponent.triggerCollisionsB = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player")) return;
            _parent.playerMovementComponent.triggerCollisionsB = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player")) return;
            _parent.playerMovementComponent.triggerCollisionsB = false;
        }
        
    }
