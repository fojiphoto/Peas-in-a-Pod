using System.Collections.Generic;
using UnityEngine;

public class ItemDataHandler : MonobehaviourSingleton<ItemDataHandler>
{
    [SerializeField] private List<ItemData> m_Items = new();

    public ItemData GetItemData(Item itemName) => m_Items.Find(x => x.ItemType == itemName);
}

