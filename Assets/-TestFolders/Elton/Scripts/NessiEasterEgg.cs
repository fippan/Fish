using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NessiEasterEgg : Health {
    [SerializeField] private GameObject nessi;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Death()
    {
        Instantiate(nessi,transform);
        Destroy(gameObject);
    }
}
