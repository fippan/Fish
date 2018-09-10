using UnityEngine;

public class WaterContact : MonoBehaviour
{
    public FishyManager fishM;

    public void Start()
    {
        fishM = FindObjectOfType<FishyManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            fishM.StartFishing(transform);
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
