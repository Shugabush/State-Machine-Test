using UnityEngine;

public interface IGroundable
{
    Rigidbody RB { get; set; }
    bool IsGrounded { get; set; }
    void CheckForGround();
}
