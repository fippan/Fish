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
	public GameObject coinSpray;
	public GameObject fish;
	//public VRTK_InteractGrab grab;

	private Transform spawnPos;

	public enum FishingStates
	{
		FISHING,
		NOTFISHING
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
				fish = Instantiate(fishies[tempfish], spawnPos);

				StopFishing();
			}
		}
		else if (theStates == FishingStates.NOTFISHING)
		{
			//gameObject.SetActive(false);
		}


        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartFishing(gameObject.transform);
        }
	}

	public void StartFishing(Transform newSpawnPos)
	{
		spawnPos = newSpawnPos;
		waitTime = Random.Range(lowest, highest);
		theStates = FishingStates.FISHING;
	}

	public void StopFishing()
	{
		timeCounter = 0f;
		theStates = FishingStates.NOTFISHING;
	}

	//public void PickupFish(int amount)
	//{
	//    Instantiate(coinSpray, fish.transform);
	//    Destroy(fish.gameObject);
	//    Destroy(coinSpray);
	//}

}
