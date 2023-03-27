using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Item : MonoBehaviour,IPointerDownHandler, IDragHandler, IPointerEnterHandler, IPointerUpHandler 
{
    static Item objects;
    public GameObject linePrefab;
    public GameObject letters;
    private GameObject line;

    public void OnDrag(PointerEventData eventData)
    {
        UpdateLine(eventData.position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        line = Instantiate(linePrefab, transform.position, Quaternion.identity, transform.parent.parent);
        UpdateLine(eventData.position);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        objects = this;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!this.Equals(objects) && letters.Equals(objects.letters))
        {
            UpdateLine(objects.transform.position);
            Destroy(objects);
            Destroy(this);
        }
        else
        {
            Destroy(line);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateLine(Vector3 position)
    {
        //direction
        Vector3 direction = position - transform.position;
        line.transform.right = direction;
        //scale
        line.transform.localScale = new Vector3(direction.magnitude, 1, 1);
    }

}
