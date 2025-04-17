using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable, IMovable
{
    public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }
    public Rigidbody RB { get; set; }
    [field: SerializeField]
    public Bullet BulletPrefab { get; private set; }

    public Transform playerTarget;

    #region State Machine Variables

    public StateMachine<Enemy> StateMachine { get; set; }
    public State<Enemy> IdleState { get; set; }
    public State<Enemy> ChaseState { get; set; }
    public State<Enemy> AttackState { get; set; }
    public bool IsAggroed { get; set; }
    public bool IsWithinStrikingDistance { get; set; }

    #endregion

    #region ScriptableObject Variables

    [SerializeField] EnemyIdleSOBase enemyIdleBase;
    [SerializeField] EnemyChaseSOBase enemyChaseBase;
    [SerializeField] EnemyAttackSOBase enemyAttackBase;

    public EnemyIdleSOBase EnemyIdleBaseInstance { get; private set; }
    public EnemyChaseSOBase EnemyChaseBaseInstance { get; private set; }
    public EnemyAttackSOBase EnemyAttackBaseInstance { get; private set; }

    #endregion

    void Awake()
    {
        EnemyIdleBaseInstance = Instantiate(enemyIdleBase);
        EnemyChaseBaseInstance = Instantiate(enemyChaseBase);
        EnemyAttackBaseInstance = Instantiate(enemyAttackBase);

        StateMachine = new StateMachine<Enemy>();

        IdleState = new State<Enemy>(this, StateMachine, EnemyIdleBaseInstance);
        ChaseState = new State<Enemy>(this, StateMachine, EnemyChaseBaseInstance);
        AttackState = new State<Enemy>(this, StateMachine, EnemyAttackBaseInstance);

        playerTarget = GameObject.FindWithTag("Player").transform;
    }

    void Start()
    {
        CurrentHealth = MaxHealth;
        RB = GetComponent<Rigidbody>();

        StateMachine.Initialize(IdleState);

        EnemyIdleBaseInstance.Initialize(this);
        EnemyChaseBaseInstance.Initialize(this);
        EnemyAttackBaseInstance.Initialize(this);
    }

    void Update()
    {
        StateMachine.CurrentState.FrameUpdate();
    }

    void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    #region Health / Die Functions
    public void Damage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {

    }
    #endregion

    #region Movement / Rotation Functions
    public void Move(Vector3 velocity)
    {
        RB.linearVelocity = velocity;
    }
    #endregion
}
