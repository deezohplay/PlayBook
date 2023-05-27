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
    public GameObject prevBtn;
    public GameObject pausedBtn;
    public GameObject coin;
    public GameObject alarm;

    //Animator
    public Animator coinAnim;
    public Animator passedAnim;
    public Animator failedAnim;

    //particles
    public ParticleSystem gift;
    //letter class
    [System.Serializable]
    public class Pool_1
    {
        public Sprite letter_1;
        public Sprite item_1;
        public AudioClip letter_1_sounds;
        public AudioClip item_1_correct;
        public AudioClip inco_1;

        public Pool_1(Sprite l_1, Sprite a_1, AudioClip ls_1, AudioClip ic_1, AudioClip i1)
        {
            letter_1 = l_1;
            item_1 = a_1;
            letter_1_sounds = ls_1;
            item_1_correct = ic_1;
            inco_1 = i1;
        }
    }

    [System.Serializable]
    public class Pool_2
    {
        public Sprite letter_2;
        public Sprite item_2;
        public AudioClip letter_2_sounds;
        public AudioClip item_2_correct;
        public AudioClip inco_2;

        public Pool_2(Sprite l_2, Sprite a_2, AudioClip ls_2, AudioClip ic_2, AudioClip i2)
        {
            letter_2 = l_2;
            item_2 = a_2;
            letter_2_sounds = ls_2;
            item_2_correct = ic_2;
            inco_2 = i2;
        }
    }

    [System.Serializable]
    public class Pool_3
    {
        public Sprite letter_3;
        public Sprite item_3;
        public AudioClip letter_3_sounds;
        public AudioClip item_3_correct;
        public AudioClip inco_3;

        public Pool_3(Sprite l_3, Sprite a_3, AudioClip ls_3, AudioClip ic_3, AudioClip i3)
        {
            letter_3 = l_3;
            item_3 = a_3;
            letter_3_sounds = ls_3;
            item_3_correct = ic_3;
            inco_3 = i3;
        }
    }
    //holders
    public SpriteRenderer[] letterHolders;
    public SpriteRenderer[] itemHolders;

    public AudioSource[] letterAudios;
    public AudioSource[] itemAudios;
    public AudioSource[] incoAudios;

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

        if (LineScript.Instance.dotCount == 6 && timer != 0) // Checks if dots are 6 which equates to 2 dots per line meaning 3 lines created
        {
            // destroys lines created during the previous attempt
            LineScript.Instance.DestroyLineInstnces();
            if (countRounds <= playRounds) {
                StartCoroutine(GenerateDelay()); // Not performing its work now ***** something wrong
            }
            else
            {
               // timeSpent = timer;

                if (LineScript.Instance.score >= minScore)
                {
                    PassedLevel();
                    StartCoroutine(GiftDelay());
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
        pool_1[pool_1_Index] = new Pool_1(pool_1[pool_1_Index].letter_1, pool_1[pool_1_Index].item_1, pool_1[pool_1_Index].letter_1_sounds, pool_1[pool_1_Index].item_1_correct, pool_1[pool_1_Index].inco_1);

        //pool_2_generation
        pool_2_Index = UnityEngine.Random.Range(0, pool_2.Length);
        pool_2[pool_2_Index] = new Pool_2(pool_2[pool_2_Index].letter_2, pool_2[pool_2_Index].item_2, pool_2[pool_2_Index].letter_2_sounds, pool_2[pool_2_Index].item_2_correct, pool_2[pool_2_Index].inco_2);

        //pool_3_generation
        pool_3_Index = UnityEngine.Random.Range(0, pool_3.Length);
        pool_3[pool_3_Index] = new Pool_3(pool_3[pool_3_Index].letter_3, pool_3[pool_3_Index].item_3, pool_3[pool_3_Index].letter_3_sounds, pool_3[pool_3_Index].item_3_correct, pool_3[pool_3_Index].inco_3);

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
                //LetterAudios
                letterAudios[0].clip = pool_1[pool_1_Index].letter_1_sounds;
                letterAudios[1].clip = pool_3[pool_3_Index].letter_3_sounds;
                letterAudios[2].clip = pool_2[pool_2_Index].letter_2_sounds;
                //incorrect
                incoAudios[0].clip = pool_1[pool_1_Index].inco_1;
                incoAudios[1].clip = pool_3[pool_3_Index].inco_3;
                incoAudios[2].clip = pool_2[pool_2_Index].inco_2;
                break;
            case 1:
                //letters
                letterHolders[0].sprite = pool_2[pool_2_Index].letter_2;
                letterHolders[1].sprite = pool_1[pool_1_Index].letter_1;
                letterHolders[2].sprite = pool_3[pool_3_Index].letter_3;
                //lettersAudio
                letterAudios[0].clip = pool_2[pool_2_Index].letter_2_sounds;
                letterAudios[1].clip = pool_1[pool_1_Index].letter_1_sounds;
                letterAudios[2].clip = pool_3[pool_3_Index].letter_3_sounds;
                //incorrect
                incoAudios[0].clip = pool_2[pool_2_Index].inco_2;
                incoAudios[1].clip = pool_1[pool_1_Index].inco_1;
                incoAudios[2].clip = pool_3[pool_3_Index].inco_3;
                break;
            case 2:
                //letters
                letterHolders[0].sprite = pool_3[pool_3_Index].letter_3;
                letterHolders[1].sprite = pool_2[pool_2_Index].letter_2;
                letterHolders[2].sprite = pool_1[pool_1_Index].letter_1;

                //lettersAudio
                letterAudios[0].clip = pool_3[pool_3_Index].letter_3_sounds;
                letterAudios[1].clip = pool_2[pool_2_Index].letter_2_sounds;
                letterAudios[2].clip = pool_1[pool_1_Index].letter_1_sounds;
                //incorrect
                incoAudios[0].clip = pool_3[pool_3_Index].inco_3;
                incoAudios[1].clip = pool_2[pool_2_Index].inco_2;
                incoAudios[2].clip = pool_1[pool_1_Index].inco_1;
                break;
        }

        switch (position2)
        {
            case 0:
                //items
                itemHolders[0].sprite = pool_3[pool_3_Index].item_3;
                itemHolders[1].sprite = pool_2[pool_2_Index].item_2;
                itemHolders[2].sprite = pool_1[pool_1_Index].item_1;
                //itemAudios
                itemAudios[0].clip = pool_3[pool_3_Index].item_3_correct;
                itemAudios[1].clip = pool_2[pool_2_Index].item_2_correct;
                itemAudios[2].clip = pool_1[pool_1_Index].item_1_correct;
                
                break;
            case 1:
                //items
                itemHolders[0].sprite = pool_1[pool_1_Index].item_1;
                itemHolders[1].sprite = pool_3[pool_3_Index].item_3;
                itemHolders[2].sprite = pool_2[pool_2_Index].item_2;
                //itemAudios
                itemAudios[0].clip = pool_1[pool_1_Index].item_1_correct;
                itemAudios[1].clip = pool_3[pool_3_Index].item_3_correct;
                itemAudios[2].clip = pool_2[pool_2_Index].item_2_correct;
                
                break;
            case 2:
                //items
                itemHolders[0].sprite = pool_1[pool_1_Index].item_1;
                itemHolders[1].sprite = pool_2[pool_2_Index].item_2;
                itemHolders[2].sprite = pool_3[pool_3_Index].item_3;
                //itemAudios
                itemAudios[0].clip = pool_1[pool_1_Index].item_1_correct;
                itemAudios[1].clip = pool_2[pool_2_Index].item_2_correct;
                itemAudios[2].clip = pool_3[pool_3_Index].item_3_correct;
                
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
        if (timer <= 0 && LineScript.Instance.score<minScore)
        {
            FailedLevel();
        }
        else
        {
            
        }
    }
    //retry level
    public void ReplayPassed()
    {
        passedAnim.SetTrigger("out");
        StartCoroutine(PassedReplayDelay());
    }
    public void ReplayFailed()
    {
        failedAnim.SetTrigger("out");
        StartCoroutine(FailedReplayDelay());
    }

    //ads bonus play
    public void AdBonus()
    {
        failedAnim.SetTrigger("out");
        StartCoroutine(DelayAddBonus());
        StartCoroutine(GenerateDelay());
    }

    //This method is invoked when the player attains a certain amount of points enough to progress to another level
    public void PassedLevel()
    {
        TimeCalc();
        LineScript.Instance.DestroyLineInstnces();
        StartCoroutine(PassedDelay());
    }

    //time calculations ** not used yet, this is the gift
    void TimeCalc()
    {
        remainingTime = 60.0f - timeSpent;
        remainingTime = Mathf.Round(remainingTime);
    }
    //win logic
    public void Next()
    {
       
    }

    //This method is invoked when the player failed to score a certain amount of point to progress to another level.
    public void FailedLevel()
    {
        LineScript.Instance.DestroyLineInstnces();
        StartCoroutine(DelayFailed());
    }
    //This creates delay at every generation of new items on the screen
    IEnumerator GenerateDelay()
    {
        yield return new WaitForSeconds(1.0f);
        GenerateChallenge();
    }

    //Creates a one second delay after replay button hit from passed panel
    IEnumerator PassedReplayDelay()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Identify");
    }

    //Creates a one second delay after replay button hit from failed panel
    IEnumerator FailedReplayDelay()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Identify");
    }
    IEnumerator DelayAddBonus()
    {
        yield return new WaitForSeconds(1.0f);
        //Enables objects since time bonus is added to the previously elapsed time
        leftSpot[0].SetActive(true);
        leftSpot[1].SetActive(true);
        leftSpot[2].SetActive(true);
        rightSpot[0].SetActive(true);
        rightSpot[1].SetActive(true);
        rightSpot[2].SetActive(true);
        pausedBtn.SetActive(true);
        prevBtn.SetActive(true);
        coin.SetActive(true);
        scoreText.gameObject.SetActive(true);
        alarm.SetActive(true);
        timer = 30.0f;
        began = true;

    }
    //creates delay on failed disabling to match the line destructions
    IEnumerator DelayFailed()
    {
        yield return new WaitForSeconds(1.0f);
        //disabling all gameobjects from the device screen
        leftSpot[0].SetActive(false);
        leftSpot[1].SetActive(false);
        leftSpot[2].SetActive(false);
        rightSpot[0].SetActive(false);
        rightSpot[1].SetActive(false);
        rightSpot[2].SetActive(false);
        pausedBtn.SetActive(false);
        prevBtn.SetActive(false);
        coin.SetActive(false);
        scoreText.gameObject.SetActive(false);
        alarm.SetActive(false);
        faileScoreText.text = LineScript.Instance.score.ToString();
        timer = 0;
        began = false;
        failedAnim.SetTrigger("in");
    }
    //creates delay on passed disabling
    IEnumerator PassedDelay()
    {
        yield return new WaitForSeconds(1.0f);
        //disabling all game objects from the screen
        leftSpot[0].SetActive(false);
        leftSpot[1].SetActive(false);
        leftSpot[2].SetActive(false);
        rightSpot[0].SetActive(false);
        rightSpot[1].SetActive(false);
        rightSpot[2].SetActive(false);
        pausedBtn.SetActive(false);
        prevBtn.SetActive(false);
        coin.SetActive(false);
        scoreText.gameObject.SetActive(false);
        alarm.SetActive(false);
        passedScoreText.text = LineScript.Instance.score.ToString();
        began = false;
        passedAnim.SetTrigger("in");
    }
    IEnumerator GiftDelay()
    {
        yield return new WaitForSeconds(2.0f);
        gift.Play();
    }
}
