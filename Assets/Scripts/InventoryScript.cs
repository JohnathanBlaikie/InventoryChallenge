using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    [SerializeField] int ItemsPerRow;
    public GameObject[] InventorySlots = new GameObject[25];
    public GameObject initialSlot, nextSlotOffset, nextRowOffset;
    Vector3 initialSlotV3, nextSlotV3, nextRowV3;
    [SerializeField] GameObject slotGO;
    [SerializeField] List<ItemScript> InventoryItems;
    // Start is called before the first frame update
    void Start()
    {
        initialSlotV3 = initialSlot.transform.position;
        nextSlotV3 = initialSlotV3 - nextSlotOffset.transform.position;
        nextRowV3 = initialSlotV3 - nextRowOffset.transform.position;
        int invIncrementor = 0;
        for(int i = 0; i < InventorySlots.Length - 1; i++)
        {
            //if(InventorySlots.Length - i % ItemsPerRow == 0)
            for (int j = 0; j < ItemsPerRow; j++)
            {
                if(invIncrementor < InventorySlots.Length)
                InventorySlots[invIncrementor] = Instantiate(slotGO, initialSlotV3 - (nextSlotV3 * j) - (nextRowV3*i),
                new Quaternion(0, 0, 0, 0), transform);
                invIncrementor++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
