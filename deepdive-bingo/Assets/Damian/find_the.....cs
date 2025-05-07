using UnityEngine;

public class find_the: MonoBehaviour
{
    public GameObject findthemistake;
    public void GoSearch()
    {
        findthemistake.SetActive(true);
    }
    public void found()
    {
        findthemistake.SetActive(false);
    }
}
