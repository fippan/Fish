using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class DiverAttackers : Enemy
{

    public GameObject diver;

    public GameObject Player;

    [SerializeField] private Transform throwingStart;

    [SerializeField] private GameObject bomb;

    private Animator anims;
    private Health health;

    private void Start()
    {
        health = GetComponent<Health>();
        Player = GameObject.FindGameObjectWithTag("Player");
        anims = GetComponent<Animator>();
        enemyModel = diver;
        if(Player != null)
        {
            transform.LookAt(Player.transform);
        }
        InvokeRepeating("AttackPlayer", 2f, 15f);
    }

    private void AttackPlayer()
    {
        anims.SetTrigger("Throw");
        GameObject newBomb = Instantiate(bomb, throwingStart.position, Quaternion.identity);
        newBomb.GetComponent<Bomb>().Throw(throwingStart, Player.transform);
    }

    //public void CheckIfAlive()
    //{
    //    if (Dead == true)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    private void OnDisable()
    {
        CancelInvoke();
    }
}
