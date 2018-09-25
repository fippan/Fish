using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class DiverAttackers : Health
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
    private List<string> attackVoices;

    protected override void Start()
    {
        attackVoices = new List<string>(3);
        attackVoices.Add("Attack1");
        attackVoices.Add("Attack2");
        attackVoices.Add("Attack3");
        health = GetComponent<Health>();
        Player = GameObject.FindGameObjectWithTag("Player");

        anims = GetComponent<Animator>();
        swimRoutine = StartCoroutine(SwimUpwards());
        if (Player != null)
        {
            transform.LookAt(Player.transform);
        }
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
                int temp = Random.Range(0, 2);
                GetComponent<AudioController>().PlayOneShot(attackVoices[temp], transform.position);
                swimUp = false;
                StopCoroutine(swimRoutine);
                InvokeRepeating("AttackPlayer", 2f, 15f);
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
