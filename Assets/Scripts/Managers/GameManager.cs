using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameManager 인스턴스가 없습니다!");
            }
            return _instance;
        }
    }
    
    // 다른 매니저들에 대한 참조
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private PlayerController playerController;
    // 나중에 추가할 매니저들: StageManager, EnemyManager, UIManager 등
    
    // 게임 상태 관련
    public enum GameState { Playing, Paused, GameOver, MainMenu }
    private GameState currentState;
    
    // 경제 시스템 이벤트
    public event Action<float> OnGoldChanged;
    
    // 오프라인 진행 관련
    private DateTime lastLoginTime;
    [SerializeField] private float goldPerMinuteOffline = 5f;
    
    private void Awake()
    {
        // 싱글톤 패턴 구현
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        // 컴포넌트 참조 초기화
        if (upgradeManager == null)
            upgradeManager = FindObjectOfType<UpgradeManager>();
            
        if (playerController == null)
            playerController = FindObjectOfType<PlayerController>();
    }
    
    private void Start()
    {
        currentState = GameState.Playing;
        LoadGameData();
        CalculateOfflineProgress();
    }
    
    private void OnApplicationQuit()
    {
        SaveGameData();
    }
    
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus) // 앱이 백그라운드로 갈 때
        {
            SaveGameData();
        }
        else // 앱이 다시 포그라운드로 올 때
        {
            CalculateOfflineProgress();
        }
    }
    
    #region 경제 시스템
    
    public float Gold
    {
        get { return upgradeManager != null ? upgradeManager.Gold : 0; }
        set 
        { 
            if (upgradeManager != null)
            {
                upgradeManager.Gold = value;
                OnGoldChanged?.Invoke(value);
            }
        }
    }
    
    public void AddGold(float amount)
    {
        Gold += amount;
        Debug.Log($"골드 획득: +{amount}, 총 골드: {Gold}");
    }
    
    public bool SpendGold(float amount)
    {
        if (Gold >= amount)
        {
            Gold -= amount;
            Debug.Log($"골드 사용: -{amount}, 남은 골드: {Gold}");
            return true;
        }
        
        Debug.Log($"골드 부족: 필요 {amount}, 보유 {Gold}");
        return false;
    }
    
    #endregion
    
    #region 저장 및 로드
    
    public void SaveGameData()
    {
        // 마지막 접속 시간 저장
        PlayerPrefs.SetString("LastLoginTime", DateTime.Now.ToString());
        
        // 골드 저장은 UpgradeManager에서 처리
        
        // 기타 게임 상태 저장
        PlayerPrefs.Save();
        Debug.Log("게임 데이터 저장 완료");
    }
    
    public void LoadGameData()
    {
        // 마지막 접속 시간 로드
        string lastLoginStr = PlayerPrefs.GetString("LastLoginTime", "");
        if (!string.IsNullOrEmpty(lastLoginStr))
        {
            DateTime.TryParse(lastLoginStr, out lastLoginTime);
        }
        else
        {
            lastLoginTime = DateTime.Now;
        }
        
        // 골드 로드는 UpgradeManager에서 처리
        
        Debug.Log("게임 데이터 로드 완료");
    }
    
    #endregion
    
    #region 오프라인 진행
    
    private void CalculateOfflineProgress()
    {
        if (lastLoginTime == default(DateTime))
            return;
            
        // 오프라인 시간 계산 (분 단위)
        TimeSpan offlineTime = DateTime.Now - lastLoginTime;
        float minutesOffline = (float)offlineTime.TotalMinutes;
        
        // 최대 시간 제한 (예: 24시간)
        minutesOffline = Mathf.Min(minutesOffline, 24 * 60);
        
        if (minutesOffline > 1)
        {
            // 오프라인 골드 보상 계산
            float goldEarned = minutesOffline * goldPerMinuteOffline;
            
            // 보상 추가
            AddGold(goldEarned);
            
            Debug.Log($"오프라인 보상: {minutesOffline:F1}분 동안 {goldEarned:F0} 골드 획득");
            
            // 여기에 UI 표시 로직 추가
        }
    }
    
    #endregion
    
    #region 게임 상태 관리
    
    public void SetGameState(GameState newState)
    {
        if (currentState == newState) return;
        
        currentState = newState;
        
        switch (currentState)
        {
            case GameState.Playing:
                Time.timeScale = 1f;
                break;
                
            case GameState.Paused:
                Time.timeScale = 0f;
                break;
                
            case GameState.GameOver:
                // 게임 오버 처리
                break;
                
            case GameState.MainMenu:
                // 메인 메뉴 처리
                break;
        }
    }
    
    #endregion
    
    #region 적 처치 관련
    
    public void OnEnemyDefeated(GameObject enemy)
    {
        // 적 종류나 레벨에 따라 골드 보상 결정
        float goldReward = 10f; // 기본값
        
        // 여기에 적 종류나 레벨에 따른 보상 계산 로직 추가
        
        // 골드 지급
        AddGold(goldReward);
        
        // 여기에 UI 이펙트 추가
    }
    
    #endregion
}