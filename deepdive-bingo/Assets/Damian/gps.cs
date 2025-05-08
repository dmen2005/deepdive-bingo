using UnityEngine;
using System.Collections;


public class gps : MonoBehaviour
{
    public tileloader tileloader;
    public float update = 10f;

    public double lattest = 53.2132367;
    public double longtest = 6.5558798;

    //all the comments is for when gps is working to lazzy for cleaner solotion rn

    void Awake()
    {
        StartCoroutine(StartLocationService());
        InvokeRepeating("UpdateGPS", 0f, update);
    }

    private void Update()
    {
        
    }

    IEnumerator StartLocationService()
    {
        while (true)
        {
            if (Input.location.isEnabledByUser && Input.location.status != LocationServiceStatus.Running)
            {
                //Debug.LogError("Location services are not enabled on this device.");
                Input.location.Start();
                yield break;
            }
            yield return new WaitForSeconds(5);
        }
        
    }

    void UpdateGPS()
    {
        Debug.Log("Test");
        if (Input.location.status == LocationServiceStatus.Running)
        {
            double latitude = Input.location.lastData.latitude;
            double longitude = Input.location.lastData.longitude;
            Debug.Log(Input.location.lastData);

            tileloader.latitude = latitude;
            tileloader.longitude = longitude;

            tileloader.location();
        }
        else
        {
            double latitude = lattest;
            double longitude = longtest;

            tileloader.latitude = latitude;
            tileloader.longitude = longitude;

            tileloader.location();
            Debug.LogWarning("Unable to get GPS data.");
        }
    }

    void OnDisable()
    {
        if (Input.location.isEnabledByUser)
        {
            Input.location.Stop();
        }
    }
}
/*
 todo
 
 Android:
Go to Edit > Project Settings > Player.

Select Android.

Under Other Settings, scroll to Identification.

Make sure you have Location permission enabled.

iOS:
Under Player Settings > iOS, ensure that you have set the required location permissions in the Info.plist file.
 */