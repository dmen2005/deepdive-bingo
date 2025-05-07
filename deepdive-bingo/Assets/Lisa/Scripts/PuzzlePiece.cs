using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int pieceIndex;
    [SerializeField] private int correctIndex;

    private void Awake()
    {
        correctIndex = pieceIndex;
    }
    //Detect if a click occurs
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Use this to tell when the user right-clicks on the Button
        if (pointerEventData.button == PointerEventData.InputButton.Right)
        {
            //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
            Debug.Log(name + " Game Object at index " + pieceIndex + " Right Clicked!");
        }

        //Use this to tell when the user left-clicks on the Button
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log(name + " Game Object at index " + pieceIndex + " Left Clicked!");
        }
    }

    public void SetIndex(int newIndex)
    {
        pieceIndex = newIndex;
    }

}