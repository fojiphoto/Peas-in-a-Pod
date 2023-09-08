using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Card : MonoBehaviour
{
    
    [SerializeField] private Item m_CardType;
    [SerializeField] private Button m_CurrentCard;
    [SerializeField] private Image m_ItemImage;
    [SerializeField] private bool m_IsFaceUp;
    [SerializeField] private bool m_IsMatched;

    public void Initialize(Item item)
    {
        ItemData itemData = ItemDataHandler.Instance.GetItemData(item);
        m_CardType = itemData.ItemType;
        m_ItemImage.sprite = itemData.ItemSprite;
    }
}
