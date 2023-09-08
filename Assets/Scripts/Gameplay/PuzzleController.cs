using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
   [SerializeField] private List<Item> m_PuzzlePieces;
   
   [SerializeField] private float m_WaitAfterInputs = 0.5f;
   [SerializeField] private float m_WaitBeforeGameStart = 0.5f;
   
   private CardRequestObject m_CurrentPuzzleItem;

   private WaitForSeconds m_InputDelay; 
   private int TapCount = 0;
   
   private void Start()
   {
      GameEvents.GameplayEvents.CardsSpawnRequest.Raise(m_PuzzlePieces.ToArray());
      
      m_InputDelay = new WaitForSeconds(m_WaitAfterInputs);
      Invoke(nameof(StartGame), m_WaitBeforeGameStart);
   }

   private void OnEnable()
   {
      GameEvents.GameplayEvents.CardTap.Register(OnCardTap);
      GameEvents.TimerEvents.TimerComplete.Register(OnTimerComplete);
   }

   private void OnDisable()
   {
      GameEvents.GameplayEvents.CardTap.UnRegister(OnCardTap);
      GameEvents.TimerEvents.TimerComplete.UnRegister(OnTimerComplete);
   }

   private void OnTimerComplete()
   {
      OnGameComplete(false);
   }
   
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
         OnRightGuess(requestObject,m_CurrentPuzzleItem);
      }
      else
      {
         OnWrongGuess();
      }
   }

   private void OnRightGuess(CardRequestObject cardRequestA, CardRequestObject cardRequestB)
   {
      m_PuzzlePieces.Remove(cardRequestA.ItemName);
      
      GameEvents.GameplayEvents.CardRemoveRequested.Raise(cardRequestA, cardRequestB);

      if (m_PuzzlePieces.Count <= 0)
      {
         OnGameComplete(true);
      }

      Reset();
   }

   private void OnWrongGuess()
   {
      Reset();
   }

   private void OnGameComplete(bool status)
   {
      SetGameInputEnabled(false);
      GameEvents.GameplayEvents.GameComplete.Raise(true);
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
}
