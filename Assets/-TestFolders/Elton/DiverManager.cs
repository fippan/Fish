using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiverManager : MonoBehaviour {
    private bool waitForActive;
    private float randomSpawnTimer = 5;
    private int randomDiver;
    public List<SpawnPoints> spawnPointers;
    public Transform[] diverSpawns;
    public DiverAttackers diveratt;
    public GameObject Diver;

    private void Start()
    {
        diveratt = GetComponent<DiverAttackers>();
        InvokeRepeating("SpawningDivers", 10f, 7);

        for(int i = 0; i < spawnPointers.Count; i++)
        {
            spawnPointers[i].spawnpoint = diverSpawns[i];
            spawnPointers[i].occupied = false;
        }

        SpawningDivers();
    }
    void Update () {

	}

    void SpawningDivers()
    {
        //check if Diver is alive, if not make the space unoccupied and destroy old diver.
        for(int i = 0; i < spawnPointers.Count; i++)
        {
            if(spawnPointers[i].GetComponentInChildren<DiverAttackers>() != null)
            {
                spawnPointers[i].GetComponentInChildren<DiverAttackers>().CheckIfAlive();
                var alive = spawnPointers[i].GetComponentInChildren<DiverAttackers>();
                if (alive.Dead == true)
                {
                    spawnPointers[i].occupied = false;
                }
            }
            else
            {
                spawnPointers[i].occupied = false;
            }

        }
        randomDiver = Random.Range(1, 5);
        //Spawns Diver one
        if (spawnPointers[0].occupied == false && randomDiver == 1)
        {
            var diver = Instantiate(Diver, spawnPointers[0].spawnpoint.position, new Quaternion(0,0,0,0), spawnPointers[0].transform);
            diver.GetComponent<DiverAttackers>().DiverBehaviour(spawnPointers[0]);
        }

        else if (spawnPointers[1].occupied == false && randomDiver == 2)
        {
            var diver = Instantiate(Diver, spawnPointers[1].spawnpoint);
            diver.GetComponent<DiverAttackers>().DiverBehaviour(spawnPointers[1]);
        }
        else if (spawnPointers[2].occupied == false && randomDiver == 3)
        {
            var diver = Instantiate(Diver, spawnPointers[2].spawnpoint);
            diver.GetComponent<DiverAttackers>().DiverBehaviour(spawnPointers[2]);
        }
        else if (spawnPointers[3].occupied == false && randomDiver == 4)
        {
            var diver = Instantiate(Diver, spawnPointers[3].spawnpoint);
            diver.GetComponent<DiverAttackers>().DiverBehaviour(spawnPointers[3]);
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

