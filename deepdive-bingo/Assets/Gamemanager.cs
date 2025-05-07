using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public class LocationDetails
{
    public string name;
    public double latitude;
    public double longitude;
    public InfoCards infoCards;
    public List<LocationQuestions> questions;
    public List<string> hints;
}

[System.Serializable]
public class LocationQuestions
{
    public string question;
    public List<string> answer;
}

[System.Serializable]
public class InfoCards
{
    public GameObject card;
    public bool obtained;
}
public class Gamemanager : MonoBehaviour
{
    public List<GameObject> minigames = new();
    public List<LocationDetails> locations = new();
    public double triggerRadius = 10.0;
    public tileloader tileloader;
    public GameObject notificationBox;
    public GameObject UICanvas;
    public GameObject infoPanel;
    public hints hintScript;
    [HideInInspector] public List<LocationDetails> completedLocations = new();
    [HideInInspector] public List<LocationDetails> uncompletedLocations = new();
    bool startedMinigame = false;
    
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
        LocationDetails currentLocation = uncompletedLocations[0];
        double distance = GeoUtils.GetDistanceMeters(lat, lon, currentLocation.latitude, currentLocation.longitude);

        Debug.Log($"Latitude: {lat}\nLongitude: {lon}");
        Debug.Log($"Distance to {currentLocation.name}: {distance} meters");

        if (distance < triggerRadius)
        {
            // Trigger pop up
            CompleteLocation();
        }
    }

    public void CompleteLocation()
    {
        if (completedLocations.Count > 0 && !completedLocations[^1].infoCards.obtained) return;
        completedLocations.Add(uncompletedLocations[0]);
        uncompletedLocations.RemoveAt(0);
        notificationBox.SetActive(true);
        Debug.Log("You reached the location!");
        if (uncompletedLocations.Count == 1)
        {
            Debug.Log("Completed Bingo!");
        }
        else if (uncompletedLocations.Count == 0)
        {
            Debug.Log("Already Completed the bingo");
        }
    }

    public void StartMinigame()
    {
        if (!completedLocations[^1].infoCards.obtained && !startedMinigame)
        {
            // Start the minigame
            Debug.Log("Starting Minigame");
            startedMinigame = true;
            GameObject.Instantiate(minigames[Random.Range(0, minigames.Count)], UICanvas.GetComponent<RectTransform>());
        }
        else
        {
            // Remove the notification box cause why is it there???
            //notificationBox.SetActive(false);
            //completedLocations[^1].infoCards.obtained = true;
            //startedMinigame = false;
            CompleteMinigame();
        }
    }

    public void CompleteMinigame()
    {
        notificationBox.SetActive(false);
        completedLocations[^1].infoCards.obtained = true;
        if (infoPanel.transform.childCount > 0) GameObject.Destroy(infoPanel.transform.GetChild(0).gameObject);
        GameObject.Instantiate(completedLocations[^1].infoCards.card, infoPanel.transform);
        startedMinigame = false;
        hintScript.nextlocation();
        hintScript.UpdateHints();
        Debug.Log("Completed Minigame");
    }

    IEnumerator CheckDistance()
    {
        yield return new WaitForEndOfFrame();
        while (true)
        {
            double newLat = tileloader.latitude;
            double newLon = tileloader.longitude;
            CheckDistanceToPOI(newLat, newLon);
            yield return new WaitForSeconds(5);
        }
    }
}
