using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Card : MonoBehaviour
{
    [SerializeField] private Button m_CurrentCard;
    [SerializeField] private Image m_ItemImage;

    private Item m_CardType;

    private void Start()
    {
        m_CurrentCard.onClick.AddListener(OnTap);
    }

    public void Initialize(Item item)
    {
        ItemData itemData = ItemDataHandler.Instance.GetItemData(item);
        m_CardType = itemData.ItemType;
        m_ItemImage.sprite = itemData.ItemSprite;
    }

    private void OnTap()
    {
        //Raise Event
        GameEvents.GameplayEvents.CardTap.Raise(m_CardType);
    }
}
