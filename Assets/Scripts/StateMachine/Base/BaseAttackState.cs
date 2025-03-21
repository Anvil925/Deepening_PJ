using UnityEngine;
using System.Collections;

public abstract class BaseAttackState : IState
{
    protected float attackCooldown; // 공격 쿨다운
    
    public void Enter()
    {
        Debug.Log("Entering Attack State");
        OnEnterAttack(); // 공격 상태 진입
    }
    
    public void Execute()
    {
        if (!IsTargetValid()) // 타겟이 유효하지 않으면
        {
            HandleTargetLost(); // 타겟을 잃음
            return;
        }
        
        if (!IsInAttackRange())
        {
            HandleTargetOutOfRange(); // 타겟이 공격 범위 밖에 있으면
            return;
        }
        
        PerformAttack(); // 공격 수행
    }
    
    public void Exit()
    {
        Debug.Log("Exiting Attack State"); 
        OnExitAttack(); // 공격 상태 종료
    }
    
    // 자식 클래스에서 구현할 메서드들
    protected abstract bool IsTargetValid(); // 타겟이 유효한지 확인
    protected abstract bool IsInAttackRange(); // 공격 범위 내에 있는지 확인
    protected abstract void PerformAttack(); // 공격 수행
    protected abstract void HandleTargetLost(); // 타겟을 잃음
    protected abstract void HandleTargetOutOfRange(); // 타겟이 공격 범위 밖에 있음
    
    // 선택적 메서드
    protected virtual void OnEnterAttack() { } // 공격 상태 진입
    protected virtual void OnExitAttack() { } // 공격 상태 종료
}