using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotScript : MonoBehaviour, IDropHandler
{
    public int ID;
    public CanvasGroup currentItem = null;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && transform.childCount == 0)
        {
            InteractionScript item = eventData.pointerDrag.GetComponent<InteractionScript>();
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            item.transform.parent = transform;
            item.homeID = ID;
        }
    }
}
