using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnPoints : MonoBehaviour
{
    public bool occupied;
    public Transform spawnpoint;

    private void Start()
    {
        spawnpoint = this.transform;
    }
}