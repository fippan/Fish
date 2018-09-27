using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoughtFish : MonoBehaviour
{
    FishingRod fishingRod;

    public void Start()
    {
        fishingRod = FindObjectOfType<FishingRod>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FinishBox")
        {
            //fishingRod.closeEnough = true;
        }
    }
}
