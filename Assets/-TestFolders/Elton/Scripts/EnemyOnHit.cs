using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnHit : MonoBehaviour {

    public Enemy enemy;
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == ("Bullet"))
        {
            //TODO take a certain damage and play animation.
        }

        if (collision.gameObject.tag == ("FishingRod"))
        {
            //enemy.TakeDamage(5f);
            anim.SetBool("Punched", true);
        }
    }
}
