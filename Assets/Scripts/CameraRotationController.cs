using UnityEngine;

public class CameraRotationController : MonoBehaviour
{
    // Assign in the inspector
    [SerializeField] FollowTarget controllerFollower;
    [SerializeField] FollowTarget cameraFollower;

    float pitch = 0f;
    float yaw = 0f;

    [Range(0f, 180f)]
    [SerializeField] float pitchLimit = 45f;
    [Range(0f, 180f)]
    [SerializeField] float yawLimit = 180f;

    public static Quaternion PitchRotation { get; private set; } = Quaternion.identity;
    public static Quaternion YawRotation { get; private set; } = Quaternion.identity;
    public static Quaternion InputRotation { get; private set; } = Quaternion.identity;

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Awake()
    {
        // This script will call SetPositionAndRotation() on the followers
        controllerFollower.autoTransform = false;
        cameraFollower.autoTransform = false;
    }

    void OnDestroy()
    {
        // Reset static properties
        PitchRotation = Quaternion.identity;
        YawRotation = Quaternion.identity;
        InputRotation = Quaternion.identity;
    }

    void Update()
    {
        pitch += Input.GetAxisRaw("Mouse Y");
        yaw += Input.GetAxisRaw("Mouse X");

        // Set quaternion rotations here instead of returning the values when they get called
        // just to make sure the calculations only have to run once per frame
        PitchRotation = Quaternion.AngleAxis(pitch, Vector3.right);
        YawRotation = Quaternion.AngleAxis(yaw, Vector3.up);
        InputRotation = YawRotation * PitchRotation;

        transform.rotation = InputRotation;

        cameraFollower.positionOffsetRotation = InputRotation;
    }

    void LateUpdate()
    {
        controllerFollower.SetPositionAndRotation();
        cameraFollower.SetPositionAndRotation();
    }
}
