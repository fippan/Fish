using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiverAttackers : MonoBehaviour {
    public GameObject spawnpoint;
    public GameObject spawnpoint1;
    public GameObject spawnpoint2;
    public GameObject spawnpoint3;

    public GameObject diver;

    public GameObject Player;

    private void Start()
    {
        DiverBehaviour();
    }
    void Update () {
        
    }

    public void spawnDiverOne()
    {
        Instantiate(diver, spawnpoint.transform);
    }

    public void spawnDiverTwo()
    {
        Instantiate(diver, spawnpoint1.transform);
    }
    public void spawnDiverThree()
    {
        Instantiate(diver, spawnpoint2.transform);
    }
    public void spawnDiverFour()
    {
        Instantiate(diver, spawnpoint3.transform);
    }


    private void DiverBehaviour()
    {
        diver.transform.LookAt(Player.transform);
        diver.transform.position = new Vector3(diver.transform.position.x, diver.transform.position.y + 10, diver.transform.position.z);
    }
}
