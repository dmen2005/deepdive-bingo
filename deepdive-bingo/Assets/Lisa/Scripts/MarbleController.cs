using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;

public class MarbleController : MonoBehaviour
{
    public Gyroscope gyro = new Gyroscope();

    private void Awake()
    {
        if (gyro != null)
        {
            InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log($"{gyro.angularVelocity.ReadValue()} gyroscope value");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collided with {other.gameObject.name}");
    }
}
