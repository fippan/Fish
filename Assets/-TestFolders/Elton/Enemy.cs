using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public int health;
    public bool Dead;
    public GameObject enemyModel;


    public void TakeDamage(int damage)
    {
        if(health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Dead = true;
        Destroy(transform);
        //kil
    }
}
