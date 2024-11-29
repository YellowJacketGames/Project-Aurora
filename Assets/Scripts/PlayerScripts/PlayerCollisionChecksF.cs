
    using UnityEngine;

    public class PlayerCollisionChecksF: PlayerComponent
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) return;
            _parent.playerMovementComponent.triggerCollisionsF = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player")) return;
            _parent.playerMovementComponent.triggerCollisionsF = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player")) return;
            _parent.playerMovementComponent.triggerCollisionsF = false;
        }
        
    }
