using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class JSONWriterScript : MonoBehaviour
{

    public class PlayerInventory
    {
        public List<ItemScript> itemList;
    }

    public PlayerInventory pInventory = new PlayerInventory();
    
    public void OutputJSON()
    {
        if(pInventory.itemList != null)
        {
            string stringOutput = JsonUtility.ToJson(pInventory.itemList);
            File.WriteAllText(Application.dataPath + "/Inventory.JSON", stringOutput);
        }
        else
        {
            Debug.Log("Something went wrong while outputting JSON file!");
        }

    }
}
