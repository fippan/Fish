﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaterContact : MonoBehaviour
{
    public FishyManager fishM;

    public void Start()
    {
        fishM = FindObjectOfType<FishyManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            fishM.StartFishing(transform);
        }
    }
}
