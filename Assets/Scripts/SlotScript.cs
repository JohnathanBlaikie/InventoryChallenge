using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotScript : MonoBehaviour, IDropHandler
{
    public int ID;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            PointerEventData tempEventData = eventData;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition =
                GetComponent<RectTransform>().anchoredPosition;
            InteractionScript tempInteraction = eventData.pointerDrag.GetComponent<InteractionScript>();
            tempInteraction.originPosition =
                GetComponent<RectTransform>().anchoredPosition;
            tempInteraction.homeID = ID;
            Debug.Log(tempInteraction.inventoryAccessor.InventoryItems.itemList.Find(x => x.itemObject.name == $"{tempInteraction.gameObject.name}"));
             //tempInteraction.inventoryAccessor.InventoryItems.itemList.Find(x => x.itemObject.name == $"{tempInteraction.gameObject.name}"));
        }
    }
}
