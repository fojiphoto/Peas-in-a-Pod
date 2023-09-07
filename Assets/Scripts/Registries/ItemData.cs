using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/ItemData/Create", fileName = "ItemData", order = 0)]
public class ItemData : ScriptableObject
{
    [field: SerializeField] public Items ItemType { get; private set; }
    [field: SerializeField] public Sprite ItemSprite { get; private set; }
}
