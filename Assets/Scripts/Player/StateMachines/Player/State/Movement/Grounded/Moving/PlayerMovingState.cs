using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeepeningPJ
{
    public class PlayerMovingState : PlayerGroundedState
    {
        public PlayerMovingState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            StartAnimation(stateMachine.Player.AnimationData.MovingParameterHash);
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.MovingParameterHash);
        }
    }
}
