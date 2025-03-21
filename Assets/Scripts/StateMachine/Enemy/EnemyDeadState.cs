using UnityEngine;
using System.Collections;

public class EnemyDeadState : BaseDeadState
{
    private EnemyController enemy;
    
    public EnemyDeadState(EnemyController enemy)
    {
        this.enemy = enemy;
    }
    
    protected override void HandleDeath()
    {
        // 사망 처리
        Debug.Log($"{enemy.gameObject.name} 사망!");
        
        // 전리품 드롭 처리
        DropLoot();
        
        // 경험치와 골드 보상 처리
        GiveRewards();
        
        // 시체 사라짐 효과 (페이드 아웃 등)
        enemy.StartCoroutine(DisappearCorpse());
    }
    
    protected override void PrepareRespawn()
    {
        // 방치형 게임에서는 적이 사망하면 보통 완전히 제거됨
        // 새로운 적을 생성하는 방식으로 구현할 수 있음
        // 또는 오브젝트 풀링을 사용하여 재사용
        
        // 객체 비활성화 또는 제거
        GameObject.Destroy(enemy.gameObject);
    }
    
    private void DropLoot()
    {
        // 확률에 따라 아이템 드롭
        // 아이템 생성 및 배치
        Debug.Log("전리품 드롭!");
    }
    
    private void GiveRewards()
    {
        // 경험치, 골드 등 보상 지급
        if (enemy.target != null && enemy.target.TryGetComponent<PlayerController>(out var player))
        {
            // 플레이어에게 보상 지급
            if (player.TryGetComponent<StatsComponent>(out var playerStats))
            {
                // 경험치 보상
                float expReward = 20f; // 기본값 또는 enemy.stats.experienceReward
                playerStats.GainExperience(expReward);
                
                // 골드 보상 (게임 매니저나 인벤토리 시스템에 추가)
                Debug.Log($"플레이어에게 {expReward} 경험치 보상 지급");
            }
        }
    }
    
    private IEnumerator DisappearCorpse()
    {
        // 시체 페이드 아웃 효과
        yield return new WaitForSeconds(2f);
        
        // 오브젝트 비활성화 또는 제거 준비
        PrepareRespawn();
    }
}