using System;

[Serializable]
public class PlayerStats : CharacterStats
{
    // 플레이어 전용 스탯
    public float goldGainMultiplier = 1.0f;
    public float expGainMultiplier = 1.0f;
    public int skillPoints = 0;
    
    // 플레이어 전용 레벨업 오버라이드
    protected override void LevelUp()
    {
        base.LevelUp();
        skillPoints += 3; // 레벨업마다 스킬 포인트 획득
    }
}