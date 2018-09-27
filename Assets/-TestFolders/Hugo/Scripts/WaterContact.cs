using UnityEngine;

public class WaterContact : MonoBehaviour
{
    public FishingRod fishingRod;
    public FishyManager fishM;
    public bool fishing;
    private AudioSource splop;

    public void Start()
    {
        fishM = FindObjectOfType<FishyManager>();
        splop.GetComponent<AudioSource>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!fishing)
            return;

        if (other.gameObject.tag == "Water")
        {
            splop.Play();
            if (Vector3.Distance(transform.position, fishingRod.transform.position) > 10f)
            {
                fishM.StartFishing(transform);
                fishingRod.thrown = true;
            }
        }
    }
}
