using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.UI;

public class SellGun : MonoBehaviour
{
    public Transform shopCannonBarrelEnd;
    public GameObject gun;
    public float cost;
    public Text costText;

    public ShopButton button;

    public void Start()
    {
        costText.text = ("Cost: " + cost);
    }

    public void Sell ()
    {
        if (CurrencyManager.Instance.CurrentCurrency() >= cost)
        {
            CurrencyManager.Instance.RemoveCurrency(cost);
            GameObject newGun = Instantiate(gun, shopCannonBarrelEnd.position, shopCannonBarrelEnd.rotation);
            shopCannonBarrelEnd.GetComponentInParent<ShopCannon>().Shoot(newGun);
            button.CloseShop();
        }
    }
}
