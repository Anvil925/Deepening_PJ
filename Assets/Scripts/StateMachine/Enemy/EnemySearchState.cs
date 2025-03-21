using UnityEngine;

public class EnemySearchState : BaseSearchState
{
    private EnemyController _enemy;
    
    public EnemySearchState(EnemyController enemy) 
    {
        this._enemy = enemy;
    }
    
    protected override GameObject FindTarget()
    {
        return _enemy.FindNearestPlayer(); // 가장 가까운 플레이어 찾기
    }
    
    protected override void OnTargetFound(GameObject target) // 타겟 찾음
    {
        _enemy.SetTarget(target); // 타겟 설정
        _enemy.ChangeState(new EnemyChaseState(_enemy)); // 추적 상태로 변경
        Debug.Log($"{_enemy.gameObject.name}이(가) 플레이어를 발견했습니다!");
    }
}