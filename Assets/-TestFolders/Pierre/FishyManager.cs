using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishyManager : MonoBehaviour {

    public int lowest;
    public int highest;
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
            Debug.Log("We are fishing for fishies");
        }
        else if(theStates == FishingStates.NOTFISHING)
        {
            gameObject.SetActive(false);
        }
	}

    void StartFishing()
    {
        int wait = Random.Range(lowest, highest);
    }

}
