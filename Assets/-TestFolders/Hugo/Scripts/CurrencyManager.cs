using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    public Text money;

    [SerializeField]
    private float currentCurrency = 0;
    private float totalCurrency = 0;

    private string moneyString = "Current money: ";

    public static CurrencyManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        money.text = moneyString + currentCurrency;
    }

    public void AddCurrency (float loot)
    {
        if (currentCurrency > 100)
        {
            DayAndNightCycle.Instance.TimeMultiplier = 688;
        }

        currentCurrency += loot;
        totalCurrency += loot;
        money.text = moneyString + currentCurrency;
    }

    public void RemoveCurrency (float cost)
    {
        currentCurrency -= cost;
        money.text = moneyString + currentCurrency;
    }

    public float CurrentCurrency ()
    {
        return currentCurrency;
    }

    public float GetTotalCurrency ()
    {
        return totalCurrency;
    }
}
