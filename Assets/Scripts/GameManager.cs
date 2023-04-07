using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Game Manager Instance
    public static GameManager Instance { set; get; }

    //Initialized before the game starts
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(Instance);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("One");
    }
}
