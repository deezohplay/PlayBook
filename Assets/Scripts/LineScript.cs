using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using System;
using TMPro;

public class LineScript : MonoBehaviour
{
    //lines to be created
    private GameObject line0;
    private GameObject line1;
    private GameObject line2;
    //primary dots
    private GameObject dot1;
    private GameObject dot2;
    //new dots
    private GameObject dot11;
    private GameObject dot12;
    private GameObject dot21;
    private GameObject dot22;
    private GameObject dot31;
    private GameObject dot33;


    public static LineScript Instance { set; get;}
    //RAYCASTS
    private RaycastHit hitLeft;
    private RaycastHit hitRight;

    //pool indices
    private string letter;
    private string item;
    public int score;
    private AudioClip letterClips;
    private AudioClip itemClips;
    private AudioClip incoClips;

    public Vector3 touchPos;

    [Header("Dots")]
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] Transform dotParent;

    private LineRenderer line;
    private Vector3 mousePos;
    private Vector3 pos;
    public Material material;
    public int currLines = 0;
    public int dotCount;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        score = 0;
        dotCount = 0;
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
                    letterClips = LevelManager.Instance.letterAudios[0].GetComponent<AudioSource>().clip;
                    incoClips = LevelManager.Instance.incoAudios[0].GetComponent<AudioSource>().clip;
                    LevelManager.Instance.letterAudios[0].PlayOneShot(letterClips, 1.0f);
                    hitLeft.collider.enabled = false;
                }else if (hitLeft.collider == LevelManager.Instance.letterColliders[1])
                {
                    letter = LevelManager.Instance.leftSpot[1].GetComponent<SpriteRenderer>().sprite.name.ToString();
                    letterClips = LevelManager.Instance.letterAudios[1].GetComponent<AudioSource>().clip;
                    incoClips = LevelManager.Instance.incoAudios[1].GetComponent<AudioSource>().clip;
                    LevelManager.Instance.letterAudios[1].PlayOneShot(letterClips, 1.0f);
                    hitLeft.collider.enabled = false;
                }
                else if (hitLeft.collider == LevelManager.Instance.letterColliders[2])
                {
                    letter = LevelManager.Instance.leftSpot[2].GetComponent<SpriteRenderer>().sprite.name.ToString();
                    letterClips = LevelManager.Instance.letterAudios[2].GetComponent<AudioSource>().clip;
                    incoClips = LevelManager.Instance.incoAudios[2].GetComponent<AudioSource>().clip;
                    LevelManager.Instance.letterAudios[2].PlayOneShot(letterClips, 1.0f);
                    hitLeft.collider.enabled = false;
                }
            }

            if (line == null && hitLeft.collider)
            { 
                DrawLine();
                mousePos = Input.mousePosition;
                mousePos.z = 1;
                pos = Camera.main.ScreenToWorldPoint(mousePos);
                dot1 = Instantiate(dotPrefab, pos, Quaternion.identity, dotParent);
                dotCount++;
               
                
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
                    itemClips = LevelManager.Instance.itemAudios[0].GetComponent<AudioSource>().clip;
                    
                    hitRight.collider.enabled = false;
                    if (letter == item)
                    {
                        score += 4;
                        LevelManager.Instance.itemAudios[0].PlayOneShot(itemClips, 1.0f);
                        LevelManager.Instance.scoreText.text = score.ToString();
                        LevelManager.Instance.tick_r1.SetActive(true);
                        
                    }
                    else
                    {
                        LevelManager.Instance.cross_r1.SetActive(true);
                        LevelManager.Instance.incoAudios[0].PlayOneShot(incoClips, 1.0f);
                        StartCoroutine(GameOverDelay());
                    }
                }
                else if (hitRight.collider == LevelManager.Instance.itemColliders[1])
                {
                    item = LevelManager.Instance.rightSpot[1].GetComponent<SpriteRenderer>().sprite.name.ToString();
                    itemClips = LevelManager.Instance.itemAudios[1].GetComponent<AudioSource>().clip;
                 
                    hitRight.collider.enabled = false;
                    if (letter == item)
                    {
                        score += 4;
                        LevelManager.Instance.itemAudios[1].PlayOneShot(itemClips, 1.0f);
                        LevelManager.Instance.scoreText.text = score.ToString();
                        LevelManager.Instance.tick_r2.SetActive(true);
                    }
                    else
                    {
                        LevelManager.Instance.cross_r2.SetActive(true);
                        LevelManager.Instance.incoAudios[1].PlayOneShot(incoClips, 1.0f);
                        StartCoroutine(GameOverDelay());
                    }
                }
                else if (hitRight.collider == LevelManager.Instance.itemColliders[2])
                {
                    item = LevelManager.Instance.rightSpot[2].GetComponent<SpriteRenderer>().sprite.name.ToString();
                    itemClips = LevelManager.Instance.itemAudios[2].GetComponent<AudioSource>().clip;
                   
                    hitRight.collider.enabled = false;
                    if (letter == item)
                    {
                        score += 4;
                        LevelManager.Instance.itemAudios[2].PlayOneShot(itemClips, 1.0f);
                        LevelManager.Instance.scoreText.text = score.ToString();
                        LevelManager.Instance.tick_r3.SetActive(true);
                    }
                    else
                    {
                        LevelManager.Instance.cross_r3.SetActive(true);
                        LevelManager.Instance.incoAudios[2].PlayOneShot(incoClips, 1.0f);
                        StartCoroutine(GameOverDelay());
                    }
                }
            }

            mousePos = Input.mousePosition;
            mousePos.z = 1;
            pos = Camera.main.ScreenToWorldPoint(mousePos);
            line.SetPosition(1, pos);
            line = null;
            dot2 = Instantiate(dotPrefab, pos, Quaternion.identity, dotParent);
            if (currLines == 0)
            {
                line0 = FindObjectOfType<LineRenderer>().gameObject;
                dot11 = dot1;
                dot12 = dot2;
            }
            else if (currLines == 1)
            {
                line1 = FindObjectOfType<LineRenderer>().gameObject;
                dot21 = dot1;
                dot22 = dot2;
            }
            else if (currLines == 2)
            {
                line2 = FindObjectOfType<LineRenderer>().gameObject;
                dot31 = dot1;
                dot33 = dot2;
            }
            currLines++;
            dotCount++;

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
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                        //feeback from hit
                        if (Physics.Raycast(ray, out hitLeft))
                        {
                            if (hitLeft.collider == LevelManager.Instance.letterColliders[0])
                            {
                                letter = LevelManager.Instance.leftSpot[0].GetComponent<SpriteRenderer>().sprite.name.ToString();
                                letterClips = LevelManager.Instance.letterAudios[0].GetComponent<AudioSource>().clip;
                                incoClips = LevelManager.Instance.incoAudios[0].GetComponent<AudioSource>().clip;
                                LevelManager.Instance.letterAudios[0].PlayOneShot(letterClips, 1.0f);
                                hitLeft.collider.enabled = false;
                            }
                            else if (hitLeft.collider == LevelManager.Instance.letterColliders[1])
                            {
                                letter = LevelManager.Instance.leftSpot[1].GetComponent<SpriteRenderer>().sprite.name.ToString();
                                letterClips = LevelManager.Instance.letterAudios[1].GetComponent<AudioSource>().clip;
                                incoClips = LevelManager.Instance.incoAudios[1].GetComponent<AudioSource>().clip;
                                LevelManager.Instance.letterAudios[1].PlayOneShot(letterClips, 1.0f);
                                hitLeft.collider.enabled = false;
                            }
                            else if (hitLeft.collider == LevelManager.Instance.letterColliders[2])
                            {
                                letter = LevelManager.Instance.leftSpot[2].GetComponent<SpriteRenderer>().sprite.name.ToString();
                                letterClips = LevelManager.Instance.letterAudios[2].GetComponent<AudioSource>().clip;
                                incoClips = LevelManager.Instance.incoAudios[2].GetComponent<AudioSource>().clip;
                                LevelManager.Instance.letterAudios[2].PlayOneShot(letterClips, 1.0f);
                                hitLeft.collider.enabled = false;
                            }
                        }
                        if (line == null && hitLeft.collider)
                        {
                            DrawLine();
                            touchPos = touch.position;
                            touchPos.z = 1;
                            pos = Camera.main.ScreenToWorldPoint(touchPos);
                            dot1 = Instantiate(dotPrefab, pos, Quaternion.identity, dotParent);
                            dotCount++;
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
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                        //feeback from hit
                        if (Physics.Raycast(ray, out hitRight))
                        {
                            if (hitRight.collider == LevelManager.Instance.itemColliders[0])
                            {
                                item = LevelManager.Instance.rightSpot[0].GetComponent<SpriteRenderer>().sprite.name.ToString();
                                itemClips = LevelManager.Instance.itemAudios[0].GetComponent<AudioSource>().clip;
                                
                                hitRight.collider.enabled = false;
                                if (letter == item)
                                {
                                    score += 2;
                                    LevelManager.Instance.itemAudios[0].PlayOneShot(itemClips, 1.0f);
                                    LevelManager.Instance.scoreText.text = score.ToString();
                                    LevelManager.Instance.tick_r1.SetActive(true);
                                }
                                else
                                {
                                    LevelManager.Instance.cross_r1.SetActive(true);
                                    LevelManager.Instance.incoAudios[0].PlayOneShot(incoClips, 1.0f);
                                }
                            }
                            else if (hitRight.collider == LevelManager.Instance.itemColliders[1])
                            {
                                item = LevelManager.Instance.rightSpot[1].GetComponent<SpriteRenderer>().sprite.name.ToString();
                                itemClips = LevelManager.Instance.itemAudios[1].GetComponent<AudioSource>().clip;
                              
                                hitRight.collider.enabled = false;
                                if (letter == item)
                                {
                                    score += 2;
                                    LevelManager.Instance.itemAudios[1].PlayOneShot(itemClips, 1.0f);
                                    LevelManager.Instance.scoreText.text = score.ToString();
                                    LevelManager.Instance.tick_r2.SetActive(true);
                                }
                                else
                                {
                                    LevelManager.Instance.cross_r2.SetActive(true);
                                    LevelManager.Instance.incoAudios[1].PlayOneShot(incoClips, 1.0f);
                                }
                            }
                            else if (hitRight.collider == LevelManager.Instance.itemColliders[2])
                            {
                                item = LevelManager.Instance.rightSpot[2].GetComponent<SpriteRenderer>().sprite.name.ToString();
                                itemClips = LevelManager.Instance.itemAudios[2].GetComponent<AudioSource>().clip;
                                
                                hitRight.collider.enabled = false;
                                if (letter == item)
                                {
                                    score += 2;
                                    LevelManager.Instance.itemAudios[2].PlayOneShot(itemClips, 1.0f);
                                    LevelManager.Instance.scoreText.text = score.ToString();
                                    LevelManager.Instance.tick_r3.SetActive(true);
                                }
                                else
                                {
                                    LevelManager.Instance.cross_r3.SetActive(true);
                                    LevelManager.Instance.incoAudios[2].PlayOneShot(incoClips, 1.0f);
                                }
                            }
                        }
                        touchPos = touch.position;
                        touchPos.z = 1;
                        pos = Camera.main.ScreenToWorldPoint(touchPos);
                        line.SetPosition(1, pos);
                        line = null;
                        dot2 = Instantiate(dotPrefab, pos, Quaternion.identity, dotParent);
                        if (currLines == 0)
                        {
                            line0 = FindObjectOfType<LineRenderer>().gameObject;
                            dot11 = dot1;
                            dot12 = dot2;
                        }
                        else if (currLines == 1)
                        {
                            line1 = FindObjectOfType<LineRenderer>().gameObject;
                            dot21 = dot1;
                            dot22 = dot2;
                        }
                        else if (currLines == 2)
                        {
                            line2 = FindObjectOfType<LineRenderer>().gameObject;
                            dot31 = dot1;
                            dot33 = dot2;
                        }
                        currLines++;
                        dotCount++;
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

    public void DestroyLineInstnces()
    {
        StartCoroutine(DestroyDelay());

        //Enabling colliders
        EnableCollider();
        //resetting
        dotCount = 0;
        currLines = 0;
    }
    public void EnableCollider()
    {
        //left
        LevelManager.Instance.letterColliders[0].enabled = true;
        LevelManager.Instance.letterColliders[1].enabled = true;
        LevelManager.Instance.letterColliders[2].enabled = true;

        //right
        LevelManager.Instance.itemColliders[0].enabled = true;
        LevelManager.Instance.itemColliders[1].enabled = true;
        LevelManager.Instance.itemColliders[2].enabled = true;
    }

    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(1.0f);
        //destroy dots
        Destroy(dot11);
        Destroy(dot12);
        Destroy(dot21);
        Destroy(dot22);
        Destroy(dot31);
        Destroy(dot33);
        //Destroying lines
        Destroy(line0);
        Destroy(line1);
        Destroy(line2);

        //disable ticks
        LevelManager.Instance.tick_r1.SetActive(false);
        LevelManager.Instance.tick_r2.SetActive(false);
        LevelManager.Instance.tick_r3.SetActive(false);

        //disable cross
        LevelManager.Instance.cross_r1.SetActive(false);
        LevelManager.Instance.cross_r2.SetActive(false);
        LevelManager.Instance.cross_r3.SetActive(false);
    }
    IEnumerator GameOverDelay()
    {
        DestroyLineInstnces();
        yield return new WaitForSeconds (1.0f);
        LevelManager.Instance.GameOver();
    }

}
