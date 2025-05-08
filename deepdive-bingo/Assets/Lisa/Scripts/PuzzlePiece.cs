using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private int pieceIndex;
    [SerializeField] private int correctIndex;

    private Transform originalParent;
    private Vector2 originalPosition;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    public bool isLocked = false;

    private void Awake()
    {
        pieceIndex = correctIndex;
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void SetIndex(int newIndex)
    {
        pieceIndex = newIndex;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log(name + " Game Object at index " + pieceIndex + " Right Clicked!");
        }

        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log(name + " Game Object at index " + pieceIndex + " Left Clicked!");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isLocked) return;

        originalParent = transform.parent;
        originalPosition = transform.position;

        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isLocked) return;

        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isLocked) return;

        canvasGroup.blocksRaycasts = true;

        GameObject target = eventData.pointerCurrentRaycast.gameObject;

        if (target != null && target.TryGetComponent(out PuzzlePiece otherPiece) && otherPiece != this && !otherPiece.isLocked)
        {
            // Swap hierarchy order
            Transform otherParent = otherPiece.transform.parent;

            int thisIndex = transform.GetSiblingIndex();
            int otherIndex = otherPiece.transform.GetSiblingIndex();

            // Swap parents and sibling indices
            transform.SetParent(otherParent);
            transform.SetSiblingIndex(otherIndex);

            otherPiece.transform.SetParent(originalParent);
            otherPiece.transform.SetSiblingIndex(thisIndex);

            // Check both tiles after swapping
            CheckIfCorrectPosition();
            otherPiece.CheckIfCorrectPosition();
        }
        else
        {
            // Snap back to original position
            transform.SetParent(originalParent);
            transform.position = originalPosition;
        }
        pieceIndex = transform.GetSiblingIndex();
    }

    private void CheckIfCorrectPosition()
    {
        int currentIndex = transform.GetSiblingIndex();
        if (currentIndex == correctIndex)
        {
            isLocked = true;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            // Optional: change color or show visual lock
            Debug.Log($"{name} locked in correct position!");


            bool check = true;
            foreach (Transform child in transform.parent)
            {
                GameObject childObj = child.gameObject;

                if (!childObj.GetComponent<PuzzlePiece>().isLocked)
                {
                    check = false;
                    break;
                }
            }
            if (check)
            {
                GameObject.Find("GameManager").GetComponent<Gamemanager>().CompleteMinigame();
                GameObject.Destroy(transform.parent.gameObject);
            }
        }
    }
}
