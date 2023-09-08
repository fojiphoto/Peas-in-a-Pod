using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
   [SerializeField] private List<Item> m_PuzzlePieces;
   [SerializeField] private float m_WaitAfterInputs = 0.5f;
   
   private CardRequestObject m_CurrentPuzzleItem;

   private WaitForSeconds m_InputDelay; 
   private int TapCount = 0;
   
   private void Start()
   {
      GameEvents.GameplayEvents.CardsSpawnRequest.Raise(m_PuzzlePieces.ToArray());
      m_InputDelay = new WaitForSeconds(m_WaitAfterInputs);
   }
   
   private void OnEnable()
   {
      GameEvents.GameplayEvents.CardTap.Register(OnCardTap);
   }

   private void OnDisable()
   {
      GameEvents.GameplayEvents.CardTap.UnRegister(OnCardTap);
   }
   
   private void OnCardTap(CardRequestObject item)
   {
      if (m_CurrentPuzzleItem == null)
      {
         m_CurrentPuzzleItem = item;
      }
      else
      {
         GameEvents.GameplayEvents.InputStatusChanged.Raise(false);
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
         OnGameWin();
      }

      Reset();
   }

   private void OnWrongGuess()
   {
      Reset();
   }

   private void OnGameWin()
   {
      Debug.LogError("Game Complete");
   }

   private void Reset()
   {
      m_CurrentPuzzleItem = null;
      GameEvents.GameplayEvents.ResetGame.Raise();
      GameEvents.GameplayEvents.InputStatusChanged.Raise(true);
   }
}
