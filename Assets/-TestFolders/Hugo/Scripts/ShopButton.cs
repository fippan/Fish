using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public GameObject allWeapons;

    private bool toggleShop = false;
    private bool canOpen = true;
    private Coroutine autoClose;

    public void ToggleShop ()
    {
        toggleShop = !toggleShop;
        Shop();
    }

    private void Shop ()
    {
        if (toggleShop)
        {
            if (canOpen)
            {
                canOpen = false;
                allWeapons.SetActive(true);
                autoClose = StartCoroutine(AutoClose());
                StartCoroutine(Cooldown());
            }
        }
        else
        {
            allWeapons.SetActive(false);
            StopCoroutine(autoClose);
        }
    }

    public IEnumerator AutoClose()
    {
        yield return new WaitForSeconds(10);
        toggleShop = false;
        Shop();
    }

    public IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1.5f);
        canOpen = true;
    }
}
