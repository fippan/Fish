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
<<<<<<< HEAD
            GameObject newGun = Instantiate(gun, transform.position, transform.rotation);
            button.CloseShop();
=======
            GameObject newGun = Instantiate(gun, shopCannonBarrelEnd.position, shopCannonBarrelEnd.rotation);
            shopCannonBarrelEnd.GetComponentInParent<ShopCannon>().Shoot(newGun);
            button.ToggleShop();
>>>>>>> 575cb6e07de43113bf5bc20484d48392e365a7c6
        }
    }
}
