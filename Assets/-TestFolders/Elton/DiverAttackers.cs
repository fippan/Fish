using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiverAttackers : Enemy {

    public GameObject diver;

    public GameObject Player;

    private void Start()
    {
        health = 100;
        enemyModel = diver;
        //DiverBehaviour(spawns);
    }
    void Update () {

    }


    public void DiverBehaviour(SpawnPoints spawns)
    {
        spawns.occupied = true;
        diver.transform.LookAt(Player.transform);
        if(diver.transform.position.y < 10f)
        {
            //diver.transform.position += new Vector3(diver.transform.position.x, diver.transform.position.y + 10, diver.transform.position.z) * Time.deltaTime;
        }
        if(Dead == true)
        {
            Destroy(gameObject);
        }
    }

    public void CheckIfAlive()
    {
            if (Dead == true)
            {
                Destroy(gameObject);
            }
    }
}
