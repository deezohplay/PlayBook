using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DrawTool : MonoBehaviour
{
    [Header("GameCanvas")]
    [SerializeField] private GameCanvas gameCanvas;

    [Header("Dots")]
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] Transform dotParent;

    [Header("Lines")]
    [SerializeField] private GameObject linePrefab;
    [SerializeField] Transform lineParent;
    private Line currentLine;


    // Start is called before the first frame update
    void Start()
    {
        gameCanvas.OnGameCanvasLeftClickEvent += AddDot;
    }

    private void AddDot()
    {
        if (currentLine == null)
        {
            currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity, lineParent).GetComponent<Line>();
        }
        GameObject dot = Instantiate(dotPrefab, GetMousePosition(), Quaternion.identity, dotParent);
        currentLine.AddPoint(dot.transform);
    }
    // Update is called once per frame
    void Update()
    {
       
    }
    private Vector3 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 1; 
        Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);
        
        return pos;
    }
}
