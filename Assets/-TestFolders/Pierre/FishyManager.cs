﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishyManager : MonoBehaviour {

    public int lowest;
    public int highest;
    public float waitTime;
    public GameObject[] fishies;
    public FishingStates theStates;

    public enum FishingStates
    {
        FISHING,
        NOTFISHING
    }



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(theStates == FishingStates.FISHING)
        {
            waitTime -= Time.deltaTime;

            int tempfish = Random.Range(0, fishies.Length);

            if (waitTime <= 0)
            {
                Instantiate(fishies[tempfish], transform);
                theStates = FishingStates.NOTFISHING;
            }
        }
        else if(theStates == FishingStates.NOTFISHING)
        {
            //gameObject.SetActive(false);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartFishing();
        }
    }

    public void StartFishing()
    {
        waitTime = Random.Range(lowest, highest);
        theStates = FishingStates.FISHING;
    }

}
