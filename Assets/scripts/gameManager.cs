using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class gameManager : MonoBehaviour
{
    
    public GameObject card;
    public GameObject endText;
    public Text timeTaxt;
    float time;
    public static gameManager I;
    public GameObject firstCard;
    public GameObject secondCard;
    public AudioSource audioSource;
    public AudioClip match;

    void Awake()
    {
        I = this;
    }

    void Start()
    {
        Time.timeScale = 1.0f;

        int[] rtans = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();


        for (int i = 0; i < 16; i++)
        {
           
            GameObject newCard = Instantiate(card);
            newCard.transform.parent = GameObject.Find("Cards").transform;

            float x = (i / 4) * 1.4f -2.1f;
            float y = (i % 4) * 1.4f - 3.0f;
            newCard.transform.position = new Vector3(x, y, 0);

            string rtanName = "rtan" + rtans[i].ToString();
            newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName);

        }

    }

    public void isMatched()
    {
       

        string firstCardImage = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;

        if (firstCardImage == secondCardImage)
        {
            firstCard.GetComponent<card>().destroyCard();
            secondCard.GetComponent<card>().destroyCard();
            audioSource.PlayOneShot(match);

            int cardsLeft = GameObject.Find("Cards").transform.childCount;
            if (cardsLeft == 2)
            {
             
                Time.timeScale = 0.0f;
                endText.SetActive(true);
                
            }
        }
        else
        {
            firstCard.GetComponent<card>().closeCard();
            secondCard.GetComponent<card>().closeCard();
        }

        firstCard = null;
        secondCard = null;
    }



    void Update()
    {
        time += Time.deltaTime;
        timeTaxt.text = time.ToString("N2");
        if (time >= 30f)
        {
            Time.timeScale = 0.0f;
            endText.SetActive(true);
        }
    }
}
