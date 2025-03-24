using UnityEngine;
using System.Collections;

public class PlayerAttackState : BaseAttackState
{
    private PlayerController _player;
    private float _attackRange = 2f; // 공격 범위
    private Coroutine _attackCoroutine; // 공격 코루틴
    
    public PlayerAttackState(PlayerController player)
    {
        this._player = player; 
        this.attackCooldown = 1f / player.AttackSpeed; // 공격 속도에 따른 쿨타임 계산
    }
    
    protected override void OnEnterAttack()
    {
        // 공격 상태 진입 시 이동 중지
        _player.StopMoving();
        
        // 공격 코루틴 시작
        _attackCoroutine = _player.StartCoroutine(AttackRoutine());
    }
    
    protected override void OnExitAttack()
    {
        // 실행 중인 코루틴 중지
        if (_attackCoroutine != null)
            _player.StopCoroutine(_attackCoroutine);
    }
    
    protected override bool IsTargetValid()
    {
        // 기존 검사: 타겟이 null이 아닌지
        if (_player.target == null) return false;
        
        // 추가 검사: 타겟이 살아있는지
        StatsComponent targetStats = _player.target.GetComponent<StatsComponent>();
        if (targetStats == null || !targetStats.IsAlive)
        {
            HandleDeadTarget();
            return false;
        }
        
        return true;
    }
    
    private void HandleDeadTarget()
    {
        Debug.Log("죽은 타겟 감지, 타겟 초기화");
        _player.SetTarget(null);
    }
    
    protected override bool IsInAttackRange()
    {
        if (_player.target == null) return false; // 타겟이 없으면 false 반환
        
        float distance = Vector3.Distance(_player.transform.position, _player.target.transform.position); // 플레이어와 타겟 사이의 거리 계산
        return distance <= _attackRange; // 공격 범위 내에 있는지 반환
    }
    
    protected override void PerformAttack()
    {
        // 이 메서드는 코루틴에서 처리하므로 비워둠
    }
    
    protected override void HandleTargetLost()
    {
        _player.ChangeState(new PlayerSearchState(_player)); // 타겟을 잃으면 탐색 상태로 변경
    }
    
    protected override void HandleTargetOutOfRange()
    {
        _player.ChangeState(new PlayerChaseState(_player)); // 타겟이 공격 범위 밖에 있으면 추적 상태로 변경
    }
    
    private IEnumerator AttackRoutine()
    {
        while (IsTargetValid())
        {
            // 실제 공격 실행
            _player.PerformAttack(); 
            Debug.Log($"{_player.gameObject.name}이(가) 공격 중");
            
            // 공격 속도에 따른 대기
            yield return new WaitForSeconds(attackCooldown);
            
            // 공격 범위를 벗어났는지 확인
            if (!IsInAttackRange())
            {
                HandleTargetOutOfRange();
                yield break;
            }
        }
        
        // 타겟이 없어진 경우
        HandleTargetLost();
    }
}