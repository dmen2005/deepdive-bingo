using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

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
            playerInput.GetComponent<Image>().color = Color.green;
            StartCoroutine(white());
        }
        else
        {
            playerInput.GetComponent<Image>().color = Color.red;
            StartCoroutine(white());
            //wrong
        }
    }

    IEnumerator white()
    {
        yield return new WaitForSeconds(1);
        playerInput.GetComponent<Image>().color = Color.white;
    }
}