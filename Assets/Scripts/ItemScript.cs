using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemScript  
{
    public enum ItemTypeENUM { Potion, Poison, Bomb, Scroll, Weapon, Armor, Pouch};
    public ItemTypeENUM itemType;
    public CanvasGroup itemObject;
    public float value;
    public int inventoryPosition;
    /// <summary>
    /// Full Item Constructor
    /// </summary>
    /// <param name="_itemType"></param>
    /// <param name="_itemObject"></param>
    /// <param name="_value"></param>
    /// <param name="_inventoryPosition"><</param>
    public ItemScript(ItemTypeENUM _itemType, CanvasGroup _itemObject, float _value, int _inventoryPosition)
    {
        itemType = _itemType;
        itemObject = _itemObject;
        value = _value;
        inventoryPosition = _inventoryPosition;
    }


    /// <summary>
    /// Simplified Item Constructor
    /// </summary>
    /// <param name="_itemEnumInt"></param>
    public ItemScript(int _itemEnumInt)
    {
        switch (_itemEnumInt)
        {
            case 0:
                itemType = ItemScript.ItemTypeENUM.Potion;
                break;
            case 1:
                itemType = ItemScript.ItemTypeENUM.Poison;
                break;
            case 2:
                itemType = ItemScript.ItemTypeENUM.Bomb;
                break;
            case 3:
                itemType = ItemScript.ItemTypeENUM.Armor;
                break;
            case 4:
                itemType = ItemScript.ItemTypeENUM.Weapon;
                break;
            case 5:
                itemType = ItemScript.ItemTypeENUM.Scroll;
                break;
            case 6:
                itemType = ItemScript.ItemTypeENUM.Pouch;
                break;
        }
    }

    public ItemScript()
    {
        itemType = 0;
        itemObject = null;
        value = 0;
    }

}

[System.Serializable]
public class ItemScriptCollection
{
    public List<ItemScript> itemList;
    public ItemScriptCollection()
    {
        itemList = null;
    }
    public ItemScriptCollection(List<ItemScript> _itemList)
    {
        itemList = _itemList;
    }
}

[System.Serializable]
public class ItemListSorter : IComparer<string>
{ 
    
    public int Compare(string x, string y)
    {
        if (x == null || y == null)
        {
            return 0;
        }
        return x.CompareTo(y);
    }

}


