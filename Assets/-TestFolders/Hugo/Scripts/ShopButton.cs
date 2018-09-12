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
            toggleShop = !toggleShop;
            allWeapons.SetActive(toggleShop);
            StartCoroutine(Cooldown());
        }
    }

    public IEnumerator Cooldown()
    {
        cd = false;
        yield return new WaitForSeconds(.5f);
        cd = true;
    }
}
