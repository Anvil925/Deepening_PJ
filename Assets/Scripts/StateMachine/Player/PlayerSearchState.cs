using UnityEngine;
public class PlayerSearchState : BaseSearchState 
{
    private PlayerController _player; 
    
    public PlayerSearchState(PlayerController player)
    {
        this._player = player;
    }
    
    protected override GameObject FindTarget()
    {
        return _player.FindNearestEnemy(); // 가장 가까운 적 찾기
    }
    
    protected override void OnTargetFound(GameObject target) // 타겟 찾음
    {
        _player.SetTarget(target); // 타겟 설정
        _player.ChangeState(new PlayerChaseState(_player)); // 추적 상태로 변경
    }
}