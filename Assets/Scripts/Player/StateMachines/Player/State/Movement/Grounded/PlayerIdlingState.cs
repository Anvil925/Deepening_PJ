using UnityEngine;

namespace DeepeningPJ
{
    public class PlayerIdlingState : PlayerGroundedState
    {
        private PlayerIdleData idleData;
        private bool hasIdling = false;

        public PlayerIdlingState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            idleData = movementData.IdleData;
        }

        #region IState Methods
        public override void Enter()
        {
            stateMachine.ReusableData.MovementSpeedModifier = 0f;

            stateMachine.ReusableData.BackwardsCameraRecenteringData = idleData.BackwardsCameraRecenteringData;

            SetFloatAnimation(stateMachine.Player.AnimationData.RandomIdleParameterHash, Random.Range(0, 1f));
            StartAnimation(stateMachine.Player.AnimationData.IdleParameterHash);

            base.Enter();

            stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.StationaryForce;

            ResetVelocity();
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
        }

        public override void Update()
        {
            base.Update();

            if (stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                return;
            }

            OnMove();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (!IsMovingHorizontally()) return;

            ResetVelocity();
        }
        #endregion

        #region Input Methods

        #endregion
    }
}
