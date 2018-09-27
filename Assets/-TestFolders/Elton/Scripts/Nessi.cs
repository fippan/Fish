using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nessi : MonoBehaviour {
    [SerializeField] private float mulitplier;
    private float y = -28;

	// Update is called once per frame
	void Update () {
        y += Time.deltaTime * mulitplier;
        if (transform.position.y <= 0)
        {
            transform.position = new Vector3(-157.3f, y, -148.4f);
        }
	}
}
