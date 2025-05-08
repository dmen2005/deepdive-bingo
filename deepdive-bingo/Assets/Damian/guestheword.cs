using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class guestheword : MonoBehaviour
{
    public GameObject guestheword2;
    public TMP_InputField playerInput;

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
        string correctAnswer = "noorderpoort";
        string userAnswer = playerInput.text.Trim().ToLower();

        if (userAnswer == correctAnswer)
        {
            //correct
            guestheword2.SetActive(false);
        }
        else
        {
            Debug.Log("wrong");
        }
    }
}