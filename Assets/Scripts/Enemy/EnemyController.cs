using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private StateMachine _stateMachine; // 상태 머신
    public float attackSpeed = 0.8f;  // 플레이어보다 약간 느림
    public GameObject target; // 타겟
    public float searchRadius = 8f; // 탐색 반경

    [SerializeField] private EnemyStats stats = new EnemyStats();
    private StatsComponent statsComponent;

    private void Awake()
    {
        statsComponent = GetComponent<StatsComponent>();
        if (statsComponent == null)
        {
            statsComponent = gameObject.AddComponent<StatsComponent>();
        }
    }

    private void Start()
    {
        _stateMachine = new StateMachine();
        // 기본 상태는 대기 상태
        _stateMachine.ChangeState(new EnemyIdleState(this));

        // 상태머신 업데이트 코루틴 시작
        StartCoroutine(StateMachineRoutine());
    }

    public GameObject FindNearestPlayer()
    {
        // 플레이어 레이어에 있는 모든 콜라이더 검색
        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius, LayerMask.GetMask("Player"));

        if (colliders.Length == 0) // 플레이어가 없으면
            return null;

        GameObject nearestPlayer = null; // 가장 가까운 플레이어
        float nearestDistance = float.MaxValue; // 가장 가까운 거리

        foreach (Collider col in colliders) // 모든 플레이어에 대해
        {
            float distance = Vector3.Distance(transform.position, col.transform.position); // 거리 계산
            if (distance < nearestDistance) // 가장 가까운 플레이어 갱신
            {
                nearestDistance = distance;
                nearestPlayer = col.gameObject; // 가장 가까운 플레이어 설정
            }
        }

        return nearestPlayer; // 가장 가까운 플레이어 반환
    }

    public void PerformAttack()
    {
        // 적의 공격 구현
        Debug.Log($"{gameObject.name}이(가) 공격!");

        if (target != null && target.TryGetComponent<StatsComponent>(out var targetStats))
        {
            float damage = statsComponent.CalculateDamage();
            targetStats.TakeDamage(damage);
        }
    }

    public void SetTarget(GameObject newTarget) // 타겟 설정 메서드
    {
        target = newTarget; // 타겟 설정
    }

    public void ChangeState(IState newState) // 상태 변경 메서드
    {
        _stateMachine.ChangeState(newState); // 상태 변경
    }

    private IEnumerator StateMachineRoutine() // 상태머신 업데이트 코루틴
    {
        while (true) 
        {
            _stateMachine.Update();
            yield return null;
        }
    }
}