using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;


public class InventoryScript : MonoBehaviour
{
    [SerializeField] int ItemsPerRow;
    public GameObject[] inventorySlots = new GameObject[25];
    public GameObject initialSlot, nextSlotOffset, nextRowOffset;
    Vector3 initialSlotV3, nextSlotV3, nextRowV3;
    [SerializeField] GameObject slotGO;
    //[SerializeField] List<ItemScript> InventoryItems = new List<ItemScript>();
    [SerializeField] ItemScriptCollection InventoryItems = new ItemScriptCollection();
    //[SerializeField] JSONWriterScript JSONWriter;
    // Start is called before the first frame update
    void Start()
    {
        InventoryItems.itemList.Clear();
        initialSlotV3 = initialSlot.transform.position;
        nextSlotV3 = initialSlotV3 - nextSlotOffset.transform.position;
        nextRowV3 = initialSlotV3 - nextRowOffset.transform.position;
        int invIncrementor = 0;
        for(int i = 0; i < inventorySlots.Length - 1; i++)
        {
            //if(InventorySlots.Length - i % ItemsPerRow == 0)
            for (int j = 0; j < ItemsPerRow; j++)
            {
                if(invIncrementor < inventorySlots.Length)
                inventorySlots[invIncrementor] = Instantiate(slotGO, initialSlotV3 - (nextSlotV3 * j) - (nextRowV3*i),
                new Quaternion(0, 0, 0, 0), transform);
                invIncrementor++;
               
            }
        }
        //JsonConvert.SerializeObject(InventorySlots);
    }

    public void AddRandomItem()
    {
        if(InventoryItems.itemList.Count < inventorySlots.Length - 1)
        {
            int tempInt = Random.Range(0, 5);
            ItemScript tempIS = new ItemScript();
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
                    tempIS.itemType = ItemScript.ItemTypeENUM.Scroll;
                    break;
                case 5:
                    tempIS.itemType = ItemScript.ItemTypeENUM.Scroll;
                    break;
            }
            InventoryItems.itemList.Add(tempIS);
        }
    }


    public void OutputJSON()
    {
        if (InventoryItems != null)
        {
            string stringOutput = JsonUtility.ToJson(InventoryItems);

            //for(int i = 0; i < pInventory.itemList.Count - 1; i++)
            //{
            //    string stringOutput = JsonUtility.ToJson(pInventory.itemList[i]); 
            //}
            File.WriteAllText(Application.dataPath + "/Inventory.json", stringOutput);

        }
        else
        {
            Debug.Log("Something went wrong while outputting JSON file!");
        }

    }

    public void InputJSON()
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
        //if (JsonUtility.FromJson<ItemScriptCollection>(tempText) != null) 

        InventoryItems.itemList.Clear();
        InventoryItems = JsonUtility.FromJson<ItemScriptCollection>(tempString);
        
    }

}
