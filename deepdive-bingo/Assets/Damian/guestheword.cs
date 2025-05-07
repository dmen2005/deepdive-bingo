using UnityEngine;
using UnityEngine.InputSystem;

public class guestheword : MonoBehaviour
{
    public GameObject guestheword2;
    public GameObject playerInput;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void gues()
    {
        guestheword2.SetActive(true);
    }

    public void uguesed()
    {
        string correctAnswer = noorderpoort
        string userAnswer = playerInput.text.Trim().ToLower();

        if (userAnswer == noorderpoort) ;
        {
            guestheword2.SetActive (false);
        }
    }
}
