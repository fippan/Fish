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

    private void Start()
    {
        bobRb = bob.GetComponent<Rigidbody>();
        spinner = FindObjectOfType<Spinner>();
    }

    public void ThrowBob (Vector3 direction)
    {
        thrown = true;
        closeEnough = false;
        bobRb.AddForce(direction * throwingMultiplier);
        spinner.isGrabbed = false;
    }

    public void Update()
    {
        Vector3 direction = throwPoint.position - bob.transform.position;
        float distance = direction.magnitude;

        if (spinner.isGrabbed)
        {
            Vector3 pullDir = direction.normalized;
            bobRb.velocity += (pullDir * floaterSpeed * Time.deltaTime) * spinner.rotationSpeed;
        }

        if (distance > .2f && thrown == false)
        {
            Vector3 pullDirection = direction.normalized;
            pullDirection.y = pullDirection.y * 5;
            bobRb.velocity += pullDirection * Time.deltaTime * 3.5f;
        }

        if (distance < .4f && thrown)
        {
            thrown = false;

            if (FishyManager.Instance.HasFish())
            {
                FishyManager.Instance.ResetFish();
                FishyManager.Instance.ExplodeFish();
                Haptics.Instance.StartHaptics(gameObject, 1, .5f, .1f);
            }
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
