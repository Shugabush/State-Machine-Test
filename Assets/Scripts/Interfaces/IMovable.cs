using UnityEngine;

public interface IMovable
{
    Rigidbody RB { get; set; }

    void Move(Vector3 velocity);
}
