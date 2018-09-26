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

    Rigidbody bobRb;
    Spinner spinner;

    //[HideInInspector]
    public bool closeEnough = false;
    [HideInInspector]
    public bool throwable = false;

    private FishingLine fishingLine;
    private WaterContact waterContact;

    private void Start()
    {
        bobRb = bob.GetComponent<Rigidbody>();
        spinner = FindObjectOfType<Spinner>();
        fishingLine = GetComponent<FishingLine>();
        waterContact = bob.GetComponent<WaterContact>();
        bobRb.isKinematic = true;
        waterContact.fishing = false;
    }

    public void OnThrowBob(List<float> magnitudes)
    {
        float magnitude = GetHighestMagnitude(magnitudes);

        //if (magnitude < 1f)
        //    return;

        bobRb.isKinematic = false;
        fishingLine.reeledIn = false;
        //thrown = true;
        closeEnough = false;
        bobRb.AddForce(throwPoint.forward * magnitude * throwingMultiplier);
        spinner.isGrabbed = false;
        waterContact.fishing = true;
    }

    private float GetHighestMagnitude(List<float> magnitudes)
    {
        float highestMagnitude = 0;
        foreach (var item in magnitudes)
        {
            if (item > highestMagnitude)
                highestMagnitude = item;
        }
        return highestMagnitude;
    }

    public void ThrowBob (Vector3 direction)
    {
        //bobRb.isKinematic = false;
        //fishingLine.reeledIn = false;
        //thrown = true;
        //closeEnough = false;
        //bobRb.AddForce(direction * throwingMultiplier);
        //spinner.isGrabbed = false;
        //waterContact.fishing = true;
    }

    public void Update()
    {
        Vector3 direction = throwPoint.position - bob.transform.position;
        float distance = direction.magnitude;

        if (spinner.isGrabbed)
        {
            Vector3 pullDir = direction.normalized;
            if (bobRb.velocity.magnitude < 10f)
                bobRb.velocity += (pullDir * floaterSpeed * Time.deltaTime) * spinner.rotationSpeed;
            //Debug.Log(bobRb.velocity.magnitude);
        }

        //if (distance > .3f && thrown == false)
        //{
        //    if (bob.transform.position.y < throwPoint.position.y - .3f)
        //    {

        //    }
        //    Vector3 pullDirection = direction.normalized;
        //    //pullDirection.y = pullDirection.y * 5;
        //    bobRb.velocity = pullDirection * 2f/* * Time.deltaTime * 3.5f*/;
        //}

        if (distance < 1f && thrown)
        {
            thrown = false;

            if (FishyManager.Instance.HasFish())
            {
                FishyManager.Instance.ResetFish();
                FishyManager.Instance.ExplodeFish();
                Haptics.Instance.StartHaptics(gameObject, 1, .5f, .1f);
            }

            fishingLine.reeledIn = true;
            bobRb.isKinematic = true;
        }
    }

    public void Throwable()
    {
        throwable = true;
    }

    public void NotThrowable ()
    {
        throwable = false;
    }
}
