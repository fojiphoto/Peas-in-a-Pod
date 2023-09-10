using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PuzzleController : MonoBehaviour
{
    [SerializeField] private List<PuzzleObject> m_PuzzleToSolve = new();

    [SerializeField] private float m_WaitAfterInputs = 0.5f;
    [SerializeField] private float m_WaitBeforeGameStart = 0.5f;

    private CardRequestObject m_CurrentPuzzleItem;
    private List<Item> m_PuzzlePieces = new();

    private WaitForSeconds m_InputDelay;
    public int TapCount = 0;
    private int currentLevelIndex = 0;

    private void Start()
    {
        if (PlayerPrefsManager.Get(PlayerPrefsManager.CurrentLevel, 0) <= m_PuzzleToSolve.Count)
            currentLevelIndex = PlayerPrefsManager.Get(PlayerPrefsManager.CurrentLevel, 0);

        ConstructPuzzle(m_PuzzleToSolve[currentLevelIndex]);
    }

    private void OnEnable()
    {
        GameEvents.GameplayEvents.CardTap.Register(OnCardTap);
    }

    private void OnDisable()
    {
        GameEvents.GameplayEvents.CardTap.UnRegister(OnCardTap);
    }
    public void ConstructPuzzle(PuzzleObject Obj)
    {
        SetGameInputEnabled(false);
        Debug.Log("Constructing Puzzle");
        int totalGridCells = Obj.TotalCells;
        Debug.Log("Total Grid Cells " + totalGridCells);
        Item[] enumValues = (Item[])Enum.GetValues(typeof(Item));

        for (int i = 0; i < totalGridCells / 2; i++)
        {
            m_PuzzlePieces.Add(enumValues[Random.Range(0, enumValues.Length)]);
            Debug.Log("adding Grid Cell to List");
        }

        m_PuzzlePieces.AddRange(m_PuzzlePieces);

        GameEvents.GameplayEvents.CardsSpawnRequest.Raise(m_PuzzlePieces, Obj.PuzzleConfig.Rows);
        m_InputDelay = new WaitForSeconds(m_WaitAfterInputs);
        Invoke(nameof(StartGame), m_WaitBeforeGameStart);
    }
    #region GameLogics

    void StartGame()
    {
        GameEvents.GameplayEvents.StartGame.Raise();
        GameEvents.GameplayEvents.ResetGame.Raise();
        SetGameInputEnabled(true);
    }

    private void OnCardTap(CardRequestObject item)
    {
        if (m_CurrentPuzzleItem == null)
        {
            m_CurrentPuzzleItem = item;
        }
        else
        {
            SetGameInputEnabled(false);
            StartCoroutine(CompareCards(item));
        }
    }

    private IEnumerator CompareCards(CardRequestObject requestObject)
    {
        yield return m_InputDelay;

        if (m_CurrentPuzzleItem.ItemName == requestObject.ItemName)
        {
            OnRightGuess(requestObject, m_CurrentPuzzleItem);
        }
        else
        {
            OnWrongGuess();
        }
    }

    private void OnRightGuess(CardRequestObject cardRequestA, CardRequestObject cardRequestB)
    {
        GameEvents.GameplayEvents.CardRemoveRequested.Raise(cardRequestA, cardRequestB);
        Reset();
    }

    private void OnWrongGuess()
    {
        Reset();
    }

    public void OnGameComplete(bool status)
    {
        SetGameInputEnabled(false);
        GameEvents.GameplayEvents.GameComplete.Raise(status);

        Debug.Log(" Complete  " + status);

    }

    private void SetGameInputEnabled(bool status)
    {
        GameEvents.GameplayEvents.InputStatusChanged.Raise(status);
    }

    private void Reset()
    {
        m_CurrentPuzzleItem = null;
        GameEvents.GameplayEvents.ResetGame.Raise();
        SetGameInputEnabled(true);
    }
    #endregion

}
