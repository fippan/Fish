using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyGun : MonoBehaviour
{
    public GameObject gun;
    public float cost = 100f;

    public void Buy()
    {
        if (CurrencyManager.Instance.CurrentCurrency() >= cost)
        {
            Instantiate(gun);
            CurrencyManager.Instance.RemoveCurrency(cost);
        }
    }
}
