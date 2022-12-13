using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PouchScript : MonoBehaviour, IDropHandler
{
    [SerializeField] Color highlightColor;
    [SerializeField] Canvas canvas;
    [SerializeField] CanvasGroup canvasGroup;
    RectTransform rectTransform;
    Transform lastParent;
    public InventoryScript inventoryAccessor;
    public Vector3 originPosition;
    public int personalID, slotPositionID;
    public CanvasGroup currentItem = null;
    List<ItemScript>inventoryWithinInventory = null;


    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        inventoryAccessor = GameObject.Find("InventoryGO").GetComponent<InventoryScript>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }


    public void OnPointerDown(PointerEventData eventData)
    {


    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = .6f;
        lastParent = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        originPosition = rectTransform.anchoredPosition;


    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        if (transform.GetComponentInParent<SlotScript>() == null)
        {
            rectTransform.anchoredPosition = originPosition;
            transform.SetParent(lastParent);
        }

    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && transform.childCount == 0)
        {
            InteractionScript item = eventData.pointerDrag.GetComponent<InteractionScript>();
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            item.transform.parent = transform;
            item.homeID = slotPositionID;
            
            inventoryWithinInventory.Add(item.GetComponent<ItemScript>());
        }
    }
}
