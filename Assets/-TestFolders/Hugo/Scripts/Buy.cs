using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Buy : MonoBehaviour
{
    SellGun gunToBuy;

    private GameObject spawnedGun;

    public void WhichGun(SellGun newGun)
    {
        gunToBuy = newGun;
    }

    public void BuyGun()
    {
        if (CurrencyManager.Instance.CurrentCurrency() > gunToBuy.cost && gunToBuy.hasBeenBought == false)
        {
            gunToBuy.hasBeenBought = true;
            spawnedGun = Instantiate(gunToBuy.gun, transform);
        }
    }
}
