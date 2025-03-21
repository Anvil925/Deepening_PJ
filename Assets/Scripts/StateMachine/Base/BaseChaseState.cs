using System;
using UnityEngine;

public abstract class BaseChaseState : IState
{
    protected float attackRange = 2f; // 공격 범위
    
    public void Enter()
    {
        Debug.Log("Entering Chase State"); // 추적 상태 진입
        OnEnterChase(); // 추적 상태 진입
    }
    
    public void Execute() // 매 프레임 실행
    {
        if (!IsTargetValid()) // 타겟이 유효하지 않으면
        {
            HandleTargetLost(); // 타겟을 잃음
            return;
        }
        
        if (IsInAttackRange()) // 공격 범위 내에 있으면
        {
            TransitionToAttack(); // 공격 상태로 전환
        }
        else
        {
            MoveTowardsTarget(); // 타겟으로 이동
        }
    }
    
    public void Exit() // 정리
    {
        Debug.Log("Exiting Chase State"); // 추적 상태 종료
        OnExitChase(); // 추적 상태 종료
    } 
    
    // 자식 클래스에서 구현할 추상 메서드들
    protected abstract bool IsTargetValid(); // 타겟이 유효한지 확인
    protected abstract bool IsInAttackRange(); // 공격 범위 내에 있는지 확인
    protected abstract void MoveTowardsTarget(); // 타겟으로 이동
    protected abstract void TransitionToAttack(); // 공격 상태로 전환
    protected abstract void HandleTargetLost(); // 타겟을 잃음
    
    // 선택적으로 오버라이드 가능한 가상 메서드들
    protected virtual void OnEnterChase() { } // 추적 상태 진입
    protected virtual void OnExitChase() { } // 추적 상태 종료
}