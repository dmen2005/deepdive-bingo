using System.Globalization;
using UnityEngine;

public class find_the: MonoBehaviour
{
    public GameObject findthemistake;
    public GameObject butten;
    public GameObject butten1;
    public GameObject butten2;
    public int objfound = 0;
    public void GoSearch()
    {
        butten.SetActive(true);
        butten1.SetActive(true);
        butten2.SetActive(true);

        findthemistake.SetActive(true);
    }
    public void found()
    {
        objfound++;

        if (objfound == 3)
        {
            findthemistake.SetActive(false);
            objfound = 0;
        }
    }
}
