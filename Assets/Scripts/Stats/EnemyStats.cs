using System;
using UnityEngine;

[Serializable]
public class EnemyStats : CharacterStats
{
    // 적 전용 속성
    public float experienceReward = 20f; // 경험치 보상
    public float goldReward = 10f; // 골드 보상
    public EnemyType enemyType = EnemyType.Normal; // 적 타입
    
    // 적 타입에 따른 보상 배율
    public float GetRewardMultiplier()
    {
        switch (enemyType) // 적 타입에 따라
        {
            case EnemyType.Elite: // 엘리트면
                return 2.5f; // 2.5배
            case EnemyType.Boss: // 보스면
                return 10f; // 10배
            default: // 그 외에는
                return 1f; // 1배
        }
    }
}

public enum EnemyType // 적 타입
{
    Normal,
    Elite,
    Boss
}