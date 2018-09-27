using UnityEngine;

public class WaterContact : MonoBehaviour
{
    public FishingRod fishingRod;
    public FishyManager fishM;
    public bool fishing;

    public void Start()
    {
        fishM = FindObjectOfType<FishyManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!fishing)
            return;

        if (other.gameObject.tag == "Water")
        {
            if (Vector3.Distance(transform.position, fishingRod.transform.position) > 10f)
            {
                fishM.StartFishing(transform);
                fishingRod.thrown = true;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            fishM.StopFishing();
        }
    }
}
