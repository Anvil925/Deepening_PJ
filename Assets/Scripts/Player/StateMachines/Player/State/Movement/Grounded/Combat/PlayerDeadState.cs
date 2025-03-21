namespace DeepeningPJ
{
    public class PlayerDeadState : PlayerState
    {
        public PlayerDeadState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            stateMachine.Player.ColliderUtility.CapsuleColliderData.Collider.enabled = false;
            stateMachine.Player.Rigidbody.useGravity = false;
            stateMachine.Player.Animator.applyRootMotion = true;

            StartAnimation(stateMachine.Player.AnimationData.DeadParameterHash);
        }

        public override void Exit()
        {
            base.Exit();

            stateMachine.Player.ColliderUtility.CapsuleColliderData.Collider.enabled = true;
            stateMachine.Player.Rigidbody.useGravity = true;
            stateMachine.Player.Animator.applyRootMotion = true;

            StopAnimation(stateMachine.Player.AnimationData.DeadParameterHash);
        }

        public override void Update()
        {
            
        }
        #endregion

        #region Event Methods
        protected override void TakeDamage()
        {
            
        }
        #endregion
    }
}
