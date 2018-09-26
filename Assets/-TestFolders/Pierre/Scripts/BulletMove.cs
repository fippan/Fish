using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {

    [SerializeField]
    private float damage;
	// Update is called once per frame
	void Update () {
        transform.position += transform.forward * 0.35f;
	}

    private void OnTriggerEnter(Collider other)
    {
        Health target = other.GetComponentInParent<Health>();
        if (target != null)
        {
            if(target != target.GetComponent<Submarine>())
            target.TakeDamage(damage, transform.position);
        }
    }
}
