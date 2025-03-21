using ProjectM;
using UnityEngine;

namespace DeepeningPJ
{
    public class PlayerDamagedState : PlayerGroundedState
    {
        public PlayerDamagedState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            StartAnimation(stateMachine.Player.AnimationData.DamagedParameterHash);
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.DamagedParameterHash);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (animator != stateMachine.Player.Animator) return;

            if(stateMachine.Player.Hp <= 0) stateMachine.ChangeState(stateMachine.DeadState);
            else stateMachine.ChangeState(stateMachine.IdlingState);
        }
    }
}
