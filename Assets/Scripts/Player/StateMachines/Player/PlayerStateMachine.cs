using ProjectM;

namespace DeepeningPJ
{
    public class PlayerStateMachine : StateMachine
    {
        public PlayerHandler Player { get; }
        public PlayerStateReusableData ReusableData { get; }

        #region Moving
        public PlayerIdlingState IdlingState { get; }
        public PlayerDashingState DashingState { get; }

        public PlayerWalkingState WalkingState { get; }
        public PlayerRunningState RunningState { get; }
        public PlayerSprintingState SprintingState { get; }

        public PlayerLightStoppingState LightStoppingState { get; }
        public PlayerMediumStoppingState MediumStoppingState { get; }
        public PlayerHardStoppingState HardStoppingState { get; }

        public PlayerLightLandingState LightLandingState { get; }
        public PlayerRollingState RollingState { get; }
        public PlayerHardLandingState HardLandingState { get; }

        public PlayerJumpingState JumpingState { get; }
        public PlayerFallingState FallingState { get; }
        #endregion

        #region Combat
        public PlayerAttackState AttackState { get; }
        public PlayerDamagedState DamagedState { get; }
        public PlayerDeadState DeadState { get; }
        #endregion

        public PlayerStateMachine(PlayerHandler player)
        {
            Player = player;
            ReusableData = new PlayerStateReusableData();

            IdlingState = new PlayerIdlingState(this);
            DashingState = new PlayerDashingState(this);

            WalkingState = new PlayerWalkingState(this);
            RunningState = new PlayerRunningState(this);
            SprintingState = new PlayerSprintingState(this);

            LightStoppingState = new PlayerLightStoppingState(this);
            MediumStoppingState = new PlayerMediumStoppingState(this);
            HardStoppingState = new PlayerHardStoppingState(this);

            JumpingState = new PlayerJumpingState(this);
            FallingState = new PlayerFallingState(this);

            LightLandingState = new PlayerLightLandingState(this);
            RollingState = new PlayerRollingState(this);
            HardLandingState = new PlayerHardLandingState(this);

            AttackState = new PlayerAttackState(this);
            DamagedState = new PlayerDamagedState(this);
            DeadState = new PlayerDeadState(this);
        }
    }
}
