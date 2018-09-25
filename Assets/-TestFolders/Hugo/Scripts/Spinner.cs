using UnityEngine;

public class Spinner : MonoBehaviour
{
    //TODO: make sure the spinner does'nt calculate when you are not holding it!
    public float rotationSpeed = 0f;

    Vector3 rotationLast;
    Vector3 rotationDelta;

    //[HideInInspector]
    public bool isGrabbed = false;

    private void Start()
    {
        rotationLast = transform.rotation.eulerAngles;
    }

    private void Update()
    {
        if (isGrabbed)
        {
            rotationDelta = transform.rotation.eulerAngles - rotationLast;
            rotationLast = transform.rotation.eulerAngles;

            rotationSpeed = Mathf.Abs(rotationDelta.normalized.x * 1.5f);
        }
    }

    public void IsGrabbed ()
    {
        isGrabbed = !isGrabbed;
    }
}
