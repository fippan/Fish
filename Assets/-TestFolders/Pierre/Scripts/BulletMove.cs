using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.forward * 0.5f;
	}
}
