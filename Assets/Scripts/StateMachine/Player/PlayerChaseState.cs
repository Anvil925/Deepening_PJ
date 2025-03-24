using UnityEngine;

public class PlayerChaseState : BaseChaseState
{
    private PlayerController _player; 
    
    public PlayerChaseState(PlayerController player) 
    {
        this._player = player;
    }
    
    protected override bool IsTargetValid()
    {
        return _player.target != null; // 타겟이 유효한지 확인
    }
    
    protected override bool IsInAttackRange()
    {
        if (_player.target == null) return false; // 타겟이 없으면 false 반환
        
        float distance = Vector3.Distance(_player.transform.position, _player.target.transform.position); // 플레이어와 타겟 사이의 거리 계산
        return distance <= attackRange; // 공격 범위 내에 있는지 반환
    }
    
    protected override void MoveTowardsTarget()
    {
        if (_player.target != null)
        {
            _player.MoveToTarget(_player.target.transform.position);
        }
    }
    
    protected override void TransitionToAttack()
    {
        _player.ChangeState(new PlayerAttackState(_player)); // 공격 상태로 변경
    }
    
    protected override void HandleTargetLost()
    {
        _player.ChangeState(new PlayerSearchState(_player)); // 타겟을 잃으면 탐색 상태로 변경
    }
}