using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, IDamagable, IMovable
{
    [SerializeField] float movementSpeed = 5f;

    public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }
    public Rigidbody RB { get; set; }

    void Start()
    {
        RB = GetComponent<Rigidbody>();
        CurrentHealth = MaxHealth;
    }

    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        Move(movement * movementSpeed);
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
