using UnityEngine;

public interface IGroundable<ICollider> where ICollider : Collider
{
    ICollider Collider { get; set; }
    Rigidbody RB { get; set; }
    bool IsGrounded { get; set; }
    float DistanceToGround { get; set; }
    Vector3 GroundPlaneNormal { get; set; }
    Vector3 Gravity { get; set; }
    void CheckForGround();
    Vector3 ProjectOnGroundPlane(Vector3 direction);
}
