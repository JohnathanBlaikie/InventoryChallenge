using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
    private Color startColor;
    [SerializeField] Color highlightColor;
    [SerializeField] SpriteRenderer spriteRenderer;
    bool followCursor;
    Vector3 mousePosition;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.material.color;
    }

    private void FixedUpdate()
    {
        if(followCursor)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = Camera.main.nearClipPlane;
            transform.position = mousePosition;
        }
    }

    void OnMouseEnter()
    {
        spriteRenderer.material.color += highlightColor;
        
    }
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !followCursor)
        {
            followCursor = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && followCursor)
        {
            followCursor = false;
        }
    }

    private void OnMouseExit()
    {
        spriteRenderer.material.color = startColor;
    }
}
