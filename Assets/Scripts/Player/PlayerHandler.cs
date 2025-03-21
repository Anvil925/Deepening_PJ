using System;
using UnityEngine;

namespace DeepeningPJ
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerHandler : MonoBehaviour, IAttackable, IDamageable // PlayerHandler 클래스가 IAttackable, IDamageable 인터페이스를 상속받음
    {
        [field: Header("References")]
        [field: SerializeField] public PlayerSO Data { get; private set; }

        [field: SerializeField] private PlayerStat stat = new PlayerStat();

        public WeaponHandler equipWeapon;

        [field: Header("Collisions")]
        [field: SerializeField] public CharacterCapsuleColliderUtility ColliderUtility { get; protected set; }
        [field: SerializeField] public PlayerLayerData LayerData { get; private set; }

        [field: Header("Camera")]
        [field: SerializeField] public PlayerCameraUtility CameraUtility { get; private set; }

        [field: Header("Animations")]
        [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

        public Rigidbody Rigidbody { get; private set; }
        public Animator Animator { get; private set; }
        public Inventory Inventory { get; private set; }

        public Transform MainCameraTransform { get; private set; }
        public PlayerInput Input {  get; private set; }

        public event Action OnPlayerDamaged;

        private PlayerStateMachine movementStateMachine;

        private void Awake()
        {
            equipWeapon.owner = this;

            Animator = GetComponentInChildren<Animator>();
            Rigidbody = GetComponent<Rigidbody>();
            Input = GetComponent<PlayerInput>();
            Inventory = GetComponent<Inventory>();

            ColliderUtility.Initialize(gameObject);
            ColliderUtility.CalculateCapsuleColliderDimensions();
            CameraUtility.Initialize();
            AnimationData.Initialize(Animator);

            MainCameraTransform = Camera.main.transform;

            movementStateMachine = new PlayerStateMachine(this);

            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnValidate()
        {
            ColliderUtility.Initialize(gameObject);
            ColliderUtility.CalculateCapsuleColliderDimensions();
        }

        private void Start()
        {
            movementStateMachine.ChangeState(movementStateMachine.IdlingState);
        }

        private void OnTriggerEnter(Collider collider)
        {
            movementStateMachine.OnTriggerEnter(collider);
        }

        private void OnTriggerExit(Collider collider)
        {
            movementStateMachine.OnTriggerExit(collider);
        }

        private void Update()
        {
            movementStateMachine.HandleInput();
            movementStateMachine.Update();
        }

        private void FixedUpdate()
        {
            movementStateMachine.FixedUpdate();
        }

        public void OnMovementStateAnimationEnterEvent()
        {
            movementStateMachine.OnAnimationEnterEvent();
        }

        public void OnMovementStateAnimationExitEvent()
        {
            movementStateMachine.OnAnimationExitEvent();
        }

        public void OnMovementStateAnimationTransitionEvent()
        {
            movementStateMachine.OnAnimationTransitionEvent();
        }

        #region Stat Value
        public float MaxHp => stat.m_maxHp;
        public float MaxShild => stat.m_maxShild;
        public float MaxHungerPoint => stat.m_maxHungerPoint;
        public float MaxStemina => stat.m_maxStemina;
        public float Hp => stat.m_hp;
        public float Shild => stat.m_shild;
        public float HungerPoint => stat.m_hungerPoint;
        public float Stemina => stat.m_stemina;
        public float Def => stat.m_def;
        public float Atk => stat.m_atk;
        public float AtkSpeed => stat.m_atkSpeed;
        public float Critical => stat.m_critical;
        #endregion

        #region Stat Methods
        public void SetMaxHp(float amount) => stat.SetMaxHp(amount);
        public void SetMaxShild(float amount) => stat.SetMaxShild(amount);
        public void SetMaxHungerPoint(float amount) => stat.SetMaxHungerPoint(amount);
        public void SetMaxStemina(float amount) => stat.SetMaxStemina(amount);
        public void SetDef(float amount) => stat.SetDef(amount);
        public void SetAtk(float amount) => stat.SetAtk(amount);
        public void SetAtkSpeed(float amount) => stat.SetAtkSpeed(amount);
        public void SetCritical(float amount) => stat.SetCritical(amount);
        public void AddHp(float amount) => stat.AddHp(amount);
        public void AddShild(float amount) => stat.AddShild(amount);
        public void AddHungerPoint(float amount) => stat.AddHungerPoint(amount);
        public void AddStemina(float amount) => stat.AddStemina(amount);
        public void SubHp(float amount) => stat.SubHp(amount);
        public void SubShild(float amount) => stat.SubShild(amount);
        public void SubHungerPoint(float amount) => stat.SubHungerPoint(amount);
        public void SubStemina(float amount) => stat.SubStemina(amount);
        #endregion

        #region IAttackable Methods
        public float CalculateDamage()
        {
            float calculate = Atk + equipWeapon.atk;

            float random = UnityEngine.Random.Range(0f, 100f);

            if (random <= Critical)
            {
                calculate *= 2;
            }

            return calculate;
        }
        #endregion

        #region IDamageable Methods
        public float TakeDamage(float amount, int weaponRate, int weaponType)
        {
            if (amount < stat.m_def) return 0;

            float calculate = amount - stat.m_def;

            TakeTrueDamage(calculate);

            return calculate;
        }

        public float TakeTrueDamage(float amount)
        {
            if (amount <= 0) return 0;

            stat.m_hp -= stat.SubShild(amount);
            if (stat.m_hp < 0) stat.m_hp = 0;

            OnPlayerDamaged?.Invoke();

            return amount;
        }
        #endregion
    }
}
