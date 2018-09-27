using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;


public class FishyManager : MonoBehaviour
{
	public int lowest;
	public int highest;
	private float waitTime;
	private float timeCounter = 0f;
	public GameObject[] fishies;
	public FishingStates theStates;
	public ParticleSystem coinSpray;
    public ParticleSystem smokeFX;
    public GameObject currentFish;
	//public VRTK_InteractGrab grab;
    [SerializeField]
	private Transform spawnPos;
    public Transform SpawnPos
    {
        get { return spawnPos; }
        set { spawnPos = value; }
    }
	private bool caughtFish = false;

	public enum FishingStates
	{
		FISHING,
		NOTFISHING
	}

	public static FishyManager Instance { get; private set; }

	void Awake()
	{
		Instance = this;
        theStates = FishingStates.NOTFISHING;
	}

	// Update is called once per frame
	void Update()
	{
		if (theStates == FishingStates.FISHING)
		{
			timeCounter += Time.deltaTime;

			if (timeCounter >= waitTime)
			{
				int tempfish = Random.Range(0, fishies.Length);
				currentFish = Instantiate(fishies[tempfish], spawnPos.transform.position, Quaternion.identity);
                currentFish.GetComponent<FishFollowTransform>().Follow();

                //fish.GetComponent<VRTK_TransformFollow>().gameObjectToFollow = spawnPos.gameObject;

                caughtFish = true;
				StopFishing();
			}
		}
		else if (theStates == FishingStates.NOTFISHING)
		{
			//gameObject.SetActive(false);
		}
	}

	public void StartFishing(Transform newSpawnPos)
	{
		if (!caughtFish)
		{
			spawnPos = newSpawnPos;
			waitTime = Random.Range(lowest, highest);
			theStates = FishingStates.FISHING;
		}
	}

	public void StopFishing()
	{
        timeCounter = 0f;
		theStates = FishingStates.NOTFISHING;
	}

	public bool HasFish()
	{
		return caughtFish;
	}

	public void ResetFish()
	{
		caughtFish = false;
	}

    public void ExplodeFish()
    {
        currentFish.transform.parent = null;
        currentFish.GetComponentInChildren<ParticleSystem>().Play();
        float amount = currentFish.GetComponent<FishWorth>().worth;
        CurrencyManager.Instance.AddCurrency(amount);
        ParticleSystem ps = Instantiate(coinSpray, currentFish.transform.position, currentFish.transform.rotation) as ParticleSystem;
        ParticleSystem ps2 = Instantiate(smokeFX, currentFish.transform.position, currentFish.transform.rotation) as ParticleSystem;
        Destroy(ps.gameObject, 4f);
        Destroy(ps2.gameObject, 1f);
        Destroy(currentFish);
        //AudioManager.instance.Play("Explosion");
        //AudioManager.instance.Play("Rattle");
    }

	//public void PickupFish(int amount)
	//{
	//    Instantiate(coinSpray, fish.transform);
	//    Destroy(fish.gameObject);
	//    Destroy(coinSpray);
	//}

}
