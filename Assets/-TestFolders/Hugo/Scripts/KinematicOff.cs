using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicOff : MonoBehaviour
{
    Rigidbody rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log(rb.name);
    }

    public void TurnOff ()
    {
        Debug.Log("I has turned off");
        rb.isKinematic = false;
    }
}
