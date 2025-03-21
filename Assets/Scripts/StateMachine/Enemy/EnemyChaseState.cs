using UnityEngine;

public class EnemyChaseState : BaseChaseState
{
    private EnemyController _enemy;
    
    public EnemyChaseState(EnemyController enemy) 
    {
        this._enemy = enemy;
    }
    
    protected override bool IsTargetValid()
    {
        return _enemy.target != null; // 타겟이 유효한지 확인
    }
    
    protected override bool IsInAttackRange()
    {
        if (_enemy.target == null) return false; // 타겟이 없으면 false 반환
        
        float distance = Vector3.Distance(_enemy.transform.position, _enemy.target.transform.position); // 적과 타겟 사이의 거리 계산
        return distance <= attackRange; // 공격 범위 내에 있는지 반환
    }
    
    protected override void MoveTowardsTarget()
    {
        // 실제 구현 시 NavMeshAgent 등을 사용하여 이동
        Debug.Log($"{_enemy.gameObject.name}이(가) 플레이어를 추적 중");
    }
    
    protected override void TransitionToAttack()
    {
        _enemy.ChangeState(new EnemyAttackState(_enemy)); // 공격 상태로 변경
    }
    
    protected override void HandleTargetLost()
    {
        _enemy.ChangeState(new EnemySearchState(_enemy)); // 타겟을 잃으면 탐색 상태로 변경
    }
}