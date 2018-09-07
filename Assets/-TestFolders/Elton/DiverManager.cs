using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiverManager : MonoBehaviour {
    private bool waitForActive;
    private float randomSpawnTimer = 5;
    private int randomDiver;
    public List<SpawnPoints> spawnPointers;
    public DiverAttackers diveratt;
    public GameObject Diver;

    private void Start()
    {
        diveratt = GetComponent<DiverAttackers>();
        InvokeRepeating("SpawningDivers", 10f, 7);
        spawnPointers = new List<SpawnPoints>();
        spawnPointers[0].occupied = false;
        spawnPointers[1].occupied = false;
        spawnPointers[2].occupied = false;
        spawnPointers[3].occupied = false;
    }
    //Waiting for call to activate Divers
    void Update () {
        //if (waitForActive)
        //{
        //    SendingDivers();
        //}
	}

    //Sending 10 Divers at a random time through 4-7 seconds
    //void SendingDivers()
    //{
    //    //randomSpawnTimer = Random.Range(4, 7);
    //    InvokeRepeating("SpawningDivers", 10f, 7);
        
    //    waitForActive = false;
    //}

    //Spawning a random Diver 1-4
    void SpawningDivers()
    {
        randomDiver = Random.Range(1, 4);
        //Spawns Diver one
        if (spawnPointers[0].occupied == false && randomDiver == 1)
        {
            var diver = Instantiate(Diver, transform);
            spawnPointers[0].occupied = true;
            if(diver.GetComponent<DiverAttackers>().Dead == true)
            {
                spawnPointers[0].occupied = false;
            }
        }

        else if (spawnPointers[1].occupied == false && randomDiver == 2)
        {
            var diver = Instantiate(Diver, transform);
            spawnPointers[1].occupied = true;
            if (diver.GetComponent<DiverAttackers>().Dead == true)
            {
                spawnPointers[1].occupied = false;
            }
        }
        else if (spawnPointers[2].occupied == false && randomDiver == 3)
        {
            var diver = Instantiate(Diver, transform);
            spawnPointers[2].occupied = true;
            if (diver.GetComponent<DiverAttackers>().Dead == true)
            {
                spawnPointers[2].occupied = false;
            }
        }
        else if (spawnPointers[3].occupied == false && randomDiver == 4)
        {
            var diver = Instantiate(Diver, transform);
            spawnPointers[3].occupied = true;
            if (diver.GetComponent<DiverAttackers>().Dead == true)
            {
                spawnPointers[3].occupied = false;
            }
        }

        else if (spawnPointers[0].occupied == true && spawnPointers[1].occupied == true && spawnPointers[2].occupied == true && spawnPointers[3].occupied == true)
        {
            return;
        }
        else {
            SpawningDivers();
        }
    }
}

public class SpawnPoints : MonoBehaviour
{
    public bool occupied;
}
