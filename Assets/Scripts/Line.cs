using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Line : MonoBehaviour
{
    private static Line Instance { set; get;}
    private LineRenderer lineRenderer;
    private List<Transform> points;

    private void Awake()
    {
        Instance = this;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        points = new List<Transform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoint(Transform point)
    {
        lineRenderer.positionCount++;
        points.Add(point);
    }

    private void LateUpdate()
    {
        if (points.Count >= 2)
        {
            for(int i = 0; i < points.Count; i++)
            {
                lineRenderer.SetPosition(i, points[i].position);
            }
        }
    }
}
