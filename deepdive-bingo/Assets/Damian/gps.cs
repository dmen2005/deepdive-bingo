using UnityEngine;
using System.Collections;


public class gps : MonoBehaviour
{
    public tileloader tileloader;
    public float update = 10f;

    public float lattest = 53.2194f;
    public float longtest = 6.5665f;

    //all the comments is for when gps is working to lazzy for cleaner solotion rn

    void Start()
    {

        if (!Input.location.isEnabledByUser)
        {
            Debug.LogError("Location services are not enabled on this device.");
            return;
        }

        Input.location.Start();


        InvokeRepeating("UpdateGPS", 0f, update);
    }

    void UpdateGPS()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            //float latitude = lattest;
            //float longitude = longtest;

            float latitude = Input.location.lastData.latitude;
            float longitude = Input.location.lastData.longitude;



            tileloader.latitude = latitude;
            tileloader.longitude = longitude;

            tileloader.location();
        }
        else
        {
            Debug.LogError("Unable to get GPS data.");
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