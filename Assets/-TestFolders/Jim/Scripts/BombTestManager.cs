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
        Bomb newBomb = Instantiate(bomb, transform.position, transform.rotation);
        newBomb.Throw(transform, GameObject.Find("Target").transform);
    }
}
