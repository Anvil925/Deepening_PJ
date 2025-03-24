using UnityEngine;

public class EnemyIdleState : BaseIdleState
{
    private EnemyController _enemy;
    
    public EnemyIdleState(EnemyController enemy)
    {
        this._enemy = enemy;
    }
    
    protected override void CheckForTransition()
    {
        // 주변에 플레이어가 있는지 확인
        GameObject player = _enemy.FindNearestPlayer(); 
        
        if (player != null)
        {
            _enemy.SetTarget(player); // 플레이어를 타겟으로 설정
            _enemy.ChangeState(new EnemyChaseState(_enemy)); // 추적 상태로 전환
        }
    }
    
    protected override void DoIdleAction()
    {
        // 대기 상태에서의 행동
        // 예: 제자리 배회, 주변 둘러보기 등
        // Debug.Log($"{_enemy.gameObject.name}이(가) 대기 중...");
    }
}