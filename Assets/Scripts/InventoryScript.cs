using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using TMPro;


public class InventoryScript : MonoBehaviour
{
    [SerializeField] int ItemsPerRow;
    public GameObject[] inventorySlots = new GameObject[25];
    public GameObject initialSlot, nextSlotOffset, nextRowOffset, potionGO, poisonGO, bombGO,
        armorGO, weaponGO, scrollGO;
    Vector3 initialSlotV3, nextSlotV3, nextRowV3;
    [SerializeField] GameObject slotGO;
    [SerializeField] ItemScriptCollection InventoryItems = new ItemScriptCollection();
    [SerializeField] TMP_Dropdown sortMenu;
    // Start is called before the first frame update
    void Start()
    {
        InstantiateInventorySlots();

    }


    public void InstantiateInventorySlots()
    {
        InventoryItems.itemList.Clear();
        initialSlotV3 = initialSlot.transform.position;
        nextSlotV3 = initialSlotV3 - nextSlotOffset.transform.position;
        nextRowV3 = initialSlotV3 - nextRowOffset.transform.position;
        int invIncrementor = 0;
        for (int i = 0; i < inventorySlots.Length - 1; i++)
        {
            for (int j = 0; j < ItemsPerRow; j++)
            {
                if (invIncrementor < inventorySlots.Length)
                    inventorySlots[invIncrementor] = Instantiate(slotGO, initialSlotV3 - (nextSlotV3 * j) - (nextRowV3 * i),
                    new Quaternion(0, 0, 0, 0), transform);
                invIncrementor++;

            }
        }
    }

    public void AddRandomItem()
    {

        int tempInt = Random.Range(0, 6);
        ItemScript tempIS = new ItemScript(tempInt);

        SpawnSpecificItem(tempIS.itemType, InventoryItems.itemList.Count);

    }

    public void SortItemInventory()
    {
        if (InventoryItems.itemList != null)
        {
            switch (sortMenu.value)
            {
                case 0:
                    break;
                case 1:
                    InventoryItems.itemList.Sort((x, y) => string.Compare(x.itemType.ToString(), y.itemType.ToString()));
                    for (int i = 0; i < InventoryItems.itemList.Count; i++)
                    {
                        InventoryItems.itemList[i].itemObject.transform.position = inventorySlots[i].transform.position;
                    }
                    break;
                case 2:
                    InventoryItems.itemList.Sort((x, y) => string.Compare(y.itemType.ToString(), x.itemType.ToString()));
                    for (int i = 0; i < InventoryItems.itemList.Count; i++)
                    {
                        InventoryItems.itemList[i].itemObject.transform.position = inventorySlots[i].transform.position;
                    }
                    break;
            }
        }
    }

    public void SpawnSpecificItem(ItemScript.ItemTypeENUM _itemType, int iteration)
    {
        if (InventoryItems.itemList.Count < inventorySlots.Length)
        {
            GameObject tempGO = null;
            switch (_itemType)
            {
                case ItemScript.ItemTypeENUM.Potion:
                    tempGO = potionGO;
                    break;
                case ItemScript.ItemTypeENUM.Poison:
                    tempGO = poisonGO;
                    break;
                case ItemScript.ItemTypeENUM.Bomb:
                    tempGO = bombGO;
                    break;
                case ItemScript.ItemTypeENUM.Armor:
                    tempGO = armorGO;
                    break;
                case ItemScript.ItemTypeENUM.Weapon:
                    tempGO = weaponGO;
                    break;
                case ItemScript.ItemTypeENUM.Scroll:
                    tempGO = scrollGO;
                    break;
            }

            tempGO = Instantiate(tempGO, inventorySlots[iteration].transform);

            ItemScript tempIS = new ItemScript(_itemType, tempGO, 0);
            InventoryItems.itemList.Add(tempIS);
        }
    }

    public void ClearInventory()
    {
        if (InventoryItems.itemList != null)
        {
            for (int i = 0; i < InventoryItems.itemList.Count; i++)
            {
                if (InventoryItems.itemList[i].itemObject != null)
                {
                    Destroy(InventoryItems.itemList[i].itemObject.gameObject);
                }
            }
            InventoryItems.itemList.Clear();
        }
    }

    public void OutputJSON()
    {
        if (InventoryItems != null)
        {
            string stringOutput = JsonUtility.ToJson(InventoryItems);

            File.WriteAllText(Application.dataPath + "/Inventory.json", stringOutput);

        }
        else
        {
            Debug.Log("Something went wrong while outputting JSON file!");
        }

    }

    public void LoadJSON()
    {
        string tempString;
        try
        {
            tempString = File.ReadAllText(Application.dataPath + "/Inventory.json");
        }
        catch
        {
            tempString = null;

        }

        ClearInventory();
        ItemScriptCollection tempItemScriptCollection = JsonUtility.FromJson<ItemScriptCollection>(tempString);
        int tempListLength = tempItemScriptCollection.itemList.Count;
        for(int i = 0; i < tempListLength; i++)
        {
            SpawnSpecificItem(tempItemScriptCollection.itemList[i].itemType, i);
        }
        
    }

}
