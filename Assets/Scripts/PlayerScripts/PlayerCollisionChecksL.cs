
    using UnityEngine;

    public class PlayerCollisionChecksL: PlayerComponent
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) return;
            _parent.playerMovementComponent.triggerCollisionsL = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player")) return;
            _parent.playerMovementComponent.triggerCollisionsL = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player")) return;
            _parent.playerMovementComponent.triggerCollisionsL = false;
        }
        
    }
