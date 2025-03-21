using ProjectM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerAttackState : PlayerGroundedState
{
    private bool hasTriggered = false;

    public PlayerAttackState(PlayerStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region IState Methods

    public override void Enter()
    {
        stateMachine.Player.Rigidbody.velocity = Vector3.zero;

        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.AttackingParameterHash);

        Attack();
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.AttackingParameterHash);

        stateMachine.Player.equipWeapon.isAttacking = false;
    }

    public override void Update()
    {
        RotateTowardsTargetRotation();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator != stateMachine.Player.Animator) return;

        if (animator.GetCurrentAnimatorClipInfo(layerIndex)[0].clip.name != "Attack") return;

        hasTriggered = true;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator != stateMachine.Player.Animator || layerIndex != 0) return;

        if (!hasTriggered) return;

        if (stateInfo.normalizedTime >= 0.95f)
        {
            hasTriggered = false;
            stateMachine.ChangeState(stateMachine.IdlingState);
        }
    }
    #endregion

    #region Main Methods

    protected virtual void Attack()
    {
        stateMachine.Player.equipWeapon.Attack();

        if (GetMovementInputDirection() != Vector3.zero)
        {
            Vector3 movementDirection = GetMovementInputDirection();

            float targetRotationYAngle = Rotate(movementDirection);

            Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);
        }
    }

    #endregion

    protected override void OnAttackStarted(InputAction.CallbackContext context)
    {
        
    }
}
