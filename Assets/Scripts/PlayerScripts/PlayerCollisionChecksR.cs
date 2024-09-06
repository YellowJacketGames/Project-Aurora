
    using UnityEngine;

    public class PlayerCollisionChecksR: PlayerComponent
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) return;
            _parent.playerMovementComponent.triggerCollisionsR = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player")) return;
            _parent.playerMovementComponent.triggerCollisionsR = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player")) return;
            _parent.playerMovementComponent.triggerCollisionsR = false;
        }
        
    }
