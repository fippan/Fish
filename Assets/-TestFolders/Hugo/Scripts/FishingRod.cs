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

    [Space(20)]
    public float floaterSpeed = 11f;

    private GameObject newBob;

    Spinner spinner;

    private void Start()
    {
        spinner = FindObjectOfType<Spinner>();
    }

    public void ThrowBob (Vector3 direction)
    {
        thrown = true;
        newBob = Instantiate(bob, throwPoint.position, transform.rotation);
        newBob.GetComponent<Rigidbody>().AddForce(direction * throwingMultiplier);
        spinner.isGrabbed = false;
    }

    public void Update()
    {
        if (spinner.isGrabbed)
        {
            Vector3 D = throwPoint.position - newBob.transform.position;
            float dist = D.magnitude;
            Vector3 pullDir = D.normalized;

            if (dist < .5f)
            {
                Destroy(newBob);
                thrown = false;
            }

            newBob.GetComponent<Rigidbody>().velocity += pullDir * (floaterSpeed * Time.deltaTime * spinner.rotationSpeed);
        }
    }
}
