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

    [SerializeField] private AudioSource whoosh;

    [Space(20)]
    public float floaterSpeed = 11f;

    Rigidbody bobRb;
    Spinner spinner;

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
        if (thrown)
            return;

        whoosh.Play();
        bobRb.isKinematic = false;
        fishingLine.reeledIn = false;
        float magnitude = GetHighestMagnitude(magnitudes);
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

    public void Update()
    {
        Vector3 direction = throwPoint.position - bob.transform.position;
        float distance = direction.magnitude;

        if (spinner.isGrabbed)
        {
            Vector3 pullDir = direction.normalized;
            if (bobRb.velocity.magnitude < 10f)
                bobRb.velocity += (pullDir * floaterSpeed * Time.deltaTime) * spinner.rotationSpeed;
        }

        if (distance < 1f && thrown)
        {
            thrown = false;

            if (FishyManager.Instance.HasFish())
            {
                FishyManager.Instance.StopFishing();
                FishyManager.Instance.ResetFish();
                FishyManager.Instance.ExplodeFish();
                Haptics.Instance.StartHaptics(gameObject, 1, .5f, .1f);
            }

            fishingLine.reeledIn = true;
            bobRb.isKinematic = true;
        }
    }
}
