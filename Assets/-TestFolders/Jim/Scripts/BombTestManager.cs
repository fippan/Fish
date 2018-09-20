using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTestManager : MonoBehaviour
{

    public Bomb bomb;

    private void Start()
    {
        InvokeRepeating("SpawnBomb", 1f, .5f);
    }

    private void SpawnBomb()
    {
        Instantiate(bomb, transform.position, transform.rotation);
    }
}
