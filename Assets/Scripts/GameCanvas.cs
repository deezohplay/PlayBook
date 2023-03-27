using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameCanvas : MonoBehaviour, IPointerClickHandler
{
    public Action OnGameCanvasLeftClickEvent;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.pointerId == -1)
        {
            //Left click
            OnGameCanvasLeftClickEvent?.Invoke();
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
}
