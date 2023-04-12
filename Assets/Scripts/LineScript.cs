using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using System;
using TMPro;

public class LineScript : MonoBehaviour
{
    //RAYCASTS
    private RaycastHit hitLeft;
    private RaycastHit hitRight;

    //pool indices
    private string letter;
    private string item;
    public int score;

    public Vector3 touchPos;

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
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //MOUSE INPUTS
        
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0) && LevelManager.Instance.began == true)
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //feeback from hit
            if (Physics.Raycast(ray, out hitLeft))
            {
                if (hitLeft.collider == LevelManager.Instance.letterColliders[0])
                {
                    letter = LevelManager.Instance.leftSpot[0].GetComponent<SpriteRenderer>().sprite.name.ToString();
                }else if (hitLeft.collider == LevelManager.Instance.letterColliders[1])
                {
                    letter = LevelManager.Instance.leftSpot[1].GetComponent<SpriteRenderer>().sprite.name.ToString();
                }
                else if (hitLeft.collider == LevelManager.Instance.letterColliders[2])
                {
                    letter = LevelManager.Instance.leftSpot[2].GetComponent<SpriteRenderer>().sprite.name.ToString();
                }
                else if(hitLeft.collider != null)
                {
                    hitLeft.collider.enabled = false;
                }
            }

            if (line == null)
            {
                if(hitLeft.collider != null)
                {
                    DrawLine();
                    mousePos = Input.mousePosition;
                    mousePos.z = 1;
                    pos = Camera.main.ScreenToWorldPoint(mousePos);
                    GameObject dot = Instantiate(dotPrefab, pos, Quaternion.identity, dotParent);
                }  
            }
            mousePos = Input.mousePosition;
            mousePos.z = 1;
            pos = Camera.main.ScreenToWorldPoint(mousePos);
            line.SetPosition(0, pos);
            line.SetPosition(1, pos);
        }
        else if(Input.GetMouseButtonUp(0) && line && LevelManager.Instance.began == true)
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //feeback from hit
            if (Physics.Raycast(ray, out hitRight))
            {
                if (hitRight.collider == LevelManager.Instance.itemColliders[0])
                {
                    item = LevelManager.Instance.rightSpot[0].GetComponent<SpriteRenderer>().sprite.name.ToString();
                    if (letter == item)
                    {
                        score += 1;
                        LevelManager.Instance.scoreText.text = score.ToString();
                        LevelManager.Instance.tick_r1.SetActive(true);
                    }
                    else
                    {
                        LevelManager.Instance.cross_r1.SetActive(true);
                    }
                }
                else if (hitRight.collider == LevelManager.Instance.itemColliders[1])
                {
                    item = LevelManager.Instance.rightSpot[1].GetComponent<SpriteRenderer>().sprite.name.ToString();
                    if (letter == item)
                    {
                        score += 1;
                        LevelManager.Instance.scoreText.text = score.ToString();
                        LevelManager.Instance.tick_r2.SetActive(true);
                    }
                    else
                    {
                        LevelManager.Instance.cross_r2.SetActive(true);
                    }
                }
                else if (hitRight.collider == LevelManager.Instance.itemColliders[2])
                {
                    item = LevelManager.Instance.rightSpot[2].GetComponent<SpriteRenderer>().sprite.name.ToString();
                    if (letter == item)
                    {
                        score += 1;
                        LevelManager.Instance.scoreText.text = score.ToString();
                        LevelManager.Instance.tick_r3.SetActive(true);
                    }
                    else
                    {
                        LevelManager.Instance.cross_r3.SetActive(true);
                    }
                }
            }


            mousePos = Input.mousePosition;
            mousePos.z = 1;
            pos = Camera.main.ScreenToWorldPoint(mousePos);
            line.SetPosition(1, pos);
            line = null;
            currLines++;
            GameObject dot = Instantiate(dotPrefab, pos, Quaternion.identity, dotParent);
        }
        else if (Input.GetMouseButton(0) && line && LevelManager.Instance.began == true)
        {
            mousePos = Input.mousePosition;
            mousePos.z = 1;
            pos = Camera.main.ScreenToWorldPoint(mousePos);
            line.SetPosition(1, pos);
        }
#endif
        
        //TOUCH EDITED
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on TouchPhase
            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    // Record initial touch position.
                    if (LevelManager.Instance.began == true) {
                        if (line == null)
                        {
                            DrawLine();
                            touchPos = touch.position;
                            touchPos.z = 1;
                            pos = Camera.main.ScreenToWorldPoint(touchPos);
                            GameObject dot = Instantiate(dotPrefab, pos, Quaternion.identity, dotParent);
                        }
                        touchPos = touch.position;
                        touchPos.z = 1;
                        pos = Camera.main.ScreenToWorldPoint(touchPos);
                        line.SetPosition(0, pos);
                        line.SetPosition(1, pos);
                    }
                    break;

                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    if (Input.GetMouseButton(0) && line && LevelManager.Instance.began == true)
                    {
                        touchPos = touch.position;
                        touchPos.z = 1;
                        pos = Camera.main.ScreenToWorldPoint(touchPos);
                        line.SetPosition(1, pos);
                    }
                    break;

                case TouchPhase.Ended:
                    // Report that the touch has ended when it ends
                    if (line && LevelManager.Instance.began == true)
                    {
                        touchPos = touch.position;
                        touchPos.z = 1;
                        pos = Camera.main.ScreenToWorldPoint(touchPos);
                        line.SetPosition(1, pos);
                        line = null;
                        currLines++;
                        GameObject dot = Instantiate(dotPrefab, pos, Quaternion.identity, dotParent);
                    }
                    break;
            }
        }
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
