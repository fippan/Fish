using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private Transform bob;
    private Transform lookPos;

    public void GetTransform (Transform newBob, Transform newLookPos)
    {
        bob = newBob;
        lookPos = newLookPos;
    }

    void Update()
    {
        if (bob == null)
        {
            return;
        }

        transform.position = bob.position;
        transform.LookAt(lookPos);
    }
}
