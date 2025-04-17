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
    public EnemyIdleState IdleState { get; set; }
    public EnemyChaseState ChaseState { get; set; }
    public EnemyAttackState AttackState { get; set; }
    public bool IsAggroed { get; set; }
    public bool IsWithinStrikingDistance { get; set; }

    #endregion

    #region Idle Variables

    public float randomMovementRange = 1f;
    public float randomMovementSpeed = 1f;

    #endregion

    void Awake()
    {
        StateMachine = new StateMachine<Enemy>();

        IdleState = new EnemyIdleState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);

        playerTarget = GameObject.FindWithTag("Player").transform;
    }

    void Start()
    {
        CurrentHealth = MaxHealth;
        RB = GetComponent<Rigidbody>();

        StateMachine.Initialize(IdleState);
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
