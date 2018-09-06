using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaterContact : MonoBehaviour
{
    FishingRod fR;

    public void Start()
    {
        fR = FindObjectOfType<FishingRod>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
        }
    }
}
