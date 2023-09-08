using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SpawnCards : MonoBehaviour
{
    [SerializeField] private Transform m_CardsContainer;
    [SerializeField] private GameObject m_CardPrefab;
    
    [SerializeField] private Item [] m_ItemsToSpawn;

    private List<Item> m_Spawnables = new();
    private List<Card> m_Card = new();

    private void Start()
    {
        GenerateCards();
    }

    void GenerateCards()
    {
        for (int i = 0; i < 2; i++)
        {
            m_Spawnables.AddRange(m_ItemsToSpawn);
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
    }
}
