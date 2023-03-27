using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Link : MonoBehaviour,IPointerDownHandler, IDragHandler, IPointerEnterHandler, IPointerUpHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Getting mouse position
    private Vector3 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 1;
        Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);
        return pos;
    }
}
