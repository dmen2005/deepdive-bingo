using System.Globalization;
using UnityEngine;

public class find_the: MonoBehaviour
{
    public GameObject findthemistake;
    public int objfound = 0;
    public void GoSearch()
    {
        findthemistake.SetActive(true);
    }
    public void found()
    {
        objfound++;

        if (objfound == 3)
        {
            objfound = 0;
            findthemistake.SetActive(false);
        }
    }
}
