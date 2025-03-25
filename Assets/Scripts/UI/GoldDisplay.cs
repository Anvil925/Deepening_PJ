using System;
using TMPro;
using UnityEngine;

public class GoldDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;
    
    private void Start()
    {
        // GameManager 찾기
        if (GameManager.Instance != null)
        {
            // 초기 골드 표시 설정
            UpdateGoldDisplay(GameManager.Instance.Gold);
            
            // 골드 변경 이벤트 구독
            GameManager.Instance.OnGoldChanged += UpdateGoldDisplay;
        }
        else
        {
            Debug.LogError("GameManager를 찾을 수 없습니다!");
        }
    }
    
    private void UpdateGoldDisplay(float goldValue)
    {
        string formattedGold;
        
        if (goldValue >= 1_0000_0000_0000) // 1조 이상
        {
            double jo = Math.Floor(goldValue / 1_0000_0000_0000);
            double eok = Math.Floor((goldValue % 1_0000_0000_0000) / 1_0000_0000);
            double man = Math.Floor((goldValue % 1_0000_0000) / 1_0000);
            double rest = goldValue % 1_0000;
            
            formattedGold = $"{jo}조";
            if (eok > 0) formattedGold += $" {eok}억";
            if (man > 0) formattedGold += $" {man}만";
            if (rest > 0) formattedGold += $" {rest:N0}";
        }
        else if (goldValue >= 1_0000_0000) // 1억 이상
        {
            double eok = Math.Floor(goldValue / 1_0000_0000);
            double man = Math.Floor((goldValue % 1_0000_0000) / 1_0000);
            double rest = goldValue % 1_0000;
            
            formattedGold = $"{eok}억";
            if (man > 0) formattedGold += $" {man}만";
            if (rest > 0) formattedGold += $" {rest:N0}";
        }
        else if (goldValue >= 1_0000) // 1만 이상
        {
            double man = Math.Floor(goldValue / 1_0000);
            double rest = goldValue % 1_0000;
            
            formattedGold = $"{man}만";
            if (rest > 0) formattedGold += $" {rest:N0}";
        }
        else // 1만 미만
        {
            formattedGold = $"{goldValue:N0}";
        }
        
        goldText.text = $"{formattedGold}";
    }
    
    private void OnDestroy()
    {
        // 이벤트 구독 해제 (메모리 누수 방지)
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGoldChanged -= UpdateGoldDisplay;
        }
    }
}