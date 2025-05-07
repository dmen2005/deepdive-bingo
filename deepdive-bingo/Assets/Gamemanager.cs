using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public class LocationDetails
{
    public string name;
    public double latitude;
    public double longitude;
    public List<LocationQuestions> questions;
    public List<string> hints;
}

[System.Serializable]
public class LocationQuestions
{
    public string question;
    public string answer;
}
public class Gamemanager : MonoBehaviour
{
    public List<LocationDetails> locations = new();
    public double triggerRadius = 10.0;
    public tileloader tileloader;
    public GameObject notificationBox;
    [HideInInspector] public List<LocationDetails> completedLocations = new();
    [HideInInspector] public List<LocationDetails> uncompletedLocations = new();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uncompletedLocations = new(locations);

        StartCoroutine(CheckDistance());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckDistanceToPOI(double lat, double lon)
    {
        if (uncompletedLocations.Count <= 0) return;
        Debug.Log(lat);
        LocationDetails currentLocation = uncompletedLocations[0];
        double distance = GeoUtils.GetDistanceMeters(lat, lon, currentLocation.latitude, currentLocation.longitude);

        Debug.Log($"Latitude: {lat}\nLongitude: {lon}");
        Debug.Log($"Distance to {currentLocation.name}: {distance} meters");

        if (distance < triggerRadius)
        {
            // Trigger pop up
            Debug.Log("You reached the location!");
            CompleteLocation();
        }
    }

    void CompleteLocation()
    {
        completedLocations.Add(uncompletedLocations[0]);
        uncompletedLocations.RemoveAt(0);
        if (uncompletedLocations.Count == 1)
        {
            completedLocations.Add(uncompletedLocations[0]);
            uncompletedLocations.RemoveAt(0);

            Debug.Log("Completed Bingo!");
        }
        else if (uncompletedLocations.Count == 0)
        {
            Debug.Log("Already Completed the bingo");
        }
    }

    public void StartMinigame()
    {

    }

    IEnumerator CheckDistance()
    {
        yield return new WaitForEndOfFrame();
        while (true)
        {
            double newLat = tileloader.latitude;
            double newLon = tileloader.longitude;
            Debug.Log(newLat);
            Debug.Log(uncompletedLocations[0].latitude);
            CheckDistanceToPOI(newLat, newLon);
            yield return new WaitForSeconds(5);
        }
    }
}
