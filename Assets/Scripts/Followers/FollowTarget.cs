using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public Vector3 positionOffset = Vector3.back * 7.5f;
    public Quaternion positionOffsetRotation = Quaternion.identity;
    public Quaternion rotationOffset = Quaternion.identity;

    public bool followPosition = true;
    public bool followRotation = true;

    [Tooltip("Set this to false if you want another script to call SetPositionAndRotation on this follower")]
    public bool autoTransform = true;

    void LateUpdate()
    {
        if (autoTransform)
        {
            SetPositionAndRotation();
        }
    }

    public void SetPositionAndRotation()
    {
        Vector3 positionOffset = positionOffsetRotation * this.positionOffset;
        if (followPosition)
        {
            transform.position = target.position + positionOffset;
        }
        if (followRotation)
        {
            transform.rotation = target.rotation * rotationOffset;
        }
    }
}
