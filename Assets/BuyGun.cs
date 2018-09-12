using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BuyGun : MonoBehaviour
{
    SellGun gunToBuy;

    private bool readyToBuy = false;

    public void GetGunToBuy(SellGun newGun)
    {
        gunToBuy = newGun;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shop")
        {
            Debug.Log("Test");
            readyToBuy = true;
            //other.gameObject.GetComponent<SellGun>().Buy(GetComponent<VRTK_InteractGrab>());
        }
    }
}
