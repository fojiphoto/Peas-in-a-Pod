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

    public Guid ID { get; private set; }

    private void Start()
    {
        m_CurrentCard.onClick.AddListener(OnTap);
    }

    public void Initialize(Item item)
    {
        ItemData itemData = ItemDataHandler.Instance.GetItemData(item);
        m_CardType = itemData.ItemType;
        m_ItemImage.sprite = itemData.ItemSprite;
        ID = Guid.NewGuid();
        
        Hide();
    }

    private void OnTap()
    {
        Reveal();
        StartCoroutine(OnTapDelay());

    }

    IEnumerator OnTapDelay()
    {
        yield return new WaitForSeconds(0.5f);
        //Raise Event
        GameEvents.GameplayEvents.CardTap.Raise(new CardRequestObject()
        {
            ID = ID,
            ItemName = m_CardType
        });
        
    }

    public void Reveal()
    {
        m_CurrentCard.interactable = false;
        m_ItemImage.gameObject.SetActive(true);
    }

    public void Hide()
    {
        m_CurrentCard.interactable = true;
        m_ItemImage.gameObject.SetActive(false);
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
