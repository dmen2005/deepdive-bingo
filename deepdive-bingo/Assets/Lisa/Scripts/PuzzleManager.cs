using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Metadata;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private List<Transform> puzzlePieces;

    void Awake()
    {
        // index all puzzle pieces based on their position in the hirarchy
        foreach (Transform puzzlePiece in puzzlePieces)
        {
            puzzlePiece.GetComponent<PuzzlePiece>().SetIndex(puzzlePiece.GetSiblingIndex());
        }
    }

    private void Start()
    {
        Shuffle();
    }

    void Shuffle()
    {
        // shuffle puzzle pieces
        for (int i = 0; i < puzzlePieces.Count; i++)
        {
            int randomIndex = Random.Range(i, puzzlePieces.Count);
            (puzzlePieces[i], puzzlePieces[randomIndex]) = (puzzlePieces[randomIndex], puzzlePieces[i]);
        }

        for (int i = 0; i < puzzlePieces.Count; i++)
        {
            puzzlePieces[i].SetSiblingIndex(i);
        }


    }
}
