using UnityEngine;


public class PlayerIdleState : BaseIdleState
{
    private PlayerController _player;
    
    public PlayerIdleState(PlayerController player)
    {
        this._player = player;
    }
    
    protected override void CheckForTransition()
    {
        Debug.Log("적 탐색 중...");
        GameObject nearestEnemy = _player.FindNearestEnemy();
        
        if (nearestEnemy != null)
        {
            Debug.Log($"적 발견: {nearestEnemy.name}");
            _player.SetTarget(nearestEnemy);
            _player.ChangeState(new PlayerChaseState(_player));
        }
    }
    
    protected override void DoIdleAction()
    {
        // 유휴 상태에서 플레이어의 동작
        // 예: 가만히 서있기, 주변 둘러보기, 가벼운 애니메이션 등
        Debug.Log("플레이어 대기 중...");
    }
}