using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Vector3 lookAxis = Vector3.up;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lookAxis.Normalize();
    }

    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.TransformPoint(-lookAxis.normalized * 2f), transform.TransformPoint(lookAxis.normalized * 2f));
    }
}
