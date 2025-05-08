using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;


public class hints : MonoBehaviour
{
    public Gamemanager gameManager;
    public GameObject completionPanel;
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
        if (gameManager.uncompletedLocations.Count > 0)
        {
            var hintsloc = gameManager.uncompletedLocations[0];
            if (hintsloc.hints.Count > 0)
            {
                hint1.text = hintcount > 0 ? "1. " + hintsloc.hints[0] : "";
                hint2.text = hintcount > 1 ? "2. " + hintsloc.hints[1] : "";
                hint3.text = hintcount > 2 ? "3. " + hintsloc.hints[2] : "";
            }
        }

        if (location.questions.Count > 0)
        {
            questionText.text = location.questions[currentQuestionIndex].question;
        }
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
        hint1.text = "1. ";
        hint2.text = "2. ";
        hint3.text = "3. ";
    }

    public void CheckAnswer()
    {
        if (gameManager.completedLocations.Count == 0) return;

        var location = gameManager.completedLocations[^1];
        var question = location.questions[currentQuestionIndex];

        string userAnswer = playerInput.text.Trim().ToLower();
        bool correctBool = true;

        foreach (string ans in question.answer)
        {
            string newAns = ans.Trim().ToLower();
            Debug.Log(newAns);
            if (!userAnswer.Contains(newAns))
            {
                correctBool = false;
                break;
            }
        }

        if (correctBool)
        {
            playerInput.GetComponent<Image>().color = Color.green;
            StartCoroutine(white());

            location.questions.RemoveAt(0);

            if (gameManager.uncompletedLocations.Count > 0)
            {
                var hintsLoc = gameManager.uncompletedLocations[0];
                if (hintsLoc.hints.Count > 0)
                {
                    hintcount += 1;
                    hint1.text = hintsLoc.hints.Count > 0 ? "1. " + hintsLoc.hints[0] : "1. ";
                    if (hintcount == 2) { hint2.text = hintsLoc.hints.Count > 1 ? "2. " + hintsLoc.hints[1] : "2. "; }
                    if (hintcount == 3) { hint3.text = hintsLoc.hints.Count > 2 ? "3. " + hintsLoc.hints[2] : "3. "; }
                }
                else
                {

                    if (location.questions.Count == 0)
                    {
                        completionPanel.SetActive(true);
                        gameManager.StopAllCoroutines();
                    }
                    else
                    {
                        hint1.text = "Beantwoord alle vragen om de stadsbingo te halen.";
                        hint2.text = "";
                        hint3.text = "";
                    }
                }
            }
            else
            {
                if (location.questions.Count == 0)
                {
                    completionPanel.SetActive(true);
                    gameManager.StopAllCoroutines();
                }
                else
                {
                    hint1.text = "Beantwoord alle vragen om de stadsbingo te halen.";
                    hint2.text = "";
                    hint3.text = "";
                }
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
