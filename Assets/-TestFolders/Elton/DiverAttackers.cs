using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class DiverAttackers : Enemy {

    public GameObject diver;

    public GameObject Player;

    [SerializeField] private Transform throwingStart;

    [SerializeField] private GameObject bomb;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        //health = 100;
        enemyModel = diver;
        transform.LookAt(Player.transform);
        InvokeRepeating("AttackPlayer", 2f,15f);
        //DiverBehaviour(spawns);
    }
    void Update () {

    }

    private void AttackPlayer()
    {
        GameObject newBomb = Instantiate(bomb, throwingStart.position, Quaternion.identity);
        newBomb.GetComponent<Bomb>().Throw(throwingStart, Player.transform);
    }

    public void DiverBehaviour(SpawnPoints spawns)
    {
        spawns.occupied = true;
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

    private void OnDisable()
    {
        CancelInvoke();
    }
}
