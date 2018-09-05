using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public GameObject bob;
    public Vector3 throwPoint;
    public GameObject bobHolder;

	public void ThrowBob (Vector3 direction)
    {
        GameObject newBob = Instantiate(bob, throwPoint, transform.rotation);
        newBob.GetComponent<Rigidbody>().AddForce(direction);
    }
}
