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
    public GameObject initialSlot, nextSlotOffset, nextRowOffset;
    Vector3 initialSlotV3, nextSlotV3, nextRowV3;
    public CanvasGroup potionCG, poisonCG, bombCG,
        armorCG, weaponCG, scrollCG;
    [SerializeField] GameObject slotGO;
    public ItemScriptCollection InventoryItems = new ItemScriptCollection();
    [SerializeField] TMP_Dropdown sortMenu;
    [SerializeField] Canvas canvas;
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
                {
                    inventorySlots[invIncrementor] = Instantiate(slotGO, initialSlotV3 - (nextSlotV3 * j) - (nextRowV3 * i) * canvas.scaleFactor,
                    new Quaternion(0, 0, 0, 0), transform);
                    inventorySlots[invIncrementor].GetComponent<SlotScript>().ID = invIncrementor;
                }
                    invIncrementor++;

            }
        }
    }

    public void AddRandomItem()
    {

        int tempInt = Random.Range(0, 6);
        ItemScript tempIS = new ItemScript(tempInt);

        SpawnItem(tempIS.itemType, InventoryItems.itemList.Count, false);

    }

    public void AddSpecificItem(int itemType)
    {
        ItemScript tempIS = new ItemScript(itemType);
        SpawnItem(tempIS.itemType, InventoryItems.itemList.Count, false);
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
                    ItemScript tempItem = null;
                    for (int i = 0; i < InventoryItems.itemList.Count; i++)
                    {
                        tempItem = InventoryItems.itemList[i];
                        tempItem.itemObject.transform.position = inventorySlots[i].transform.position;
                        tempItem.itemObject.transform.SetParent(inventorySlots[i].transform);
                        tempItem.inventoryPosition = i;
                        tempItem.itemObject.GetComponent<InteractionScript>().homeID = i;
                    }
                    break;
                case 2:
                    InventoryItems.itemList.Sort((x, y) => string.Compare(y.itemType.ToString(), x.itemType.ToString()));
                    for (int i = 0; i < InventoryItems.itemList.Count; i++)
                    {
                        tempItem = InventoryItems.itemList[i];
                        InventoryItems.itemList[i].itemObject.transform.position = inventorySlots[i].transform.position;
                        InventoryItems.itemList[i].itemObject.transform.SetParent(inventorySlots[i].transform);
                        tempItem.inventoryPosition = i;
                        tempItem.itemObject.GetComponent<InteractionScript>().homeID = i;
                    }
                    break;
            }
        }
    }

    public void SpawnItem(ItemScript.ItemTypeENUM _itemType, int iteration, bool load)
    {
        if (InventoryItems.itemList.Count < inventorySlots.Length)
        {
            CanvasGroup tempCG = null;
            switch (_itemType)
            {
                case ItemScript.ItemTypeENUM.Potion:
                    tempCG = potionCG;
                    break;
                case ItemScript.ItemTypeENUM.Poison:
                    tempCG = poisonCG;
                    break;
                case ItemScript.ItemTypeENUM.Bomb:
                    tempCG = bombCG;
                    break;
                case ItemScript.ItemTypeENUM.Armor:
                    tempCG = armorCG;
                    break;
                case ItemScript.ItemTypeENUM.Weapon:
                    tempCG = weaponCG;
                    break;
                case ItemScript.ItemTypeENUM.Scroll:
                    tempCG = scrollCG;
                    break;
            }
            if (load)
            {

                tempCG = Instantiate(tempCG, inventorySlots[iteration].transform);
                iteration = tempCG.GetComponent<InteractionScript>().homeID = iteration;

            }
            else
            {
                for (int i = 0; i < inventorySlots.Length; i++)
                {
                    if (inventorySlots[i].transform.childCount == 0)
                    {
                        tempCG = Instantiate(tempCG, inventorySlots[i].transform);
                        iteration = tempCG.GetComponent<InteractionScript>().homeID = i;
                        break;
                    }
                }
            }
            ItemScript tempIS = new ItemScript(_itemType, tempCG, iteration);
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
            for(int i = 0; i < InventoryItems.itemList.Count; i++)
            {
                int tempID = InventoryItems.itemList[i].itemObject.GetComponent<InteractionScript>().homeID;
                InventoryItems.itemList[i].inventoryPosition = tempID;
            }
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
        for (int i = 0; i < tempListLength; i++)
        {
            SpawnItem(tempItemScriptCollection.itemList[i].itemType,
                tempItemScriptCollection.itemList[i].inventoryPosition, true);

        }
        
    }

}
