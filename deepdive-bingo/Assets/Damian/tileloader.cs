using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
using static UnityEditor.FilePathAttribute;


public class tileloader : MonoBehaviour
{
    public Renderer tileRenderer;
    public int zoom = 15;
    public double latitude = 53.2194;
    public double longitude = 6.5665;

    public void location()
    {
        int x = LonToTileX(longitude, zoom);
        int y = LatToTileY(latitude, zoom);

        // use in testing so not get bloked
        //  string url = $"https://a.tile.openstreetmap.fr/osmfr/{zoom}/{x}/{y}.png";

        string url = $"https://tile.openstreetmap.org/{zoom}/{x}/{y}.png";
        // use in final build
        StartCoroutine(LoadTile(url));
    }

    IEnumerator LoadTile(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
            yield return new WaitForSeconds(5);
            StartCoroutine(LoadTile(url));
        }
        else
            tileRenderer.material.mainTexture = DownloadHandlerTexture.GetContent(www);

    }

    int LonToTileX(double lon, int zoom) =>
        (int)((lon + 180.0) / 360.0 * (1 << zoom));

    int LatToTileY(double lat, int zoom)
    {
        double latRad = lat * Math.PI / 180.0;
        return (int)((1.0 - Math.Log(Math.Tan(latRad) + 1.0 / Math.Cos(latRad)) / Math.PI) / 2.0 * (1 << zoom));
    }
}
