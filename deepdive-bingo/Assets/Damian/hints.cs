using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.InputSystem;


public class hints : MonoBehaviour
{
    public Gamemanager gameManager;
    public TMP_Text questionText;
    public TMP_Text hint1;
    public TMP_Text hint2;
    public TMP_Text hint3;
    public TMP_InputField playerInput;

    private int currentQuestionIndex = 0;

    [HideInInspector] public int hintcount = 0;


    public void UpdateHints()
    {
        if (gameManager.completedLocations.Count == 0) return;

        var location = gameManager.completedLocations[^1];

        if (location.questions.Count > 0)
        {
            questionText.text = location.questions[currentQuestionIndex].question;
        }

        hint1.text = hintcount > 0 ? location.hints[0] : "";
        hint2.text = hintcount > 1 ? location.hints[1] : "";
        hint3.text = hintcount > 2 ? location.hints[2] : "";
    }
    void Start()
    {
        
    }

    private void Update()
    {

    }

    public void nextlocation()
    {
        hintcount = 0;
        playerInput.interactable = true;
        hint1.text = "---------------------";
        hint2.text = "---------------------";
        hint3.text = "---------------------";
    }

    public void CheckAnswer()
    {
        if (gameManager.completedLocations.Count == 0) return;

        var location = gameManager.completedLocations[^1];
        var hintsLoc = gameManager.uncompletedLocations[0];
        var question = location.questions[currentQuestionIndex];

        string correctAnswer = question.answer.Trim().ToLower();
        string userAnswer = playerInput.text.Trim().ToLower();

        if (userAnswer == correctAnswer)
        {
            location.questions.RemoveAt(0);

            if (location.hints.Count > 0)
            {
                hintcount += 1;
                hint1.text = hintsLoc.hints.Count > 0 ? hintsLoc.hints[0] : "";
                if (hintcount == 2) { hint2.text = hintsLoc.hints.Count > 1 ? hintsLoc.hints[1] : ""; }
                if (hintcount == 3) { hint3.text = hintsLoc.hints.Count > 2 ? hintsLoc.hints[2] : ""; }
            }

            if (location.questions.Count == 0)
            {

                playerInput.interactable = false;
                //done
            }
            else
            {
                questionText.text = location.questions[currentQuestionIndex].question;
            }
        }
        else
        {
            //wrong
        }
    }
}
