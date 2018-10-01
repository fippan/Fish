using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NessiEasterEgg : Health {
    [SerializeField] private GameObject nessi;
    public Transform spawnPoint;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Death()
    {
        Instantiate(nessi, spawnPoint.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
