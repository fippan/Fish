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

    GameObject newBob;
    Rigidbody newBobRB;
    Spinner spinner;

    [HideInInspector]
    public bool closeEnough = false;

    private void Start()
    {
        spinner = FindObjectOfType<Spinner>();
    }

    public void ThrowBob (Vector3 direction)
    {
        thrown = true;
        newBob = Instantiate(bob, throwPoint.position, transform.rotation);
        newBobRB = newBob.GetComponent<Rigidbody>();
        newBobRB.AddForce(direction * throwingMultiplier);
        spinner.isGrabbed = false;
    }

    public void Update()
    {
        if (spinner.isGrabbed && !closeEnough && newBob != null)
        {
            Vector3 D = throwPoint.position - newBob.transform.position;
            float dist = D.magnitude;
            Vector3 pullDir = D.normalized;

            newBobRB.velocity += pullDir * ((floaterSpeed * spinner.rotationSpeed) * Time.deltaTime);
        }

        if (closeEnough)
        {
            Vector3 D = throwPoint.position - newBob.transform.position;
            float dist = D.magnitude;
            Vector3 pullDir = D.normalized;
            pullDir.y = pullDir.y * 5;
            newBobRB.velocity += pullDir * Time.deltaTime * 3.5f;
            
            if (dist < .3f)
            {
                if (newBob.GetComponent<CoughtFish>().hasFish)
                {
                    //TODO: Spawn The particle effect!!! and Get the MoNeYYY!
                }

                thrown = false;
                closeEnough = false;
                Destroy(newBob);
            }
        }
    }
}
