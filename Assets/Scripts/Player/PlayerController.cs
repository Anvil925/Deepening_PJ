using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private StateMachine _stateMachine; // 상태 머신
    private Coroutine _combatCoroutine; // 전투 코루틴
    public GameObject target; // 타겟
    public float searchRadius = 30f; // 탐색 반경
    [SerializeField] private PlayerStats stats = new PlayerStats();
    private StatsComponent statsComponent;
    private NavMeshAgent _navMeshAgent;
    public float Health
    {
        get => statsComponent.baseStats.currentHealth;
        set => statsComponent.baseStats.currentHealth = value;
    }

    public float AttackPower
    {
        get => statsComponent.baseStats.attackPower;
        set => statsComponent.baseStats.attackPower = value;
    }

    public float Defense
    {
        get => statsComponent.baseStats.defense;
        set => statsComponent.baseStats.defense = value;
    }

    public float AttackSpeed
    {
        get => statsComponent.baseStats.attackSpeed;
        set => statsComponent.baseStats.attackSpeed = value;
    }

    public float MoveSpeed
    {
        get => statsComponent.baseStats.moveSpeed;
        set 
        {
            statsComponent.baseStats.moveSpeed = value;
            // MoveSpeed가 변경될 때 NavMeshAgent 속도도 함께 업데이트
            UpdateNavMeshAgentSpeed();
        }
    }

    public float CritChance
    {
        get => statsComponent.baseStats.critChance;
        set => statsComponent.baseStats.critChance = value;
    }

    public float CritDamage
    {
        get => statsComponent.baseStats.critDamage;
        set => statsComponent.baseStats.critDamage = value;
    }

    public float LifeSteal
    {
        get => statsComponent.baseStats.lifeSteal;
        set => statsComponent.baseStats.lifeSteal = value;
    }

    public int Level
    {
        get => statsComponent.baseStats.level;
        set => statsComponent.baseStats.level = value;
    }

    public float ExperiencePoints
    {
        get => statsComponent.baseStats.experiencePoints;
        set => statsComponent.baseStats.experiencePoints = value;
    }

    public float ExperienceToNextLevel
    {
        get => statsComponent.baseStats.experienceToNextLevel;
        set => statsComponent.baseStats.experienceToNextLevel = value;
    }
    private void Awake()
    {
        statsComponent = GetComponent<StatsComponent>(); // StatsComponent 컴포넌트 가져오기
        if (statsComponent == null)
        {
            statsComponent = gameObject.AddComponent<StatsComponent>();
        }

        if (statsComponent != null)
        {
            statsComponent.baseStats = stats; // 기본 스탯 설정
            statsComponent.baseStats.Initialize(); // 초기화
        }

        _navMeshAgent = GetComponent<NavMeshAgent>();
        if (_navMeshAgent == null)
        {
            _navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        }

        // NavMeshAgent 설정
        _navMeshAgent.angularSpeed = 360f;
        _navMeshAgent.stoppingDistance = 1.5f; // 공격 범위보다 약간 작게
    }

    void Start()
    {
        _stateMachine = new StateMachine(); // 상태 머신 초기화
        _stateMachine.ChangeState(new PlayerIdleState(this)); // 초기 상태 설정

        // 자동전투 코루틴 시작
        _combatCoroutine = StartCoroutine(CombatRoutine());
        UpdateNavMeshAgentSpeed(); // NavMeshAgent 속도 업데이트

        if (statsComponent != null)
        {
            Debug.Log($"플레이어 체력: {statsComponent.baseStats.currentHealth}");
        }
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
            yield return new WaitForSeconds(1f / statsComponent.baseStats.attackSpeed); // 공격 속도에 따른 대기
        }
    }

    public void MoveToTarget(Vector3 targetPosition)
    {
        if (_navMeshAgent != null && _navMeshAgent.isActiveAndEnabled)
        {
            Debug.Log($"목적지로 이동: {targetPosition}");
            _navMeshAgent.SetDestination(targetPosition);
        }
    }

    public void StopMoving()
    {
        if (_navMeshAgent != null && _navMeshAgent.isActiveAndEnabled)
        {
            _navMeshAgent.isStopped = true;
            _navMeshAgent.ResetPath();
        }
    }

    // 새로운 메서드 추가 - NavMeshAgent 속도 업데이트
    private void UpdateNavMeshAgentSpeed()
    {
        if (_navMeshAgent != null && _navMeshAgent.isActiveAndEnabled)
        {
            _navMeshAgent.speed = MoveSpeed;
            Debug.Log($"이동 속도 업데이트: {MoveSpeed}");
        }
    }
}