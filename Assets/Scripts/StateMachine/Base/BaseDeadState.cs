using UnityEngine;
using System.Collections;

public abstract class BaseDeadState : IState
{
    protected float respawnTime = 3f; // 기본 부활 시간
    
    public void Enter()
    {
        Debug.Log("Entering Dead State");
        OnEnterDead();
        HandleDeath();
    }
    
    public void Execute()
    {
        // 사망 상태에서는 특별한 업데이트 로직이 없을 수 있음
        // 또는 사망 이펙트, 투명도 조절 등을 여기서 처리
    }
    
    public void Exit()
    {
        Debug.Log("Exiting Dead State");
        OnExitDead();
    }
    
    // 자식 클래스에서 구현할 메서드들
    protected abstract void HandleDeath();
    protected abstract void PrepareRespawn();
    
    // 선택적 메서드
    protected virtual void OnEnterDead() { }
    protected virtual void OnExitDead() { }
}