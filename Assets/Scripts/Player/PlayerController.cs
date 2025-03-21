using System.Collections;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    private StateMachine _stateMachine; // 상태 머신
    private Coroutine _combatCoroutine; // 전투 코루틴
    public float attackSpeed = 1f; // 공격 속도
    public GameObject target; // 타겟
    public float searchRadius = 10f; // 탐색 반경

    [SerializeField] private PlayerStats stats = new PlayerStats();
    private StatsComponent statsComponent;

    private void Awake()
    {
        statsComponent = GetComponent<StatsComponent>(); // StatsComponent 컴포넌트 가져오기
        if (statsComponent == null)
        {
            statsComponent = gameObject.AddComponent<StatsComponent>(); 
        }
    }

    void Start()
    {
        _stateMachine = new StateMachine(); // 상태 머신 초기화
        _stateMachine.ChangeState(new PlayerIdleState(this)); // 초기 상태 설정
        
        // 자동전투 코루틴 시작
        _combatCoroutine = StartCoroutine(CombatRoutine()); 
    }
    
    // 가장 가까운 적을 찾는 메서드
    public GameObject FindNearestEnemy()
    {
        // Enemy 레이어에 있는 모든 콜라이더 검색
        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius, LayerMask.GetMask("Enemy")); 
        
        if (colliders.Length == 0)// 적이 없으면
            return null;
            
        GameObject nearestEnemy = null; // 가장 가까운 적
        float nearestDistance = float.MaxValue; // 가장 가까운 거리
        
        foreach (Collider col in colliders) // 모든 적에 대해
        {
            float distance = Vector3.Distance(transform.position, col.transform.position); // 거리 계산
            if (distance < nearestDistance) // 가장 가까운 적 갱신
            {
                nearestDistance = distance;
                nearestEnemy = col.gameObject;
            }
        }
        
        return nearestEnemy; // 가장 가까운 적 반환
    }
    
    // 타겟 설정 메서드
    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }
    
    // 상태 변경 메서드
    public void ChangeState(IState newState)
    {
        _stateMachine.ChangeState(newState);
    }

    public void PerformAttack() // 공격 실행
    {
        Debug.Log($"{gameObject.name}이(가) 공격!");
        
        if (target != null && target.TryGetComponent<StatsComponent>(out var targetStats)) // 타겟이 StatsComponent를 가지고 있다면
        {
            float damage = statsComponent.CalculateDamage(); // 데미지 계산
            targetStats.TakeDamage(damage); // 데미지 적용
        }
    }
   
    // StartCoroutine을 사용할 수 있도록 추가
    public Coroutine StartAttackCoroutine(IEnumerator routine)
    {
        return StartCoroutine(routine);
    }
    
    IEnumerator CombatRoutine() // 자동전투 코루틴
    {
        while (true)
        {
            // FSM 업데이트는 매 프레임
            _stateMachine.Update();
            
            // 탐색은 더 긴 간격으로
            if (_stateMachine.CurrentState is PlayerIdleState)
            {
                yield return new WaitForSeconds(1f); // 1초 대기
            }
            else
            {
                yield return null; // 다음 프레임까지 대기
            }
        }
    }
    
    // 공격 코루틴 예시
    IEnumerator AttackRoutine()
    {
        while (target != null)
        {
            PerformAttack(); // 공격 실행
            yield return new WaitForSeconds(1f / attackSpeed); // 공격 속도에 따른 대기
        }
    }
}