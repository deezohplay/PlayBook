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
    //Load Scene
    public void LoadScene()
    {
        SceneManager.LoadScene("Identify");
    }
    //exits Game
    public void QuitGame()
    {
        Application.Quit();
    }

    //mute sounds
    public void MuteSounds()
    {

    }
}
