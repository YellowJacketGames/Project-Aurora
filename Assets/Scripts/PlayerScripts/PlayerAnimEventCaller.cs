using UnityEngine;

namespace PlayerScripts
{
    public class PlayerAnimEventCaller : MonoBehaviour
    {
        private PlayerMovement _playerMovement;

        private void Awake()
        {
            _playerMovement = GetComponentInParent<PlayerMovement>();
        }

        public void TriggerJumpPhysics()
        {
            _playerMovement.JumpPhysicsFromAnim();
        }

        public void StopVelocityPlayerForAFrame()
        {
            _playerMovement.StopPlayerForAFrameAnim();
        }
    }
}