using System.Collections;
using UnityEngine;

public class WaterContact : MonoBehaviour
{
    public FishingRod fishingRod;
    public FishyManager fishM;
    public bool fishing;
    public AudioSource splop;
    public bool hasLandedInWater;

    public void Start()
    {
        fishM = FindObjectOfType<FishyManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            splop.Play();

            if (fishing && !hasLandedInWater)
            {
                hasLandedInWater = true;
                if (Vector3.Distance(transform.position, fishingRod.transform.position) > 10f)
                {
                    fishM.StartFishing(transform);
                }
                else
                {
                    StartCoroutine(CheckDistance());
                }
            }
        }
    }

    private IEnumerator CheckDistance()
    {
        while (fishing && Vector3.Distance(transform.position, fishingRod.transform.position) < 10f)
        {
            yield return null;
        }
        fishM.StartFishing(transform);
    }
}
