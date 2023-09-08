using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardsHandler : MonoBehaviour
{
    [SerializeField] private Transform m_CardsContainer;
    [SerializeField] private GameObject m_CardPrefab;

    [SerializeField] private GridLayoutGroup m_GridComponent;
    
    private List<Item> m_Spawnables = new();
    private List<Card> m_Card = new();

    private void OnEnable()
    {
        GameEvents.GameplayEvents.CardsSpawnRequest.Register(GenerateCards);
        GameEvents.GameplayEvents.CardRemoveRequested.Register(OnCardRemoveRequested);
        GameEvents.GameplayEvents.ResetGame.Register(OnResetGame);
    }

    private void OnDisable()
    {
        GameEvents.GameplayEvents.CardsSpawnRequest.UnRegister(GenerateCards);
        GameEvents.GameplayEvents.CardRemoveRequested.UnRegister(OnCardRemoveRequested);
        GameEvents.GameplayEvents.ResetGame.UnRegister(OnResetGame);
    }
    
    void GenerateCards(Item[] cards)
    {
        for (int i = 0; i < 2; i++)
        {
            m_Spawnables.AddRange(cards);
        }

        SpawnAndInitializeCardsInternal();
    }

    private void SpawnAndInitializeCardsInternal()
    {
        while (m_Spawnables.Count > 0)
        {
            GameObject cardGO = Instantiate(m_CardPrefab, m_CardsContainer);
            Card card = cardGO.GetComponent<Card>();

            int index = Random.Range(0, m_Spawnables.Count);
            
            card.Initialize(m_Spawnables[index]);
            m_Spawnables.RemoveAt(index);
            
            m_Card.Add(card);
        }

        Invoke(nameof(DisableGridComponent), 0.5f);
    }

    private void DisableGridComponent()
    {
        m_GridComponent.enabled = false;
    }

    void OnCardRemoveRequested(CardRequestObject cardRequestA, CardRequestObject cardRequestB)
    {

        Card card = GetItemData(cardRequestA.ID);
        m_Card.Remove(card);
        card.Destroy();
        
        card = GetItemData(cardRequestB.ID);
        
        m_Card.Remove(card);
        card.Destroy();
    }

    void OnResetGame()
    {
        for (int i = 0; i < m_Card.Count; i++)
        {
            m_Card[i].Hide();
        }
    }
    
    public Card GetItemData(Guid ID) => m_Card.Find(x => x.ID == ID);
}
