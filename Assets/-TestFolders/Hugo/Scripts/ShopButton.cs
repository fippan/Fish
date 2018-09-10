using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public GameObject allWeapons;

    private bool toggleShop = false;

    public void OnTriggerEnter(Collider other)
    {
        toggleShop = !toggleShop;
        allWeapons.SetActive(toggleShop);
    }
}
