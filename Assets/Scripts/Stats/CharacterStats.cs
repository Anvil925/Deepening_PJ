using System;
using UnityEngine;

[Serializable]
public class CharacterStats
{
    [Header("기본 스탯")]
    public float maxHealth = 100f;
    public float currentHealth;
    public float attackPower = 10f;
    public float defense = 5f;
    public float attackSpeed = 1f;
    public float moveSpeed = 3f;
    
    [Header("추가 스탯")]
    public float critChance = 0.05f;
    public float critDamage = 1.5f;
    public float lifeSteal = 0f;
    
    [Header("레벨")]
    public int level = 1;
    public float experiencePoints = 0f;
    public float experienceToNextLevel = 100f;
    
    // 초기화
    public void Initialize()
    {
        currentHealth = maxHealth;
    }
    
    // 대미지 계산
    public float CalculateDamage()
    {
        // 기본 공격력
        float damage = attackPower;
        
        // 크리티컬 계산
        if (UnityEngine.Random.value <= critChance)
        {
            damage *= critDamage;
            Debug.Log("크리티컬 히트!");
        }
        
        return damage;
    }
    
    // 대미지 받기
    public float TakeDamage(float incomingDamage)
    {
        // 방어력 적용 (간단한 공식)
        float actualDamage = incomingDamage * (100f / (100f + defense));
        currentHealth -= actualDamage;
        
        if (currentHealth < 0)
            currentHealth = 0;
            
        return actualDamage;
    }
    
    // 경험치 획득
    public bool GainExperience(float expAmount)
    {
        experiencePoints += expAmount;
        
        // 레벨업 체크
        if (experiencePoints >= experienceToNextLevel)
        {
            LevelUp();
            return true;
        }
        
        return false;
    }
    
    // 레벨업
    protected virtual void LevelUp()
    {
        level++;
        experiencePoints -= experienceToNextLevel;
        experienceToNextLevel *= 1.2f; // 다음 레벨업 필요 경험치 증가
        
        // 스탯 증가
        maxHealth *= 1.1f;
        attackPower *= 1.1f;
        defense *= 1.05f;
        
        // 체력 회복
        currentHealth = maxHealth;
    }
}