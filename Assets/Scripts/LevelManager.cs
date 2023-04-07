using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Sprites;
using TMPro;

public class LevelManager : MonoBehaviour
{
    private static LevelManager Instance { set; get; }
    //class arrays
    public Sprite[] letters;
    public Sprite[] objects;

    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
