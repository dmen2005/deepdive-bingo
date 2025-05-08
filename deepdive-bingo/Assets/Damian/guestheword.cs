using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class guestheword : MonoBehaviour
{
    public GameObject guestheword2;
    Gamemanager gamemanager;
    public TMP_InputField playerInput;

    void Start()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<Gamemanager>();
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
            gamemanager.CompleteMinigame();
            GameObject.Destroy(guestheword2);
            playerInput.GetComponent<Image>().color = Color.green;
            StartCoroutine(white());
        }
        else
        {
            playerInput.GetComponent<Image>().color = Color.red;
            StartCoroutine(white());
            //wrong
        }

        IEnumerator white()
        {
            yield return new WaitForSeconds(1);
            playerInput.GetComponent<Image>().color = Color.white;
        }
    }
}