using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    public GameObject boat;
    void Update()
    {
        transform.position = boat.transform.position;

        transform.Rotate(0, -0.05f, 0);
    }
}
