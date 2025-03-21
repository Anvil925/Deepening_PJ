using UnityEngine;
using System.Collections;

public class PlayerDeadState : BaseDeadState
{
    private PlayerController player;
    
    public PlayerDeadState(PlayerController player)
    {
        this.player = player;
        this.respawnTime = 1f; // 플레이어는 부활 시간이 좀 더 길게
    }
    
    protected override void HandleDeath()
    {
        // 사망 처리
        Debug.Log("플레이어 사망!");
        
        // 사망 애니메이션 재생
        // 사망 이펙트 표시
        
        // 플레이어 입력 비활성화
        
        // 부활 코루틴 시작
        player.StartCoroutine(RespawnRoutine());
    }
    
    protected override void PrepareRespawn()
    {
        // 플레이어 부활 준비
        Debug.Log("플레이어 부활 준비");
        
        // 체력 일부 회복
        if (player.TryGetComponent<StatsComponent>(out var stats)) // StatsComponent 컴포넌트가 있으면
        {
            stats.baseStats.currentHealth = stats.baseStats.maxHealth * 0.5f; // 최대 체력의 절반만 회복
        }
        
        // 안전 지역으로 이동하거나 제자리에서 부활
        
        // 다시 Idle 상태로 전환
        player.ChangeState(new PlayerIdleState(player));
    }
    
    private IEnumerator RespawnRoutine()
    {
        // 부활 대기 시간
        yield return new WaitForSeconds(respawnTime);
        
        // 부활 처리
        PrepareRespawn();
    }
}