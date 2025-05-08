using UnityEngine;
using TMPro;

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
        }
        else
        {
            Debug.Log("wrong");
        }
    }
}