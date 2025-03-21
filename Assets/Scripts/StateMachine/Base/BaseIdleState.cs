using UnityEngine;

public abstract class BaseIdleState : IState
{
    protected float idleCheckInterval = 1f; // 유휴 상태 체크 주기
    protected float lastCheckTime = 0f; // 마지막 체크 시간
    
    public void Enter()
    {
        Debug.Log("Entering Idle State");
        OnEnterIdle(); // 유휴 상태 진입
    }
    
    public void Execute()
    {
        // 주기적으로 다음 상태로 전환할지 확인
        if (Time.time >= lastCheckTime + idleCheckInterval) 
        {
            lastCheckTime = Time.time; // 마지막 체크 시간 갱신
            CheckForTransition(); // 상태 전환 확인
        }
        
        // 유휴 상태에서의 동작 처리
        DoIdleAction();
    }
    
    public void Exit()
    {
        Debug.Log("Exiting Idle State");
        OnExitIdle(); // 유휴 상태 종료
    }
    
    // 자식 클래스에서 구현할 메서드들
    protected abstract void CheckForTransition(); // 상태 전환 확인
    protected abstract void DoIdleAction(); // 유휴 상태에서의 동작 처리
    
    // 선택적 메서드
    protected virtual void OnEnterIdle() { } // 유휴 상태 진입
    protected virtual void OnExitIdle() { } // 유휴 상태 종료
}