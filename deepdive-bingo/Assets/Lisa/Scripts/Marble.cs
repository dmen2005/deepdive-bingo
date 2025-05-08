using UnityEngine;

public class Marble : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Endpoint")
        {
            Debug.Log("Completed Level!");
        }
    }
}
