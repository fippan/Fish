using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public GameObject allWeapons;

    private bool toggleShop = false;
    private bool cd = true;

    private float buttonCD = 2f;
    private float currentCD = 0f;

    public void OnTriggerEnter(Collider other)
    {
        if (cd)
        {
            ToggleShop();
            StartCoroutine(AutoClose());
        }
    }

    private void ToggleShop ()
    {
        toggleShop = !toggleShop;
        allWeapons.SetActive(toggleShop);
        StartCoroutine(Cooldown());
        if (toggleShop == false)
        {
            StopCoroutine(AutoClose());
        }
    }

    public IEnumerator Cooldown()
    {
        cd = false;
        yield return new WaitForSeconds(.5f);
        cd = true;
    }

    public IEnumerator AutoClose ()
    {
        yield return new WaitForSeconds(5);
        if (toggleShop)
        {
            ToggleShop();
        }
    }
}
