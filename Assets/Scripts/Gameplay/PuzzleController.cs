using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
   [SerializeField] private List<Item> m_PuzzlePieces;
   
   private CardRequestObject m_CurrentPuzzleItem;

   private int TapCount = 0;

   private void OnEnable()
   {
      GameEvents.GameplayEvents.CardTap.Register(OnCardTap);
   }

   private void OnDisable()
   {
      GameEvents.GameplayEvents.CardTap.UnRegister(OnCardTap);
   }

   private void Start()
   {
      GameEvents.GameplayEvents.CardsSpawnRequest.Raise(m_PuzzlePieces.ToArray());
   }

   private void OnCardTap(CardRequestObject item)
   {
      if (m_CurrentPuzzleItem == null)
      {
         m_CurrentPuzzleItem = item;
      }
      else
      {
         if (m_CurrentPuzzleItem.ItemName == item.ItemName)
         {
            OnRightGuess(item,m_CurrentPuzzleItem);
         }
         else
         {
            OnWrongGuess();
         }
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
      
   }

   private void Reset()
   {
      m_CurrentPuzzleItem = null;
      GameEvents.GameplayEvents.ResetGame.Raise();
   }
}
