using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public Vector3 positionOffset = Vector3.back * 7.5f;
    public Quaternion rotationOffset = Quaternion.identity;

    public bool followPosition = true;
    public bool followRotation = true;

    public bool rotatePositionOffset = false;

    void LateUpdate()
    {
        SetPositionAndRotation();
    }

    void SetPositionAndRotation()
    {
        if (followPosition)
        {
            transform.position = target.position + (rotatePositionOffset ? rotationOffset * positionOffset : positionOffset);
        }
        if (followRotation)
        {
            transform.rotation = target.rotation * rotationOffset;
        }
    }
}
