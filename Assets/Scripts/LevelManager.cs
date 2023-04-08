using System;
using UnityEngine;
using UnityEngine.Sprites;

public class LevelManager : MonoBehaviour
{
    //instance
    private static LevelManager Instance { set; get; }
    //private Item itemScript;

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
    public GameObject[] holderSpot;
    public int position;
    public int position2;

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
         GenerateChallenge();
         RemoveElement(ref pool_1, pool_1_Index);
         RemoveElement(ref pool_2, pool_2_Index);
         RemoveElement(ref pool_3, pool_3_Index);
    }

    // Update is called once per frame
    void Update()
    {
        
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
}