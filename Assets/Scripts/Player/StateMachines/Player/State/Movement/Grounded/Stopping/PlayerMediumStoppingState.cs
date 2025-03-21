using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeepeningPJ
{
    public class PlayerMediumStoppingState : PlayerStoppingState
    {
        public PlayerMediumStoppingState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            StartAnimation(stateMachine.Player.AnimationData.MediumStopParameterHash);

            stateMachine.ReusableData.MovementDecelerationForce = movementData.StopData.MediumDecelerationForce;

            stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.MediumForce;
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.MediumStopParameterHash);
        }
        #endregion
    }
}
