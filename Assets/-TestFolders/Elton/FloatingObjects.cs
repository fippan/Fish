using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObjects : MonoBehaviour {

    [SerializeField] private float size;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Water")
        {
            if(gameObject.GetComponent<Rigidbody>() != null)
            {
                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                rb.useGravity = false;

            }
            else
            {
                Debug.LogWarning("HAS NO RIGIDBODY!");
            }
        }
    }
}
