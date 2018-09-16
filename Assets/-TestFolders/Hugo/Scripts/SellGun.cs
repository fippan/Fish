using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.UI;

public class SellGun : MonoBehaviour
{
    public GameObject gun;
    public float cost;
    public bool beenBought = false;
    public Text costText;

    public void Start()
    {
        costText.text = ("Cost: " + cost);
    }

    public void Sell ()
    {
        if (CurrencyManager.Instance.CurrentCurrency() > cost && beenBought == false)
        {
            GameObject newGun = Instantiate(gun, transform.position, transform.rotation);
            beenBought = true;
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
