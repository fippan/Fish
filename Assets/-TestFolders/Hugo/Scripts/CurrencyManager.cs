using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    private float currentCurrency = 200f;



    public static CurrencyManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public void AddCurrency (float loot)
    {
        currentCurrency += loot;
    }

    public void RemoveCurrency (float cost)
    {
        currentCurrency -= cost;
    }

    public float CurrentCurrency()
    {
        return currentCurrency;
    }
}
