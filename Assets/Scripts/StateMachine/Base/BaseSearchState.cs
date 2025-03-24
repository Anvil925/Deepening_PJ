using System;
using UnityEngine;

public abstract class BaseSearchState : IState
{
    public void Enter()
    {
        Debug.Log("서치 상태 진입");
        OnEnterSearch();
    }

    public void Execute()
    {
        GameObject target = FindTarget();
        
        if (target != null)
        {
            OnTargetFound(target);
        }
    }

    public void Exit()
    {
        Debug.Log("서치 상태 종료");
        OnExitSearch();
    }

    // 자식 클래스에서 구현할 추상 메서드들
    protected abstract GameObject FindTarget();
    protected abstract void OnTargetFound(GameObject target);

    // 선택적 메서드들
    protected virtual void OnEnterSearch() { }
    protected virtual void OnExitSearch() { }
}