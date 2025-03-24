// using UnityEngine;
// using UnityEngine.UI;

// public class UpgradeButton : MonoBehaviour
// {
//     [SerializeField] private string upgradeName;
//     [SerializeField] private Text nameText;
//     [SerializeField] private Text levelText;
//     [SerializeField] private Text costText;
//     [SerializeField] private Text valueText;
//     [SerializeField] private Button button;
    
//     private UpgradeManager upgradeManager;
//     private UpgradeData upgradeData;
    
//     private void Start()
//     {
//         upgradeManager = FindObjectOfType<UpgradeManager>();
//         button.onClick.AddListener(OnUpgradeButtonClicked);
        
//         // 업그레이드 데이터 가져오기
//         upgradeData = upgradeManager.GetUpgradeData(upgradeName);
//         UpdateUI();
//     }
    
//     private void OnUpgradeButtonClicked()
//     {
//         if (upgradeManager.PurchaseUpgrade(upgradeName))
//         {
//             UpdateUI();
//         }
//     }
    
//     private void UpdateUI()
//     {
//         nameText.text = upgradeData.name;
//         levelText.text = $"Lv.{upgradeData.level}/{upgradeData.maxLevel}";
//         costText.text = $"Cost: {upgradeData.GetUpgradeCost():F0}";
//         valueText.text = $"Value: {upgradeData.GetCurrentValue():F1}";
        
//         // 구매 가능 여부에 따라 버튼 활성화/비활성화
//         button.interactable = upgradeManager.CanAffordUpgrade(upgradeName) && 
//                               upgradeData.level < upgradeData.maxLevel;
//     }
// }