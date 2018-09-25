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
    [SerializeField]
    private bool swimUp = true;
    private Coroutine swimRoutine;

    private void Start()
    {
        health = GetComponent<Health>();
        Player = GameObject.FindGameObjectWithTag("Player");
        anims = GetComponent<Animator>();
        enemyModel = diver;
        swimRoutine = StartCoroutine(SwimUpwards());
        if (Player != null)
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

    IEnumerator SwimUpwards()
    {
        while (swimUp)
        {
            if(transform.position.y >= 0f)
            {
                swimUp = false;
                StopCoroutine(swimRoutine);
            }
            transform.position += new Vector3(0, 0.5f * Time.deltaTime, 0);
            yield return null;
        }


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
