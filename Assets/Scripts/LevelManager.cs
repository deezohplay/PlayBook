using System;
using UnityEngine;
using UnityEngine.Sprites;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //countplaytimes
    private int countRounds;
    private int minScore;
    private int playRounds;
    private float timeSpent;
    private float remainingTime;
    //instance
    public static LevelManager Instance { set; get; }
    //time var
    public float timer;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI faileScoreText;
    public TextMeshProUGUI passedScoreText;

    public bool began = false;
    private float timerSpeed = 0.6f;

    //panels
    public GameObject failed;
    public GameObject prevBtn;
    public GameObject pausedBtn;
    public GameObject coin;
    public GameObject alarm;
    public GameObject passed;

    //letter class
    [System.Serializable]
    public class Pool_1
    {
        public Sprite letter_1;
        public Sprite item_1;

        public Pool_1(Sprite l_1, Sprite a_1)
        {
            letter_1 = l_1;
            item_1 = a_1;
        }
    }

    [System.Serializable]
    public class Pool_2
    {
        public Sprite letter_2;
        public Sprite item_2;

        public Pool_2(Sprite l_2, Sprite a_2)
        {
            letter_2 = l_2;
            item_2 = a_2;
        }
    }

    [System.Serializable]
    public class Pool_3
    {
        public Sprite letter_3;
        public Sprite item_3;

        public Pool_3(Sprite l_3, Sprite a_3)
        {
            letter_3 = l_3;
            item_3 = a_3;
        }
    }
    //holders
    public SpriteRenderer[] letterHolders;
    public SpriteRenderer[] itemHolders;

    //positions
    public GameObject[] leftSpot;
    public GameObject[] rightSpot;
    public Collider[] letterColliders;
    public Collider[] itemColliders;
    public int position;
    public int position2;

    //Ticks or Wrong
    public GameObject tick_r1;
    public GameObject cross_r1;

    public GameObject tick_r2;
    public GameObject cross_r2;

    public GameObject tick_r3;
    public GameObject cross_r3;

    //class arrays
    public Pool_1[] pool_1;
    public Pool_2[] pool_2;
    public Pool_3[] pool_3;

    //pool indices
    public int pool_1_Index;
    public int pool_2_Index;
    public int pool_3_Index;

    //spawnCount

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        playRounds = 6;
        timer = 60.0f;
        GenerateChallenge();
        began = true;
        countRounds = 0;
        minScore = 100;
        timeSpent = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (began == true)
        {
            StartCoroutine(LittlelDelayOnGamePlay());
        }

        if (LineScript.Instance.dotCount == 6 && timer != 0)
        {
            LineScript.Instance.DestroyLineInstnces();
            if (countRounds <= playRounds) {
                GenerateChallenge();
            }
            else
            {
                timeSpent = timer;

                if (LineScript.Instance.score >= minScore)
                {
                    PassedLevel();
                }
                else
                {
                    FailedLevel();
                }
            }
        }
    }

    //Generate Indices
    public void GenerateChallenge()
    {
        //pool_1_generation
        pool_1_Index = UnityEngine.Random.Range(0, pool_1.Length);
        pool_1[pool_1_Index] = new Pool_1(pool_1[pool_1_Index].letter_1, pool_1[pool_1_Index].item_1);

        //pool_2_generation
        pool_2_Index = UnityEngine.Random.Range(0, pool_2.Length);
        pool_2[pool_2_Index] = new Pool_2(pool_2[pool_2_Index].letter_2, pool_2[pool_2_Index].item_2);

        //pool_3_generation
        pool_3_Index = UnityEngine.Random.Range(0, pool_3.Length);
        pool_3[pool_3_Index] = new Pool_3(pool_3[pool_3_Index].letter_3, pool_3[pool_3_Index].item_3);

        position = UnityEngine.Random.Range(0,letterHolders.Length);
        position2 = UnityEngine.Random.Range(0, itemHolders.Length);

        // answer position
        switch (position)
        {  
            case 0:
                //letters
                letterHolders[0].sprite = pool_1[pool_1_Index].letter_1;
                letterHolders[1].sprite = pool_3[pool_3_Index].letter_3;
                letterHolders[2].sprite = pool_2[pool_2_Index].letter_2;
                break;
            case 1:
                //letters
                letterHolders[0].sprite = pool_2[pool_2_Index].letter_2;
                letterHolders[1].sprite = pool_1[pool_1_Index].letter_1;
                letterHolders[2].sprite = pool_3[pool_3_Index].letter_3;
                break;
            case 2:
                //letters
                letterHolders[0].sprite = pool_3[pool_3_Index].letter_3;
                letterHolders[1].sprite = pool_2[pool_2_Index].letter_2;
                letterHolders[2].sprite = pool_1[pool_1_Index].letter_1;
                break;
        }

        switch (position2)
        {
            case 0:
                //items
                itemHolders[0].sprite = pool_3[pool_3_Index].item_3;
                itemHolders[1].sprite = pool_2[pool_2_Index].item_2;
                itemHolders[2].sprite = pool_1[pool_1_Index].item_1;
                break;
            case 1:
                //items
                itemHolders[0].sprite = pool_1[pool_1_Index].item_1;
                itemHolders[1].sprite = pool_3[pool_3_Index].item_3;
                itemHolders[2].sprite = pool_2[pool_2_Index].item_2;
                break;
            case 2:
                //items
                itemHolders[0].sprite = pool_1[pool_1_Index].item_1;
                itemHolders[1].sprite = pool_2[pool_2_Index].item_2;
                itemHolders[2].sprite = pool_3[pool_3_Index].item_3;
                break;
        }

        RemoveElement(ref pool_1, pool_1_Index);
        RemoveElement(ref pool_2, pool_2_Index);
        RemoveElement(ref pool_3, pool_3_Index);
        countRounds++;
    }

    //Remove element after it has been answered
    public void RemoveElement<T>(ref T[] arr, int index)
    {
        for (int i = index; i < arr.Length - 1; i++)
        {
            arr[i] = arr[i + 1];
        }
        Array.Resize(ref arr, arr.Length - 1);
    }

    IEnumerator LittlelDelayOnGamePlay()
    {
        yield return new WaitForSeconds(1.0f);
        timer -= Time.deltaTime * timerSpeed; // Substracts 1 second
        //Converts the time into a whole number
        timerText.SetText("" + Mathf.Round(timer));
        //Checks if time is less than 0 and triggers game over method
        if (timer <= 0)
        {
            FailedLevel();
        }
    }

    //retry level
    public void Replay()
    {
        //disabling gameobjects
        failed.SetActive(false);
        leftSpot[0].SetActive(true);
        leftSpot[1].SetActive(true);
        leftSpot[2].SetActive(true);
        rightSpot[0].SetActive(true);
        rightSpot[1].SetActive(true);
        rightSpot[2].SetActive(true);
        pausedBtn.SetActive(true);
        prevBtn.SetActive(true);
        coin.SetActive(true);
        alarm.SetActive(true);
        SceneManager.LoadScene("Identify");
    }

    //ads bonus play
    public void AdBonus()
    {
        failed.SetActive(false);
        leftSpot[0].SetActive(true);
        leftSpot[1].SetActive(true);
        leftSpot[2].SetActive(true);
        rightSpot[0].SetActive(true);
        rightSpot[1].SetActive(true);
        rightSpot[2].SetActive(true);
        pausedBtn.SetActive(true);
        prevBtn.SetActive(true);
        coin.SetActive(true);
        alarm.SetActive(true);
        timer = 30.0f;
        GenerateChallenge();
        began = true;
    }

    //runs if the play is over
    public void PassedLevel()
    {
        TimeCalc();
        //disabling gameobjects
        passed.SetActive(true);
        leftSpot[0].SetActive(false);
        leftSpot[1].SetActive(false);
        leftSpot[2].SetActive(false);
        rightSpot[0].SetActive(false);
        rightSpot[1].SetActive(false);
        rightSpot[2].SetActive(false);
        pausedBtn.SetActive(false);
        prevBtn.SetActive(false);
        coin.SetActive(false);
        alarm.SetActive(false);
        passedScoreText.text = LineScript.Instance.score.ToString();
        LineScript.Instance.DestroyLineInstnces();
        began = false;
    }

    //time calculations
    void TimeCalc()
    {
        remainingTime = 60.0f - timeSpent;
        remainingTime = Mathf.Round(remainingTime);
        Debug.Log(remainingTime);
    }
    //win logic
    public void Next()
    {
       
    }

    //failed method
    public void FailedLevel()
    {
        //disabling gameobjects
        failed.SetActive(true);
        leftSpot[0].SetActive(false);
        leftSpot[1].SetActive(false);
        leftSpot[2].SetActive(false);
        rightSpot[0].SetActive(false);
        rightSpot[1].SetActive(false);
        rightSpot[2].SetActive(false);
        pausedBtn.SetActive(false);
        prevBtn.SetActive(false);
        coin.SetActive(false);
        alarm.SetActive(false);
        LineScript.Instance.DestroyLineInstnces();
        faileScoreText.text = LineScript.Instance.score.ToString();
        timer = 0;
        began = false;
    }

}
