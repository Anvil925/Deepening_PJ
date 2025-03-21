using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeepeningPJ
{
    public class PlayerLightLandingState : PlayerLandingState
    {
        public PlayerLightLandingState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            stateMachine.ReusableData.MovementSpeedModifier = 0f;

            base.Enter();

            stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.StationaryForce;

            ResetVelocity();
        }

        public override void Update()
        {
            base.Update();

            if (stateMachine.ReusableData.MovementInput == Vector2.zero) return;

            OnMove();
        }

        public override void OnAnimationTransitionEvent()
        {
            if (stateMachine.ReusableData.ShouldSprint)
            {
                stateMachine.ChangeState(stateMachine.SprintingState);
            }
            else stateMachine.ChangeState(stateMachine.IdlingState);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (!IsMovingHorizontally()) return;

            ResetVelocity();
        }
        #endregion

    }
}
