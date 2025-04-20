using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, IDamagable, IMovable, IGroundable
{
    [SerializeField] float movementSpeed = 5f;
    public Vector2 MovementInput { get; private set; }

    public Animator Anim { get; private set; }

    #region Health Variables

    public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }

    #endregion

    #region Physics Variables

    public Rigidbody RB { get; set; }
    public bool IsGrounded { get; set; }

    #endregion

    #region State Machine Variables

    public StateMachine<Player> StateMachine { get; private set; }

    public State<Player> IdleState { get; private set; }
    public State<Player> MoveState { get; private set; }
    public State<Player> JumpState { get; private set; }
    public State<Player> FallState { get; private set; }

    #endregion

    #region ScriptableObject Variables

    [SerializeField] PlayerIdleSOBase playerIdleBase;
    [SerializeField] PlayerMoveSOBase playerMoveBase;
    [SerializeField] PlayerJumpSOBase playerJumpBase;
    [SerializeField] PlayerFallSOBase playerFallBase;

    public PlayerIdleSOBase PlayerIdleBaseInstance { get; private set; }
    public PlayerMoveSOBase PlayerMoveBaseInstance { get; private set; }
    public PlayerJumpSOBase PlayerJumpBaseInstance { get; private set; }
    public PlayerFallSOBase PlayerFallBaseInstance { get; private set; }

    #endregion

    void Awake()
    {
        PlayerIdleBaseInstance = Instantiate(playerIdleBase);
        PlayerMoveBaseInstance = Instantiate(playerMoveBase);
        PlayerJumpBaseInstance = Instantiate(playerJumpBase);
        PlayerFallBaseInstance = Instantiate(playerFallBase);

        IdleState = new State<Player>(this, PlayerIdleBaseInstance);
        MoveState = new State<Player>(this, PlayerMoveBaseInstance);
        JumpState = new State<Player>(this, PlayerJumpBaseInstance);
        FallState = new State<Player>(this, PlayerFallBaseInstance);

        StateMachine = new StateMachine<Player>();
    }

    void Start()
    {
        RB = GetComponent<Rigidbody>();
        Anim = GetComponentInChildren<Animator>();
        CurrentHealth = MaxHealth;

        PlayerIdleBaseInstance.Initialize(this);
        PlayerMoveBaseInstance.Initialize(this);
        PlayerJumpBaseInstance.Initialize(this);
        PlayerFallBaseInstance.Initialize(this);

        StateMachine.Initialize(IdleState);
    }

    void Update()
    {
        MovementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

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
    public void MoveWithInput()
    {
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        if (movement != Vector3.zero)
        {
            movement.Normalize();
        }
        Move(movement * movementSpeed);
    }
    public void Move(Vector3 velocity)
    {
        RB.linearVelocity = velocity;
    }
    #endregion

    public void CheckForGround()
    {

    }
}
