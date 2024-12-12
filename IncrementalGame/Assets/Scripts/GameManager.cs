using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
    public float coinAmount = 0;
    public float currentStrength;

    [SerializeField] private TMP_Text coinAmountText;
    [SerializeField] private TMP_Text strengthText;
    public List<Upgrade> upgrades = new List<Upgrade>();

    long previousTicks;

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadGameData();

    }

    private void Update()
    {
        coinAmountText.text = "Coins: " + string.Format("{0:0.0}", coinAmount);
        strengthText.text = "Per second: " + string.Format("{0:0.0}", currentStrength);

        CoinsPerSecond();
    }

    float timer;
    private void CoinsPerSecond()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            coinAmount += currentStrength;
            timer = 0;
            SaveGameData();
        }
    }

    public void BuyUpgrade(Upgrade upgrade)
    {
        if (coinAmount >= upgrade.cost)
        {
            coinAmount += -upgrade.cost;
            currentStrength += upgrade.strength;
            upgrade.amount++;
            upgrade.cost = upgrade.baseCost * Mathf.Pow(1.15f, upgrade.amount);

            foreach(Upgrade item in upgrades)
            {
                if (item == upgrade)
                {
                    PlayerPrefs.SetFloat("costOf" + item.name, item.cost);
                    PlayerPrefs.SetFloat("amountOf" + item.name, item.amount);
                }
            }
        }
    }

    public void ClickBank()
    {
        coinAmount++;
    }

    private void SaveGameData()
    {
        foreach (Upgrade item in upgrades)
        {
            PlayerPrefs.SetFloat("costOf" + item.name, item.cost);
            PlayerPrefs.SetFloat("amountOf" + item.name, item.amount);
        }
        PlayerPrefs.SetString("time", System.DateTime.Now.Ticks.ToString());
        PlayerPrefs.SetFloat("currentStrength", currentStrength);
        PlayerPrefs.SetFloat("coinAmount", coinAmount);
    }

    private void LoadGameData()
    {
        foreach (Upgrade item in upgrades)
        {
            item.cost = PlayerPrefs.GetFloat("costOf" + item.name, item.cost);
            item.amount = PlayerPrefs.GetFloat("amountOf" + item.name, item.amount);
        }
        currentStrength = PlayerPrefs.GetFloat("currentStrength");
        coinAmount = PlayerPrefs.GetFloat("coinAmount");

        string prevTicksString = PlayerPrefs.GetString("time", string.Empty);

        if (!string.IsNullOrEmpty(prevTicksString))
            previousTicks = long.Parse(prevTicksString);

        double secondsPassed = System.TimeSpan.FromTicks(System.DateTime.Now.Ticks - previousTicks).TotalSeconds;
        coinAmount += currentStrength * (float)secondsPassed;
    }
}

