using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Vector3 lookAxis = Vector3.up;
    [SerializeField] float damage = 1f;
    
    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        lookAxis.Normalize();
    }

    void Update()
    {
        
    }

    public void Fire(Vector3 velocity)
    {
        rb.linearVelocity = velocity;
        rb.rotation = Quaternion.FromToRotation(lookAxis, velocity);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.Damage(damage);
            Destroy(gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.TransformDirection(lookAxis.normalized * 2f));
    }
}
