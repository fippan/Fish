using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnHit : MonoBehaviour {


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Bullet"))
        {
            //TODO take a certain damage and play animation.
        }

        if (collision.gameObject.tag == ("FishingRod"))
        {
            //TODO take a certain damage and play animation.
        }
    }
}
