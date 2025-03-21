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
        // 주변에 적이 있는지 확인
        GameObject nearestEnemy = _player.FindNearestEnemy();
        
        if (nearestEnemy != null)
        {
            // 적을 발견하면 타겟 설정 후 추적 상태로 전환
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