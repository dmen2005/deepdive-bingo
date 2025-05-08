using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
using UnityEngine.Tilemaps;

public class tileloader : MonoBehaviour
{
    public GameObject mapGrid;
    public int zoom = 15;
    [HideInInspector] public double latitude;
    [HideInInspector] public double longitude;

    public void location()
    {
        int x = (int)Math.Round(LonToTileX(longitude, zoom));
        int y = (int)Math.Round(LatToTileY(latitude, zoom));
        // use in testing so not get bloked
        //  string url = $"https://a.tile.openstreetmap.fr/osmfr/{zoom}/{x}/{y}.png";

        // use in final build
        StartCoroutine(RetryLoadDelay(x, y, mapGrid));
    }

    IEnumerator LoadTile(string url, GameObject obj)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogWarning(www.error);
        }
        else
        {
            obj.GetComponent<MeshRenderer>().material.mainTexture = DownloadHandlerTexture.GetContent(www);
        }
    }

    IEnumerator RetryLoadDelay(int x, int y, GameObject obj)
    {
        foreach (Transform child in obj.transform)
        {
            GameObject childObj = child.gameObject;
            int newY = y + NameCheck(childObj.name);

            string url = $"https://tile.openstreetmap.org/{zoom}/{x}/{newY}.png";
            while (true)
            {
                UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogWarning(www.error);
                    yield return new WaitForSeconds(1);
                    continue;
                }
                else
                {
                    childObj.GetComponent<MeshRenderer>().material.mainTexture = DownloadHandlerTexture.GetContent(www);
                    yield return null;
                    break;
                }
            }
        }
        
    }

    int NameCheck(string name)
    {
        if (name == "1")
        {
            return 1;
        }
        else if (name == "0")
        {
            return 0;
        }
        else if (name == "-1")
        {
            return -1;
        }
        return 0;
    }

    double LonToTileX(double lon, int zoom) =>
        (double)((lon + 180.0) / 360.0 * (1 << zoom));

    double LatToTileY(double lat, int zoom)
    {
        double latRad = lat * Math.PI / 180.0;
        return (double)((1.0 - Math.Log(Math.Tan(latRad) + 1.0 / Math.Cos(latRad)) / Math.PI) / 2.0 * (1 << zoom));
    }
}
