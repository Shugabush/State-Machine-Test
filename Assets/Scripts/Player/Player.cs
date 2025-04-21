using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class Player : MonoBehaviour, IDamagable, IMovable, IGroundable<CapsuleCollider>
{
    [SerializeField] float movementSpeed = 5f;
    public Vector2 MovementInput { get; private set; }
    [SerializeField] float rotationSpeed = 15f;
    public Quaternion TargetRotation { get; private set; }

    public Animator Anim { get; private set; }

    #region Health Variables

    public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }

    #endregion

    #region Physics Variables

    public CapsuleCollider Collider { get; set; }
    public Vector3 ColliderDirection
    {
        get
        {
            switch (Collider.direction)
            {
                case 0:
                    return Vector3.right;
                case 1:
                    return Vector3.up;
                case 2:
                    return Vector3.forward;
                default:
                    return Vector3.zero;
            }
        }
    }
    public Rigidbody RB { get; set; }
    public Vector3 Movement { get; private set; }
    public Vector3 Velocity { get; private set; }

    public bool IsGrounded { get; set; }
    public float DistanceToGround { get; set; }
    public Vector3 GroundPlaneNormal { get; set; }
    public Vector3 Gravity { get; set; }

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

        Gravity = Physics.gravity;

        TargetRotation = transform.rotation;
    }

    void Start()
    {
        RB = GetComponent<Rigidbody>();
        Collider = GetComponent<CapsuleCollider>();
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
        CheckForGround();

        if (IsGrounded)
        {
            Velocity = Vector3.zero;
        }
        else
        {
            Velocity += Gravity * Time.deltaTime;
        }

        MovementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (MovementInput != Vector2.zero)
        {
            MovementInput.Normalize();
        }

        Movement = CameraRotationController.YawRotation * new Vector3(MovementInput.x, 0f, MovementInput.y);

        Move(Movement * movementSpeed);

        Rotate(Movement);

        StateMachine.CurrentState.FrameUpdate();

        RB.rotation = Quaternion.Lerp(RB.rotation, TargetRotation, rotationSpeed * Time.deltaTime);
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

    public void Move(Vector3 movement)
    {
        RB.linearVelocity = movement + Velocity;
    }

    public void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            TargetRotation = Quaternion.LookRotation(direction, -Gravity);
        }
    }

    #endregion

    #region Ground Functions

    public void CheckForGround()
    {
        const float maxDstThreshold = 0.05f;

        Vector3 point1 = transform.position + Collider.center + (ColliderDirection * (Collider.height * 0.5f - Collider.radius));
        Vector3 point2 = transform.position + Collider.center - (ColliderDirection * (Collider.height * 0.5f - Collider.radius));

        // Make this GameObject ignore the raycast
        var originalLayer = gameObject.layer;
        gameObject.layer = 2;

        Vector3 rayDir = Gravity.normalized;

        if (Physics.CapsuleCast(point1, point2, Collider.radius, rayDir, out var hit, Mathf.Infinity, ~2))
        {
            DistanceToGround = hit.distance;

            if (hit.distance < maxDstThreshold)
            {
                IsGrounded = true;
                GroundPlaneNormal = hit.normal;
            }
            else
            {
                IsGrounded = false;
                GroundPlaneNormal = -rayDir;
            }
        }
        else
        {
            IsGrounded = false;
        }

        // Revert this GameObject to its original layer
        gameObject.layer = originalLayer;
    }

    #endregion

    public Vector3 ProjectOnGroundPlane(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, GroundPlaneNormal);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // We need the collider
        if (Collider == null)
        {
            Collider = GetComponent<CapsuleCollider>();
        }

        Vector3 point1 = transform.position + Collider.center + (ColliderDirection * (Collider.height * 0.5f - Collider.radius));
        Vector3 point2 = transform.position + Collider.center - (ColliderDirection * (Collider.height * 0.5f - Collider.radius));

        Gizmos.DrawLine(point1, point2);
    }
}
