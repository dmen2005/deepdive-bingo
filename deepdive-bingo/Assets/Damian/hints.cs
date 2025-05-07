using UnityEngine;
using TMPro;
using System.Collections.Generic;


public class hints : MonoBehaviour
{
    public Gamemanager gameManager;
    public TMP_Text questionText;
    public TMP_Text hint1;
    public TMP_Text hint2;
    public TMP_Text hint3;
    public TMP_InputField playerInput;

    private int currentQuestionIndex = 0;


    void Start()
    {
        if (gameManager.completedLocations.Count == 0) return;

        var location = gameManager.completedLocations[0];

        if (location.questions.Count > 0)
        {
            questionText.text = location.questions[currentQuestionIndex].question;
        }

        hint1.text = location.hints.Count > 0 ? location.hints[0] : "";
        hint2.text = location.hints.Count > 1 ? location.hints[1] : "";
        hint3.text = location.hints.Count > 2 ? location.hints[2] : "";
    }



    public void CheckAnswer()
    {
        Debug.Log("damiantest");
        if (gameManager.completedLocations.Count == 0) return;
        Debug.Log("damiantest2");

        var location = gameManager.completedLocations[^1];
        var question = location.questions[currentQuestionIndex];

        string correctAnswer = question.answer.Trim().ToLower();
        string userAnswer = playerInput.text.Trim().ToLower();

        if (userAnswer == correctAnswer)
        {
            Debug.Log("Correct!");

            currentQuestionIndex++;

            if (currentQuestionIndex >= location.questions.Count)
            {
                Debug.Log("Finished all questions for this location.");
            }
            else
            {
                questionText.text = location.questions[currentQuestionIndex].question;
            }
        }
        else
        {
            Debug.Log("Incorrect.");
        }
    }
}
