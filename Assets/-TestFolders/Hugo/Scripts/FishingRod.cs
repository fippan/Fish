using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public float throwingMultiplier = 300f;
    [Space(20)]
    public bool thrown = false;
    public GameObject bob;
    public Transform throwPoint;
    public GameObject bobHolder;

    private GameObject newBob;

    public void ThrowBob (Vector3 direction)
    {
        thrown = true;
        newBob = Instantiate(bob, throwPoint.position, transform.rotation);
        newBob.GetComponent<Rigidbody>().AddForce(direction * throwingMultiplier);
    }
}
