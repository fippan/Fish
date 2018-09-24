using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class DiverAttackers : Health
{

    public GameObject diver;

    public GameObject PlayerGameObject;

    [SerializeField] private Transform throwingStart;

    [SerializeField] private GameObject bomb;

    [SerializeField]
    private bool swimUp = true;
    private Coroutine swimRoutine;

    private Animator anims;
    private Health health;
    private List<string> attackVoices;

    protected override void Start()
    {
        base.Start();
        attackVoices = new List<string>(3);
        attackVoices.Add("Attack1");
        attackVoices.Add("Attack2");
        attackVoices.Add("Attack3");
        health = GetComponent<Health>();
        anims = GetComponent<Animator>();
        swimRoutine = StartCoroutine(SwimUpwards());
    }

    public void LookAtPlayer(Transform player)
    {
        PlayerGameObject = player.gameObject;
        //Vector3 targetDir = Vector3.RotateTowards(transform.position, player.position, 10f, 2f);
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        //transform.rotation = Quaternion.Euler(0, targetDir.y, 0);/*new Quaternion(player.rotation.x, transform.rotation.y, player.rotation.z, transform.rotation.w);*/
    }

    private void AttackPlayer()
    {
        anims.SetTrigger("Throw");
        GameObject newBomb = Instantiate(bomb, throwingStart.position, Quaternion.identity);
        newBomb.GetComponent<Bomb>().Throw(throwingStart, PlayerGameObject.transform);
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

    IEnumerator SwimUpwards()
    {
        while (swimUp)
        {
            if (transform.position.y >= -0.8f)
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

}
