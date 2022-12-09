using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionScript : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    //private Color startColor;
    [SerializeField] Color highlightColor;
    //[SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Canvas canvas;
    [SerializeField] CanvasGroup canvasGroup;
    RectTransform rectTransform;
    public InventoryScript inventoryAccessor;
    public Vector3 originPosition;
    public int homeID, personalID;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        inventoryAccessor = GameObject.Find("InventoryGO").GetComponent<InventoryScript>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //startColor = spriteRenderer.material.color;
        
    }

    private void FixedUpdate()
    {
        //if(followCursor)
        //{
        //    mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    mousePosition.z = Camera.main.nearClipPlane;
        //    transform.position = mousePosition;
            
        //}
        
    }

    void OnMouseEnter()
    {
        //spriteRenderer.material.color += highlightColor;
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
        inventoryAccessor.InventoryItems.itemList.Find(x => x.itemObject.name == $"{this.name}");
        //rectTransform.anchoredPosition = originPosition;

    }

    public void OnDrop(PointerEventData eventData)
    {

    }

    private void OnMouseOver()
    {
        //spriteRenderer.material.color += highlightColor;

        //if (Input.GetKeyDown(KeyCode.Mouse0) && !followCursor)
        //{
        //    returnPoint = transform.position;
        //    followCursor = true;
        //}
        //if (Input.GetKeyUp(KeyCode.Mouse0) && followCursor)
        //{
        //    followCursor = false;
        //    //if not over empty space
        //    transform.position = returnPoint;

        //    //if over empty space
        //    //do stuff
        //}
    }

    private void OnMouseExit()
    {
        //spriteRenderer.material.color = startColor;
    }
}