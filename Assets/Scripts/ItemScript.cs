using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemScript  
{
    public enum ItemTypeENUM { Potion, Poison, Bomb, Scroll, Weapon, Armor};
    public ItemTypeENUM itemType;
    public float value;

}

[System.Serializable]
public class ItemScriptCollection
{
    public List<ItemScript> itemList;
}

