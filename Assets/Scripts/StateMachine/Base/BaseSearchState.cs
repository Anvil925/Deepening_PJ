using System;
using UnityEngine;

public abstract class BaseSearchState : IState
{
    protected abstract GameObject FindTarget(); // 타겟 찾기
    
    public void Enter()
    {
        throw new NotImplementedException(); // 초기화 
    }

    public void Execute()
    {
        GameObject target = FindTarget(); // 타겟 찾기
        if (target != null) // 타겟이 있으면
        {
            OnTargetFound(target); // 타겟 찾음
        }
    }

    public void Exit()
    {
        throw new NotImplementedException(); // 정리
    }

    protected abstract void OnTargetFound(GameObject target); // 타겟 찾음
}