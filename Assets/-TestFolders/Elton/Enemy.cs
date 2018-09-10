using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ICanTakeDamage{
    public float health;
    public bool Dead;
    public GameObject enemyModel;


    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);

        if(health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Dead = true;
        Destroy(gameObject);
        //kill
    }
}
