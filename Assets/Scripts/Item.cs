using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour, IPointerEnterHandler, IPointerUpHandler, IPointerDownHandler 
{
    static Item letters;
    public GameObject objects;
    private int score;

    private void Awake()
    {
      
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        score++;
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        letters = this;
        Debug.Log(this);
    }
}
