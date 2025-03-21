using System;
using UnityEngine;
using UnityEngine.Events;

public class StatsComponent : MonoBehaviour
{
    public CharacterStats baseStats = new CharacterStats();
    
    // 이벤트
    public UnityEvent onDeath; // 사망 이벤트
    public UnityEvent<float> onDamaged; // 피해 이벤트
    public UnityEvent<int> onLevelUp; // 레벨업 이벤트
    
    public bool IsAlive => baseStats.currentHealth > 0; // 살아있는지 여부
    
    private void Start() 
    {
        baseStats.Initialize();
    }
    
    public void TakeDamage(float damage)
    {
        float actualDamage = baseStats.TakeDamage(damage); // 실제로 받은 대미지
        onDamaged?.Invoke(actualDamage); // 피해 이벤트 발생
        
        if (baseStats.currentHealth <= 0)
        {
            Die(); // 사망
        }
    }
    
    public float CalculateDamage()
    {
        return baseStats.CalculateDamage(); // 대미지 계산
    }
    
    private void Die()
    {
        onDeath?.Invoke(); // 사망 이벤트 발생
        Debug.Log($"{gameObject.name} 사망"); 
    }
    
    public void GainExperience(float expAmount)
    {
        if (baseStats.GainExperience(expAmount)) // 경험치 획득
        {
            onLevelUp?.Invoke(baseStats.level); // 레벨업 이벤트 발생
        }
    }
}