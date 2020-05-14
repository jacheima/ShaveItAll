using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("World")]
    public World world;

    [Header("UI Elements")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI moneyText;

    [Header("Timer")]
    //world.level[currentLevel].timePerCustomer
    public float timer;

    [Header("Level")]
    public List<GameObject> beardPieces;
    public int currentCustomer;
    public int currentLevel;
    public Transform headTransform;
    //level.customers.Length

    public int finishedCustomers;

    [Header("Player Stats")]
    public float paidUpFront;
    public float tipsEarned;
    public float totalMoney;

    [Header("Game States")]
    public bool isPaused = true;

    public int cuts;

    void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        SetupLevel();
    }

    void SetupLevel()
    {
        timer = world.levels[currentLevel].timePerCustomer;
        currentCustomer = 0;

        SpawnCustomer();
    }

    void SpawnCustomer()
    {
        GameObject customer = Instantiate(world.levels[currentLevel].customers[currentCustomer].prefab, headTransform.position, Quaternion.identity);
        customer.transform.SetParent(headTransform);

        Beard[] beard = customer.GetComponentsInChildren<Beard>();

        for (int i = 0; i < beard.Length; i++)
        {
            beardPieces.Add(beard[i].gameObject);
        }
    }

    private void Update()
    {
 

        if (!isPaused)
        {
            timer -= Time.deltaTime;

            float minutes = world.levels[currentLevel].timePerCustomer / 60f;
            float seconds = world.levels[currentLevel].timePerCustomer % 60f;

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            moneyText.text = "$" + totalMoney;
        }

        if (timer < 1)
        {
            isPaused = true;
            CheckScore();
        }

        if (cuts >= world.levels[currentLevel].customers[currentCustomer].killThreshold)
        {
            Debug.Log(cuts);
            world.levels[currentLevel].customers[currentCustomer].isAlive = false;
            Debug.Log("You've killed your customer");
        }
    }

    void CheckScore()
    {
        if (currentCustomer > world.levels[currentLevel].customers.Length)
        {

        }
        else
        {
            //check to see if customer is alive
            if (world.levels[currentLevel].customers[currentCustomer].isAlive)
            {
                //if so...check for refund conditions
                float nullCount = 0;

                for (int i = 0; i < beardPieces.Count; i++)
                {
                    if (beardPieces[i] == null)
                    {
                        nullCount++;
                    }
                }

                float piecesRemaining = beardPieces.Count - nullCount;
                float percentRemaining = piecesRemaining / beardPieces.Count;

                if (percentRemaining > world.levels[currentLevel].customers[currentCustomer].refundThreshold)
                {
                    //if refund...subtract money from the player money
                    totalMoney -= world.levels[currentLevel].customers[currentCustomer].willPay;
                }
                else
                {
                    //if not ...calculate tip
                    tipsEarned = CalculateTip(percentRemaining);
                    totalMoney += tipsEarned;
                    tipsEarned = 0;
                }
            }
        }
    }
    float CalculateTip(float percentRemainging)
    {
        float tip = 0;

        float percentRemoved = 1 - percentRemainging;

        if (percentRemoved < world.levels[currentLevel].customers[currentCustomer].refundThreshold)
        {
            tip = world.levels[currentLevel].customers[currentCustomer].willPay * world.levels[currentLevel].customers[currentCustomer].tipModifier;
        }

        return tip;
    }

    public void DestroyBeardPiece(GameObject beardPiece)
    {
        for (int i = 0; i < beardPieces.Count; i++)
        {
            if (beardPiece == beardPieces[i])
            {
                beardPieces[i] = null;
                beardPiece.GetComponent<Renderer>().enabled = false;
            }
        }
    }
}
