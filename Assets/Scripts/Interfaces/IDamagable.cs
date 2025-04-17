public interface IDamagable
{
    void Damage(float damage);
    void Die();

    float MaxHealth { get; set; }
    float CurrentHealth { get; set; }
}
