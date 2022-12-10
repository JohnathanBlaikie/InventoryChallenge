using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionScript : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] Color highlightColor;
    [SerializeField] Canvas canvas;
    [SerializeField] CanvasGroup canvasGroup;
    RectTransform rectTransform;
    Transform lastParent;
    public InventoryScript inventoryAccessor;
    public Vector3 originPosition;
    public int homeID;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        inventoryAccessor = GameObject.Find("InventoryGO").GetComponent<InventoryScript>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Click");
        

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = .6f;
        Debug.Log("Drag");
        lastParent = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        originPosition = rectTransform.anchoredPosition;


    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        Debug.Log("Hold Drag");

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        Debug.Log("Release");
        if (transform.GetComponentInParent<SlotScript>() == null)
        {
            rectTransform.anchoredPosition = originPosition;
            transform.SetParent(lastParent);
        }

    }

    public void OnDrop(PointerEventData eventData)
    {

    }

}
