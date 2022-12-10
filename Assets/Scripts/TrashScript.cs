using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashScript : MonoBehaviour, IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            InteractionScript item = eventData.pointerDrag.GetComponent<InteractionScript>();
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            for(int i = 0; i < item.inventoryAccessor.InventoryItems.itemList.Count; i++)
            {
                if (item.inventoryAccessor.InventoryItems.itemList[i].inventoryPosition == item.homeID)
                {
                    item.inventoryAccessor.InventoryItems.itemList.RemoveAt(i);
                    Destroy(item.gameObject);
                    break;
                }
            }
            
            //item.transform.parent = transform;
            //item.homeID = ID;
            //Debug.Log(tempInteraction.inventoryAccessor.InventoryItems.itemList.Find(x => x.itemObject.name == $"{tempInteraction.gameObject.name}"));
            // //tempInteraction.inventoryAccessor.InventoryItems.itemList.Find(x => x.itemObject.name == $"{tempInteraction.gameObject.name}"));
        }
    }
}
