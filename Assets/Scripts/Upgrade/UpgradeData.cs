using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeData
{
    public string name; // 업그레이드 이름
    public string description; // 업그레이드 설명
    public int level; // 현재 레벨
    public int maxLevel; // 최대 레벨
    public float baseValue; // 기본 값
    public float valuePerLevel; // 레벨 당 증가 값
    public float baseCost; // 기본 비용
    public float costMultiplier; // 비용 증가 배수
    public Sprite icon; // 아이콘
    
    public float GetCurrentValue()
    {
        return baseValue + (valuePerLevel * level); // 현재 레벨에 따른 값 계산
    }
    
    public float GetUpgradeCost()
    {
        return baseCost * Mathf.Pow(costMultiplier, level); // 현재 레벨에 따른 비용 계산
    }
}