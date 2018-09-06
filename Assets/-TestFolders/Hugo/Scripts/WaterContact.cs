using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaterContact : MonoBehaviour
{
    public UnityEvent onWaterHit;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            onWaterHit.Invoke();
        }
    }
}
