using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private List<UpgradeData> availableUpgrades = new List<UpgradeData>(); // 사용 가능한 업그레이드 목록
    [SerializeField] private PlayerController player; // 플레이어
    [SerializeField] private float gold; // 골드
    
    public float Gold { 
        get => gold; // 골드 반환
        set => gold = value;  // 골드 설정
    }
    
    private void Start() 
    {
        // 저장된 업그레이드 상태 로드
        LoadUpgradeStatus();
        
        // 초기 업그레이드 적용
        ApplyAllUpgrades();
    }
    
    public bool CanAffordUpgrade(string upgradeName)
    {
        UpgradeData upgrade = GetUpgrade(upgradeName); // 업그레이드 데이터 가져오기
        if (upgrade == null) return false; // 업그레이드 데이터가 없으면 false 반환
        
        return Gold >= upgrade.GetUpgradeCost(); // 골드가 충분하면 true 반환
    }
    
    public bool PurchaseUpgrade(string upgradeName)
    {
        UpgradeData upgrade = GetUpgrade(upgradeName); // 업그레이드 데이터 가져오기
        if (upgrade == null) return false; // 업그레이드 데이터가 없으면 false 반환
        
        // 최대 레벨 체크
        if (upgrade.level >= upgrade.maxLevel) // 최대 레벨에 도달하면 false 반환
            return false;
            
        float cost = upgrade.GetUpgradeCost(); // 업그레이드 비용
        if (Gold >= cost) // 골드가 충분하면 업그레이드
        {
            Gold -= cost; // 골드 차감
            upgrade.level++; // 레벨 증가
            
            // 업그레이드 효과 적용
            ApplyUpgrade(upgrade);
            
            // 저장
            SaveUpgradeStatus();
            
            return true;
        }
        
        return false;
    }
    
    private UpgradeData GetUpgrade(string name)
    {
        return availableUpgrades.Find(u => u.name == name); // 업그레이드 이름으로 찾기
    }
    
    private void ApplyUpgrade(UpgradeData upgrade)
    {
        // 업그레이드 효과 적용
        switch (upgrade.name)
        {
            case "Attack Power":
                player.AttackPower = upgrade.GetCurrentValue(); // 공격력 적용
                break;
            case "Health":
                player.Health = upgrade.GetCurrentValue(); // 체력 적용
                break;
            case "Defense":
                player.Defense = upgrade.GetCurrentValue(); // 방어력 적용
                break;
            case "Move Speed":
                player.MoveSpeed = upgrade.GetCurrentValue(); // 이동 속도 적용
                break;
            // 다른 업그레이드...
        }
    }
    
    private void ApplyAllUpgrades()
    {
        foreach (var upgrade in availableUpgrades) // 모든 업그레이드 적용
        {
            ApplyUpgrade(upgrade); // 업그레이드 효과 적용
        }
    }
    
    private void SaveUpgradeStatus()
    {
        // PlayerPrefs 또는 JSON으로 저장
        foreach (var upgrade in availableUpgrades)
        {
            PlayerPrefs.SetInt(upgrade.name + "_level", upgrade.level); // 업그레이드 레벨 저장
        }
        
        PlayerPrefs.SetFloat("Gold", Gold); // 골드 저장
        PlayerPrefs.Save(); // 저장
    }
    
    private void LoadUpgradeStatus()
    {
        // 저장된 업그레이드 상태 로드
        foreach (var upgrade in availableUpgrades)
        {
            upgrade.level = PlayerPrefs.GetInt(upgrade.name + "_level", 0); // 업그레이드 레벨 로드
        }
        
        Gold = PlayerPrefs.GetFloat("Gold", 0); // 골드 로드
    }
}