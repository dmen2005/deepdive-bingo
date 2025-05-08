using UnityEngine;

public class GyroPlaneTilt : MonoBehaviour
{
    public float maxTiltAngle = 15f;     // Max tilt in degrees
    public float tiltSpeed = 5f;         // How fast the plane tilts

    private Quaternion initialRotation;
    private bool gyroAvailable;

    void Start()
    {
        gyroAvailable = SystemInfo.supportsGyroscope;

        if (gyroAvailable)
        {
            Input.gyro.enabled = true;
        }

        // Store the original orientation
        initialRotation = transform.rotation;
    }

    void Update()
    {
        if (!gyroAvailable)
        {
            return;
        }

        // Get the device rotation in space
        Quaternion deviceRotation = Input.gyro.attitude;

        // Adjust for Unity's coordinate system
        deviceRotation = Quaternion.Euler(90f, 0f, 0f) * (new Quaternion(-deviceRotation.x, -deviceRotation.y, deviceRotation.z, deviceRotation.w));

        // Extract the euler angles from the rotation
        Vector3 targetEuler = deviceRotation.eulerAngles;

        // Normalize angles to -180 to 180
        targetEuler.x = NormalizeAngle(targetEuler.x);
        targetEuler.z = NormalizeAngle(targetEuler.z);

        // Clamp to max tilt angle
        float clampedX = Mathf.Clamp(targetEuler.x, -maxTiltAngle, maxTiltAngle);
        float clampedZ = Mathf.Clamp(targetEuler.z, -maxTiltAngle, maxTiltAngle);

        // Create new target rotation from clamped values
        Quaternion targetRotation = Quaternion.Euler(clampedX, 0f, clampedZ);

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * tiltSpeed);
    }

    float NormalizeAngle(float angle)
    {
        if (angle > 180f) angle -= 360f;
        return angle;
    }
}
