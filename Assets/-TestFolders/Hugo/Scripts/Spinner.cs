using UnityEngine;

public class Spinner : MonoBehaviour
{
    //TODO: make sure the spinner does'nt calculate when you are not holding it!
    public float rotationSpeed = 0f;

    Vector3 rotationLast;
    Vector3 rotationDelta;

    [SerializeField] private AudioSource reelIn;

    //[HideInInspector]
    public bool isGrabbed = false;
    private bool playsound = false;

    private void Start()
    {
        rotationLast = transform.rotation.eulerAngles;
        reelIn = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isGrabbed)
        {
            rotationDelta = transform.rotation.eulerAngles - rotationLast;
            rotationLast = transform.rotation.eulerAngles;

            rotationSpeed = Mathf.Abs(rotationDelta.normalized.x * 1.5f);
            if (rotationSpeed > 0.3 && !playsound)
            {
                reelIn.Play();
                Invoke("ReelSound", reelIn.clip.length);
                playsound = true;
            }
        }

        if (!isGrabbed)
        {
            reelIn.Stop();
            playsound = false;
        }
    }

    public void IsGrabbed ()
    {
        isGrabbed = !isGrabbed;
    }

    private void ReelSound()
    {
        playsound = false;
    }
}
