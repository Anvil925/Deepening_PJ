using UnityEngine;
using System.Collections;

public class EnemyAttackState : BaseAttackState
{
    private EnemyController _enemy;
    private float _attackRange = 1.5f; // 공격 범위
    private Coroutine _attackCoroutine; // 공격 코루틴
    
    public EnemyAttackState(EnemyController enemy)
    {
        this._enemy = enemy;
        this.attackCooldown = 1f / enemy.attackSpeed; // 공격 속도에 따른 쿨다운 계산
    }
    
    protected override void OnEnterAttack() 
    {
        _attackCoroutine = _enemy.StartCoroutine(AttackRoutine()); // 공격 코루틴 시작
    }
    
    protected override void OnExitAttack()
    {
        if (_attackCoroutine != null) // 코루틴이 null이 아니면
            _enemy.StopCoroutine(_attackCoroutine); // 실행 중인 코루틴 중지
    }
    
    protected override bool IsTargetValid()
    {
        return _enemy.target != null; // 타겟이 유효한지 확인
    }
    
    protected override bool IsInAttackRange()
    {
        if (_enemy.target == null) return false; // 타겟이 없으면 false 반환
        
        float distance = Vector3.Distance(_enemy.transform.position, _enemy.target.transform.position); // 적과 타겟 사이의 거리 계산
        return distance <= _attackRange; // 공격 범위 내에 있는지 반환
    }
    
    protected override void PerformAttack()
    {
        // 이 메서드는 코루틴에서 처리
    }
    
    protected override void HandleTargetLost()
    {
        _enemy.ChangeState(new EnemySearchState(_enemy)); // 타겟을 잃으면 탐색 상태로 변경
    }
    
    protected override void HandleTargetOutOfRange()
    {
        _enemy.ChangeState(new EnemyChaseState(_enemy)); // 타겟이 공격 범위 밖에 있으면 추적 상태로 변경
    }
    
    private IEnumerator AttackRoutine()
    {
        while (IsTargetValid()) // 타겟이 유효한 동안
        {
            _enemy.PerformAttack(); // 적 공격 
            yield return new WaitForSeconds(attackCooldown); // 공격 속도에 따른 대기
            Debug.Log($"{_enemy.gameObject.name}이(가) 공격 중");
            
            if (!IsInAttackRange()) // 공격 범위를 벗어났다면
            {
                HandleTargetOutOfRange(); // 타겟이 공격 범위 밖에 있을 때 처리
                yield break; // 코루틴 종료
            }
        }
        
        HandleTargetLost(); // 타겟을 잃었을 때 처리
    }
}