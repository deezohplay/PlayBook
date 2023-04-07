using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;

public class LineScript : MonoBehaviour
{
    [Header("Dots")]
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] Transform dotParent;

    private LineRenderer line;
    private Vector3 mousePos;
    private Vector3 pos;
    public Material material;
    private int currLines = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            if (line == null)
            {
                DrawLine();
                mousePos = Input.mousePosition;
                mousePos.z = 1;
                pos = Camera.main.ScreenToWorldPoint(mousePos);
                GameObject dot = Instantiate(dotPrefab, pos, Quaternion.identity, dotParent);
            }
            mousePos = Input.mousePosition;
            mousePos.z = 1;
            pos = Camera.main.ScreenToWorldPoint(mousePos);
            line.SetPosition(0, pos);
            line.SetPosition(1, pos);
        }
        else if(Input.GetMouseButtonUp(0) && line)
        {
            mousePos = Input.mousePosition;
            mousePos.z = 1;
            pos = Camera.main.ScreenToWorldPoint(mousePos);
            line.SetPosition(1, pos);
            line = null;
            currLines++;
            GameObject dot = Instantiate(dotPrefab, pos, Quaternion.identity, dotParent);
        }
        else if (Input.GetMouseButton(0) && line)
        {
            mousePos = Input.mousePosition;
            mousePos.z = 1;
            pos = Camera.main.ScreenToWorldPoint(mousePos);
            line.SetPosition(1, pos);
        }
#endif
    }

    void DrawLine()
    {
        line = new GameObject("Line" + currLines).AddComponent<LineRenderer>();
        line.material = material;
        line.positionCount = 2;
        line.startWidth = 0.02f;
        line.endWidth = 0.02f;
        line.useWorldSpace = false;
        line.numCapVertices = 50;
    }
}
