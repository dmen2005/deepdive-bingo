using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;

public class MarbleController : MonoBehaviour
{
    private Vector3 rotation; 

    private void Awake()
    {
        Input.gyro.enabled = true;
        rotation = Vector3.zero;
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = GyroToUnity(Input.gyro.attitude);
    }

    private Quaternion GyroToUnity (Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collided with {other.gameObject.name}");
    }
}
