using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;


public class InventoryScript : MonoBehaviour
{
    [SerializeField] int ItemsPerRow;
    public GameObject[] inventorySlots = new GameObject[25];
    public GameObject initialSlot, nextSlotOffset, nextRowOffset, potionGO, poisonGO, bombGO,
        armorGO, weaponGO, scrollGO;
    Vector3 initialSlotV3, nextSlotV3, nextRowV3;
    [SerializeField] GameObject slotGO;
    //[SerializeField] List<ItemScript> InventoryItems = new List<ItemScript>();
    [SerializeField] ItemScriptCollection InventoryItems = new ItemScriptCollection();
    //[SerializeField] JSONWriterScript JSONWriter;
    // Start is called before the first frame update
    void Start()
    {
        InstantiateInventorySlots();

        //JsonConvert.SerializeObject(InventorySlots);
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
            //if(InventorySlots.Length - i % ItemsPerRow == 0)
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
        
            int tempInt = Random.Range(0, 5);
            ItemScript tempIS = new ItemScript();
            //GameObject tempGO = new GameObject();
            switch (tempInt)
            {
                case 0:
                    tempIS.itemType = ItemScript.ItemTypeENUM.Potion;
                    break;
                case 1:
                    tempIS.itemType = ItemScript.ItemTypeENUM.Poison;
                    break;
                case 2:
                    tempIS.itemType = ItemScript.ItemTypeENUM.Bomb;
                    break;
                case 3:
                    tempIS.itemType = ItemScript.ItemTypeENUM.Armor;
                    break;
                case 4:
                    tempIS.itemType = ItemScript.ItemTypeENUM.Weapon;
                    break;
                case 5:
                    tempIS.itemType = ItemScript.ItemTypeENUM.Scroll;
                    break;
            }
            SpawnSpecificItem(tempIS.itemType, InventoryItems.itemList.Count);
        
    }

    public void SpawnSpecificItem(ItemScript.ItemTypeENUM _itemType, int _iteration)
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

            tempGO = Instantiate(tempGO, inventorySlots[_iteration].transform);

            ItemScript tempIS = new ItemScript();
            tempIS.itemType = _itemType;
            tempIS.itemObject = tempGO;
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
